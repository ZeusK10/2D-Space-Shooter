using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Boss_Side_Enemy : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private GameObject _laser;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }
        StartCoroutine(FireLaserRoutine());
    }
    void Update()
    {
        Movement();
    }

    IEnumerator FireLaserRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            Instantiate(_laser, transform.position + new Vector3(0, -2f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        transform.Translate(Vector3.down * 5 * Time.deltaTime);

        if(transform.position.y<-8)
        {
            if(Random.Range(0,2)==1)
            {
                transform.position = new Vector3(Random.Range(-9.5f,-5f), 8, 0);
            }
            else
            {
                transform.position = new Vector3(Random.Range(5f, 9.5f), 8, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player = other.GetComponent<Player>();

            player.UpdateScore(10);
            player.Damage();
            Destroy(this.gameObject);
        }
        else if(other.tag=="Laser")
        {
            player.UpdateScore(15);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
