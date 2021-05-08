using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    void Update()
    {
        
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        Destroy(gameObject,4f);

    }
  

private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player = other.GetComponent<Player>();
                if(player!=null)
                {
                    player.Damage();
                }
        }
    }

}
