using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _isShaking;
    private bool _gameOver;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if(_isShaking==true && _gameOver==false)
        {
            StartCoroutine(ShakingRoutine());
        }
        else if(_isShaking==false)
        {
            transform.position = _initialPosition;
        }
    }


    IEnumerator ShakingRoutine()
    {
        while(_isShaking)
        {
            transform.position = _initialPosition;
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector3(transform.position.x + 0.03f, transform.position.y + 0.03f, transform.position.z);
            yield return new WaitForSeconds(0.5f);
            transform.position = _initialPosition;
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector3(transform.position.x - 0.03f, transform.position.y - 0.03f, transform.position.z);
            

        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }

    public void Shake()
    {
        _isShaking = true;
        StartCoroutine(ShakeTimeRoutine());
    }

    IEnumerator ShakeTimeRoutine()
    {
        yield return new WaitForSeconds(2);
        _isShaking = false;
    }
}
