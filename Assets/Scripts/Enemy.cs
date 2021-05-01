using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private AudioSource _explosionAudio;
    private Player player;

    private Animator _animation;

    private float _canFire = -1;
    private float _fireRate;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player==null)
        {
            Debug.Log("Player is null");
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
    }

    void Update()
    {
        CalculateMovement();

        if(Time.time>_canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.6f, 0), Quaternion.identity);
           
        }
        
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

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
            _animation.SetTrigger("IsEnemyDead");
            _explosionAudio.Play();
            _speed = 0f;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.8f);
        }
        else if (other.tag == "Laser")
        {
            player.UpdateScore(10);
            Destroy(other.gameObject);
            _speed = 0f;
            _animation.SetTrigger("IsEnemyDead");
            _explosionAudio.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.8f);
        }
    }

   
}
