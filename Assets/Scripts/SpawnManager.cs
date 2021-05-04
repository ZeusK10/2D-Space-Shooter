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

    [SerializeField]
    private UIManager _waveText;
    private int _waveNumber = 1;
    
    [SerializeField]
    private float _waveTime;
    [SerializeField]
    private float _spawnWaitTime=5f;

    private bool _stopSpawning = false;


    private void Start()
    {
        
    }
    public void StartSpawning()
    {
        
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnAmmoRoutine());
        StartCoroutine(SpawnHealthRoutine());
        StartCoroutine(SpawnDestructiveLaserRoutine());
    }

    IEnumerator SpawnDestructiveLaserRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(45.0f, 65.0f));
            Instantiate(powerups[4], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning==false)
        {
            int rand = Random.Range(0, 3);
            if (rand==0)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9, 10), 9, 0), Quaternion.identity);

                newEnemy.transform.parent = _enemyContainer.transform;
                

            }
            else if(rand==1)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(12, Random.Range(0f, 5f), 0), Quaternion.identity);

                newEnemy.transform.parent = _enemyContainer.transform;
            }
            else if(rand==2)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(-12, Random.Range(0f, 5f), 0), Quaternion.identity);

                newEnemy.transform.parent = _enemyContainer.transform;
            }

            _waveTime++;
            if(_waveTime/5==_waveNumber*5)
            {
                _spawnWaitTime -= 0.5f;
                _waveText.UpdateWave(_waveNumber);
                _waveNumber++;
                
            }
            yield return new WaitForSeconds(_spawnWaitTime);
        }
    }
    //I made changes

    IEnumerator SpawnAmmoRoutine()
    {
        while(_stopSpawning==false)
        {
            yield return new WaitForSeconds(Random.Range(15.0f, 25.0f));
            Instantiate(powerups[2], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
        }
    }

    IEnumerator SpawnHealthRoutine()
    {
        while(_stopSpawning==false)
        {
            yield return new WaitForSeconds(Random.Range(30.0f, 45.0f));
            Instantiate(powerups[3], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 9.0f));
            int powerupID = Random.Range(0, 2);
            Instantiate(powerups[powerupID], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        
    }

}
