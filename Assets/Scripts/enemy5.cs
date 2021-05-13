using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5 : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
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

                Destroy(transform.parent.gameObject);


        }
            else if (other.tag == "Laser")
            {

                player.UpdateScore(10);
                Destroy(other.gameObject);
                Destroy(transform.parent.gameObject);
        }
            else if (other.tag == "Destructive_Laser")
            {
                player.UpdateScore(10);
                Destroy(transform.parent.gameObject);
            }
            else if (other.tag == "Projectile")
            {
                player.UpdateScore(10);
                Destroy(other.gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
}
