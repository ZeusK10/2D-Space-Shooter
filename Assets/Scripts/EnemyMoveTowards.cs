using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTowards : MonoBehaviour
{
    private int _speed = 4;
    private AudioSource _explosionAudio;
    private Transform player;
    private Player _player;
    private bool _isenemydead;
    private bool _isPlayerAlive=true;

    private float _canFire = -1;
    private float _fireRate;

    private bool rotating;

    [SerializeField]
    private GameObject _enemyLaserPrefab;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        if (player == null)
        {
            Debug.Log("Player Transform is null");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player script is null");
        }

        _explosionAudio = GameObject.Find("Explosion_Music").GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("Audio Source is null");
        }
    }

    void Update()
    {
        PlayerDied();
        if (_isenemydead==false)
        {
            if(player!=null)
            {
                EnemyMovement();
            }
            
            if(rotating==false)
            {
                FireLaser();
            }
           
        }
        
        
    }

    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            
            
        }
    }

    void EnemyMovement()
    {
        
            if (Vector3.Distance(transform.position, player.position) < 5f && _isPlayerAlive)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
                RotateTowardsPlayer();
                rotating = true;
                if (Vector3.Distance(transform.position, player.position) < 0.001f)
                {
                    player.position *= -1.0f;

                }
            }
            else
            {
                transform.Translate(Vector3.down * 4 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rotating = false;
            }

            if (transform.position.y < -6)
            {
                transform.position = new Vector3(Random.Range(-9.0f, 9.1f), 9, 0);
            }
        
    }

    void RotateTowardsPlayer()
    {

        Vector3 offset = transform.position - player.position;

        transform.rotation = Quaternion.LookRotation(new Vector3(0,0,1),offset);
       
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
            _explosionAudio.Play();
            _speed = 0;
            _isenemydead = true;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            _player.UpdateScore(10);
            Destroy(other.gameObject);
            _speed = 0;
            _explosionAudio.Play();
            _isenemydead = true;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Destructive_Laser")
        {
            _player.UpdateScore(10);
            _speed = 0;
            _explosionAudio.Play();
            _isenemydead = true;
            Destroy(this.gameObject);
        }
    }

    public void PlayerDied()
    {
        int life = _player.GetLives();
        if(life<1)
        {
            _isPlayerAlive = false;
        }
    }


}
