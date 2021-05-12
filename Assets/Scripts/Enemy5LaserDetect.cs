using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5LaserDetect : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    int rand;
    private bool _isMoveRight;
    int i = 0;
    int dir = -1;
    private Player player;
    private Transform _playerLoc;

    private float _canFire = -1;
    private float _fireRate;
    private int _life;
    float _laserInstantiateLoc;


    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private GameObject laser;
    [SerializeField]
    private Transform _laserPos;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }

        _playerLoc = GameObject.Find("Player").GetComponent<Transform>();
        if (_playerLoc == null)
        {
            Debug.LogError("Player Transform is null");
        }

    }

    private void Update()
    {
        if(_isMoveRight==true)
        {
            if(rand==0 )
            {
                transform.Translate(Vector3.right * 10 * Time.deltaTime);
            }
            else if(rand==1 )
            {
                transform.Translate(Vector3.left * 10 * Time.deltaTime);
            }
            
        }
        else
        {
            CalculateMovementDown();
        }

        try
        {
            if (transform.position.y < _playerLoc.transform.position.y)
            {
                RotateEnemy(); 
            }
            else
            {
                dir = -1;
                _laserInstantiateLoc = -0.6f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                i = 0;
            }
        }
        catch
        {
            Debug.Log("Player is dead");
        }
        


        if (player != null)
        {
            _life = player.GetLives();
        }


        if (_life > 0)
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
            
            if(i==0)
            {
                _canFire = 1;
            }
            i = 1;
            FireLaser();
        }
        else
        {
            dir = -1;
            i = 0;
            _laserInstantiateLoc = -0.6f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FireLaser()
    {
         if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 5f);
                _canFire = Time.time + _fireRate;
                laser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, _laserInstantiateLoc, 0), Quaternion.identity);
                Enemy_Laser _laser = laser.GetComponent<Enemy_Laser>();
                if (_laserInstantiateLoc == 0.6f)
                {
                    _laser.FireUp();
                }
                else if (_laserInstantiateLoc == -0.6f)
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
        if(other.gameObject.tag=="Laser")
        {
            _isMoveRight = true;
            rand = Random.Range(0, 2);
            StartCoroutine(StopMovingRightRoutine());
            
        }
    }

    IEnumerator StopMovingRightRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        _isMoveRight = false;
    }
}
