using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _speed = 8;
    private void Start()
    {
       // transform.position = new Vector3(transform.position.x, transform.position.y+0.8f, 0);
    }
    void Update()
    {
        
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y>8.0)
        {
            Destroy(gameObject);
        }
    }
}
