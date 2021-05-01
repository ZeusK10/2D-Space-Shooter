using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _baseSpeed = 5f;
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
    private bool isSpeedPowerupActive = false;

    [SerializeField]
    private int isShieldPowerupActive = 0;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private SpriteRenderer _shieldColor;

    private AudioSource _laserAudio;

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

        _shieldColor = _shield.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
        {
            FireLaser();
        }
        
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,verticalInput, 0);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 1.5f;
        }
        else
        {
            _speed = 1;
        }
        if (isSpeedPowerupActive)
        {
            transform.Translate(direction *_baseSpeed * _speed * _speedMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction *_baseSpeed * _speed * Time.deltaTime);
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
    }

    public void Damage()
    {
        if(isShieldPowerupActive == 3)
        {
            //_shieldColor.color = new Color(0, 1, 0, 1);
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
        
        StartCoroutine(SpeedPowerDownRoutine());

    }

    public void ShieldPowerup()
    {
        _shieldColor.color = new Color(0, 1, 0, 1);
        _shield.SetActive(true);
        
        isShieldPowerupActive = 3;
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isSpeedPowerupActive = false;
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _uIManager.UpdatePlayerScore(_score);
    }

}





