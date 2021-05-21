using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Laser_Pos : MonoBehaviour
{
    private GameObject _enemy;

    private void Start()
    {
        _enemy = GameObject.Find("Main_Boss");
        if(_enemy!=null)
        {
            transform.position = _enemy.transform.position;
        }
        
    }


    IEnumerator ScaleRoutine()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(8.0f, 35f, 1f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 2);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= 2);

        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            StartCoroutine(ScaleRoutine());
        }
    }
}
