using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
   
    private Player player;

    private Animator _animation;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player==null)
        {
            Debug.Log("Player is null");
        }

        _animation = gameObject.GetComponent<Animator>();
        if(_animation==null)
        {
            Debug.Log("Animator is null");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y<-6)
        {
            transform.position= new Vector3(Random.Range(-9.0f, 9.1f), 9, 0);
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
            _speed = 0f;
            //provide score value 5 when player and enemy collide
            Destroy(this.gameObject,2.8f);
        }
        else if (other.tag == "Laser")
        {
            player.UpdateScore(10);
            //provide score value 10 when laser hits enemy
            Destroy(other.gameObject);
            _speed = 0f;
            _animation.SetTrigger("IsEnemyDead");
            Destroy(this.gameObject,2.8f);
        }
    }
}
