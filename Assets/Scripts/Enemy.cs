using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private AudioSource _explosionAudio;
    private Player player;
    private bool _isEnemyDead = false;

    private Animator _animation;

    private float _canFire = -1;
    private float _fireRate;
    private int _life;

    [SerializeField]
    private bool _isNewEnemy;
    private int rand;

    [SerializeField]
    private bool _isSpawningTop=false;
    [SerializeField]
    private bool _isSpawningRight=false;
    

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player==null)
        {
            Debug.LogError("Player is null");
        }

        _explosionAudio = GameObject.Find("Explosion_Music").GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("Audio Source is null");
        }

        _animation = gameObject.GetComponent<Animator>();
        if(_animation==null)
        {
            Debug.Log("Animator is null");
        }

        if (transform.position.x >11.5f)
        {
            _isSpawningRight = true;
        }

        if(transform.position.y>8.5f)
        {
            _isSpawningTop = true;
        }

        rand = Random.Range(0, 2);
    }

    void Update()
    {
        if(_isNewEnemy==false)
        {
            CalculateMovementDown();
        }
        else
        {
            UniqueMovement();
        }

        if(player!=null)
        {
            _life = player.GetLives();
        }
        

        if(_isEnemyDead==false && _life>0)
        {
            FireLaser();
        }
    }


    private void UniqueMovement()
    {
        if(rand==0)
        {
            transform.Translate(new Vector3(-1, -1, 0) * _speed * Time.deltaTime);

            if(transform.position.x<-10)
            {
                rand = 1;
            }
        }
        else
        {
            transform.Translate(new Vector3(1, -1, 0) * _speed * Time.deltaTime);

            if(transform.position.x>10)
            {
                rand = 0;
            }
        }

        if (transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.1f), 9, 0);
        }

    }

    public void EnableUniqueMovement()
    {
        _isNewEnemy = true;
    }

    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.6f, 0), Quaternion.identity);
        }
    }

    private void CalculateMovementDown()
    {
        if(_isSpawningTop==true && _isSpawningRight==false)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if(_isSpawningRight==true && _isSpawningTop==false)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else if(_isSpawningTop==false && _isSpawningRight==false)
        {
             transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        
        

        if (transform.position.y < -6 && _isSpawningTop==true)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.1f), 9, 0);
        }
        else if(transform.position.x < -12 && _isSpawningRight==true)
        {
            transform.position = new Vector3(12, Random.Range(0f, 5.5f), 0);
        }
        else if(transform.position.x > 12 && _isSpawningRight == false)
        {
            transform.position = new Vector3(-12, Random.Range(0f, 5.5f), 0);
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
            
            
            _explosionAudio.Play();
            _speed = 0f;
            _isEnemyDead = true;
            Destroy(GetComponent<Collider2D>());
            if (_isNewEnemy == false)
            {
                _animation.SetTrigger("IsEnemyDead");
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "Laser")
        {
            player.UpdateScore(10);
            Destroy(other.gameObject);
            _speed = 0f;
            _isEnemyDead = true;
            
            _explosionAudio.Play();
            Destroy(GetComponent<Collider2D>());
            if (_isNewEnemy == false)
            {
                _animation.SetTrigger("IsEnemyDead");
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
        else if (other.tag == "Destructive_Laser")
        {
            player.UpdateScore(10);
            _speed = 0f;
            _isEnemyDead = true;
            
            _explosionAudio.Play();
            Destroy(GetComponent<Collider2D>());
            if (_isNewEnemy == false)
            {
                _animation.SetTrigger("IsEnemyDead");
                Destroy(this.gameObject, 2.8f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }



}
