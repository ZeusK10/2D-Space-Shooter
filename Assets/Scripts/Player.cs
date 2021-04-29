using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
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
    private bool isShieldPowerupActive = false;

    [SerializeField]
    private GameObject _shield;

    private AudioSource _laserAudio;

    [SerializeField]
    private int _Lives = 3;

    private int _speedMultiplier = 2;

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

        if(isSpeedPowerupActive)
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
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
    }

    public void Damage()
    {
        if(isShieldPowerupActive)
        {
            isShieldPowerupActive = false;
            _shield.SetActive(false);
            
        }
        else
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
        _shield.SetActive(true);
        isShieldPowerupActive = true;
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





