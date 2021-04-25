using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;

    private bool _stopSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning==false)
        {
            float randomNumber = Random.Range(-9.0f, 9.0f);

            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomNumber, 9, 0), Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;  

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f,9.0f));
            Instantiate(_tripleShotPowerupPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
