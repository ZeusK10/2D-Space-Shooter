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
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning==false)
        {
            int randomNumber = Random.Range(-9, 10);

            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomNumber, 9, 0), Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;  

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 9.0f));
            int powerupID = Random.Range(0, 3);
            
                Instantiate(powerups[powerupID], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
           
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
