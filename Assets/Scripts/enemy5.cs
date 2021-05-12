using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player player;
    private Transform _playerLoc;
    private bool _isEnemyDead = false;
    [SerializeField]
    private bool _isRotated;

    private float _canFire = -1;
    private float _fireRate;
    private int _life;
    float _laserInstantiateLoc;
    int dir;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private GameObject laser;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }

        _playerLoc = GameObject.Find("Player").GetComponent<Transform>();
        if(_playerLoc==null)
        {
            Debug.LogError("Player Transform is null");
        }

    }

    void Update()
    {

        if (transform.position.y - _playerLoc.transform.position.y < -2f)
        {
            RotateEnemy();
        }
        else
        {
            dir = -1;
            _laserInstantiateLoc = -0.6f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        CalculateMovementDown();
        



        if (player != null)
        {
            _life = player.GetLives();
        }


        if (_isEnemyDead == false && _life > 0)
        {
            FireLaser();
        }
    }

   

    private void RotateEnemy()
    {
        
            if (player.transform.position.x < transform.position.x + 2 && player.transform.position.x > transform.position.x - 2)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                dir = 1;
                _laserInstantiateLoc = 0.6f;
                FireLaser();
            }
            else
            {
                dir = -1;
                _laserInstantiateLoc = -0.6f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        
        

    }

    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(1f, 3f);
            _canFire = Time.time + _fireRate;
            laser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, _laserInstantiateLoc, 0), Quaternion.identity);
            Enemy_Laser _laser = laser.GetComponent<Enemy_Laser>();
            if(_laserInstantiateLoc==0.6f)
            {
                _laser.FireUp();
            }
            else if(_laserInstantiateLoc==-0.6f)
            {
                _laser.FireDown();
            }

            

        }
    }

   

    private void CalculateMovementDown()
    {
        transform.Translate(new Vector3(0,dir,0) * _speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.1f), 9, 0);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            player.UpdateScore(5);

            _isEnemyDead = true;
            Destroy(laser.gameObject);
            Destroy(this.gameObject);


        }
        else if (other.tag == "Laser")
        {

            player.UpdateScore(10);
            Destroy(other.gameObject);
            _speed = 0f;
            _isEnemyDead = true;
            Destroy(laser.gameObject);
            Destroy(this.gameObject);



        }
        else if (other.tag == "Destructive_Laser")
        {
            player.UpdateScore(10);
            _speed = 0f;
            _isEnemyDead = true;
            Destroy(laser.gameObject);
            Destroy(this.gameObject);

        }

    }
}
