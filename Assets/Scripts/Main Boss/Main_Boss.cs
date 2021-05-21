using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Boss : MonoBehaviour
{

    void Update()
    {
        if(transform.position.y>3)
        {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
        }
    }

    
}
