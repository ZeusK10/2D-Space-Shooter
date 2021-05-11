using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player player;
    private bool _isEnemyDead = false;

    private float _canFire = -1;
    private float _fireRate;
    private int _life;

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

    }

    void Update()
    {
        
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

    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(5f, 10f);
            _canFire = Time.time + _fireRate;
            laser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.6f, 0), Quaternion.identity);
            
        }
    }

    private void CalculateMovementDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6 )
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
