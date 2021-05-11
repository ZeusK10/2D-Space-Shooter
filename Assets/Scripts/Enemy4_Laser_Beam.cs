using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_Laser_Beam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
        }
    }

}

