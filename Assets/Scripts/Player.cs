using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;


    [SerializeField]
    private int _ammo = 15;

    [SerializeField]
    private GameObject _destructiveLaser;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private SpawnManager _spawnManager;
    private UIManager _uIManager;
    [SerializeField]
    private bool isTripleShotActive = false;

    [SerializeField]
    public bool isSpeedPowerupActive = true;

    [SerializeField]
    private int isShieldPowerupActive = 0;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private HUD_Bar _hudBar;

    private CameraShake _cameraShake;

    [SerializeField]
    private SpriteRenderer _shieldColor;

    private AudioSource _laserAudio;
    private bool _isDestructiveLaserActive = false;

    [SerializeField]
    private int _Lives = 3;

    private int _speedMultiplier = 3;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    private int _score;

    [SerializeField]
    private GameObject _playerHurtRight;
    [SerializeField]
    private GameObject _playerHurtLeft;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager==null)
        {
            Debug.LogError("_Spawn Manager is null");
        }

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if (_cameraShake == null)
        {
            Debug.LogError("_camera shake is null");
        }

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uIManager==null)
        {
            Debug.LogError("UI Manager is null");
        }

        _laserAudio = GameObject.Find("Laser_Music").GetComponent<AudioSource>();
        if(_laserAudio==null)
        {
            Debug.LogError("Audio Source is null");
        }

        _hudBar = GameObject.Find("HUD_Foreground").GetComponent<HUD_Bar>();

        _shieldColor = _shield.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        _uIManager.UpdatePlayerAmmo(_ammo);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
        {
            if(_ammo>0 && _isDestructiveLaserActive==false)
            {
                FireLaser();
            }
            
        }
        
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,verticalInput, 0);

        
       
        if (isSpeedPowerupActive && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
            _hudBar.UpdateHUDBar();
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);
        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {   
        _canFire = Time.time + _fireRate;
        if(isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }
        _laserAudio.Play();
        _ammo -= 1;
        
    }

    public void Damage()
    {
        if(isShieldPowerupActive == 3)
        {
            _shieldColor.color = new Color(0, 0, 1, 1);
            isShieldPowerupActive -= 1;
             
        }
        else if(isShieldPowerupActive == 2)
        {
            _shieldColor.color = new Color(1, 0, 0, 1);
            isShieldPowerupActive -= 1;
           
        }
        else if (isShieldPowerupActive == 1)
        {
            _shield.SetActive(false);
            isShieldPowerupActive -= 1;
           
        }
        else if (isShieldPowerupActive == 0)
        {
            _Lives -= 1;
            _cameraShake.Shake();
            if (_Lives == 2)
            {
                _playerHurtLeft.SetActive(true);
            }
            else if (_Lives == 1)
            {
                _playerHurtRight.SetActive(true);
            }

            _uIManager.UpdatePlayerLives(_Lives);
            if (_Lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
        
    }


    public void TripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerup()
    {
        isSpeedPowerupActive = true;
        _hudBar.AddHUDBar();
        _hudBar.UpdateHUDBar();
    }
    public void SpeedPowerDown()
    {
        isSpeedPowerupActive = false;
    }

    public void ShieldPowerup()
    {
        _shieldColor.color = new Color(0, 1, 0, 1);
        _shield.SetActive(true);
        
        isShieldPowerupActive = 3;
    }

    public void AmmoPowerup()
    {
        _ammo = 15;
    }

    public void HealthPowerup()
    {
        if(_Lives<3)
        {
            _Lives += 1;
            _uIManager.UpdatePlayerLives(_Lives);
        }

        if (_Lives == 2)
        {
            _playerHurtRight.SetActive(false);
        }
        else if (_Lives == 3)
        {
            _playerHurtLeft.SetActive(false);
        } 

    }

    public void DestructivePowerup()
    {
        _destructiveLaser.SetActive(true);
        _isDestructiveLaserActive = true;
        StartCoroutine(DestructiveLaserStopRoutine());
    }

    IEnumerator DestructiveLaserStopRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isDestructiveLaserActive = false;
        _destructiveLaser.SetActive(false);
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

    

    public void UpdateScore(int points)
    {
        _score += points;
        _uIManager.UpdatePlayerScore(_score);
    }

}





