using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while(_stopSpawning==false)
        {
            float randomNumber = Random.Range(-9.0f, 9.0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomNumber, 9, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;  
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
