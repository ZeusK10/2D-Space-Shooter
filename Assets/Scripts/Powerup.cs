using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
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
                player.TripleShot();
                
            }
            Destroy(gameObject);
        }
    }
}
