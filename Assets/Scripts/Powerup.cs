using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int powerupID;
    void Update()
    {
        transform.Translate(Vector3.down * 3f * Time.deltaTime);
        if(transform.position.y<-4.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player!=null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShot();
                        break;

                    case 1:
                        player.SpeedPowerup();
                        break;

                    case 2:
                        player.ShieldPowerup();
                        break;

                    default:
                        Debug.Log("No ID detected");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}



