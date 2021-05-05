using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int powerupID;

    private AudioSource _powerupMusic;

    private void Start()
    {
        _powerupMusic = GameObject.Find("Powerup_Music").GetComponent<AudioSource>();
        if (_powerupMusic == null)
        {
            Debug.LogError("Powerup Music is null");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * 3f * Time.deltaTime);
        if(transform.position.y<-7f)
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
                        player.ShieldPowerup();
                        break;

                    case 2:
                        player.AmmoPowerup();
                        break;

                    case 3:
                        player.HealthPowerup();
                        break;

                    case 4:
                        player.DestructivePowerup();
                        break;

                    case 5:
                        player.NegativePowerup();
                        break;

                    default:
                        Debug.Log("No ID detected");
                        break;
                }
                _powerupMusic.Play();
            }
            Destroy(gameObject);
        }
    }
}



