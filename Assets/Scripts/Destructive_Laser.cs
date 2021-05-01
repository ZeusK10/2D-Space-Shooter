using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructive_Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Laser")
        {
            Destroy(other.gameObject);
        }
    }
}
