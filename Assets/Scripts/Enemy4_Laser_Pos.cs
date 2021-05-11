using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_Laser_Pos : MonoBehaviour
{

    private GameObject _enemy;

    void Update()
    {
        transform.position = _enemy.transform.position;
    }

    IEnumerator ScaleRoutine()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(5.0f, 50f, 1f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 2);
            currentTime += Time.deltaTime;
            yield return null;
        }while(currentTime <= 2);

        Destroy(gameObject,2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy4")
        {
            _enemy = other.gameObject;
            StartCoroutine(ScaleRoutine());
        }
    }
}
