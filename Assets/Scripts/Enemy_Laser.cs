using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    int dir=-1;
    private bool _powerupDetected;
    void Update()
    {
        transform.Translate(new Vector3(0,dir,0) * _speed * Time.deltaTime);
        Destroy(gameObject,4);
    }

    public void FireUp()
    {
        dir = 1;
    }

    public void FireDown()
    {
        dir = -1;
        _powerupDetected = true;
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
            Destroy(gameObject);
        }
        else if(other.tag=="Powerup" && _powerupDetected==true)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }

}
