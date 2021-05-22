using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _uniqueEnemyPrefab;

    [SerializeField]
    private GameObject _enemy4Prefab;
    [SerializeField]
    private GameObject _enemy5Prefab;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemy5Container;

    [SerializeField]
    private GameObject _moveTowardsEnemyPrefab;

    private float _moveTowardsEnemyWaitTime;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private UIManager _waveText;
    [SerializeField]
    private int _waveNumber = 1;
    
    [SerializeField]
    private float _waveTime;
    [SerializeField]
    private float _spawnWaitTime=5f;

    private bool _stopSpawning = false;
    public bool _isPlayerAlive = true;

    private Enemy _enemy;

    [SerializeField]
    private GameObject sideBossEnemyPrefab;
    [SerializeField]
    private GameObject _mainBossPrefab;
    private int a = 0;

    private void Start()
    {
        _moveTowardsEnemyWaitTime = Random.Range(45f, 55f);
    }

    
    public void StartSpawning()
    {
        
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnAmmoRoutine());
        StartCoroutine(SpawnHealthRoutine());
        StartCoroutine(SpawnDestructiveLaserRoutine());
        StartCoroutine(SpawnMoveTowardsEnemyRoutine());
        StartCoroutine(SpawnUniqueEnemyRoutine());
        StartCoroutine(SpawnEnemy4Routine());
        StartCoroutine(SpawnEnemy5Routine());
        StartCoroutine(SpawnProjectilePowerupRoutine());
    }

    IEnumerator SpawnEnemy4Routine()
    {
        yield return new WaitForSeconds(75);
        while(_stopSpawning==false)
        {
            GameObject _enemy4 = Instantiate(_enemy4Prefab, new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            _enemy4.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(75f,150f));
        }
    }


    IEnumerator SpawnProjectilePowerupRoutine()
    {
        yield return new WaitForSeconds(100);
        while (_stopSpawning == false)
        {
            Instantiate(powerups[6], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(100, 200));
        }
    }
        IEnumerator SpawnEnemy5Routine()
        {
            yield return new WaitForSeconds(50);
            while (_stopSpawning == false)
            {
                GameObject _enemy5 = Instantiate(_enemy5Prefab, new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
                _enemy5.transform.parent = _enemy5Container.transform;
            
                yield return new WaitForSeconds(Random.Range(50f, 100f));
            }
        }

    IEnumerator SpawnUniqueEnemyRoutine()
    {
        yield return new WaitForSeconds(25);
        while (_stopSpawning == false)
        {
            
            GameObject newEnemy = Instantiate(_uniqueEnemyPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            _enemy = newEnemy.GetComponent<Enemy>();
            _enemy.EnableUniqueMovement();
            yield return new WaitForSeconds(Random.Range(25,40));
        }
        
    }

    IEnumerator SpawnDestructiveLaserRoutine()
    {
        yield return new WaitForSeconds(Random.Range(45.0f, 65.0f));
        while (_stopSpawning == false)
        {
            int rnd = Random.Range(45, 65);
            Instantiate(powerups[4], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            yield return new WaitForSeconds(rnd);
        }
    }

    IEnumerator SpawnMoveTowardsEnemyRoutine()
    {
        yield return new WaitForSeconds(_moveTowardsEnemyWaitTime);
        while (_stopSpawning == false)
        {
            Instantiate(_moveTowardsEnemyPrefab, new Vector3(Random.Range(-9, 10), 9, 0), Quaternion.identity);
            yield return new WaitForSeconds(_moveTowardsEnemyWaitTime);
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

            if(_waveNumber==5)
            {
                StartFinalBossFight();
            }

            if(_waveNumber==4 && a==0)
            {
                StartCoroutine(StartBossFightRoutine());
            }
            yield return new WaitForSeconds(_spawnWaitTime);
        }
    }

    IEnumerator SpawnAmmoRoutine()
    {
        yield return new WaitForSeconds(Random.Range(15.0f, 25.0f));
        while (_stopSpawning==false)
        {
            Instantiate(powerups[2], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20.0f, 35.0f));
        }
    }

    IEnumerator SpawnHealthRoutine()
    {
        yield return new WaitForSeconds(Random.Range(30.0f, 45.0f));
        while (_stopSpawning==false)
        {
            
            if(Random.Range(0,2)==0)
            {
                Instantiate(powerups[3], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(powerups[5], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(30.0f, 45.0f));
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(9f);
        while (_stopSpawning == false)
        { 
            if(Random.Range(0,5)==0)
            {
                Instantiate(powerups[1], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(powerups[0], new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(5.0f, 9.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        _isPlayerAlive = false;
    }

    private void StartFinalBossFight()
    {
        _stopSpawning = true;
        Instantiate(_mainBossPrefab, new Vector3(0, 9, 0), Quaternion.identity);
    }

    IEnumerator StartBossFightRoutine()
    {
        a = 1;
        yield return new WaitForSeconds(3);
        while(_stopSpawning==false)
        {
            Instantiate(sideBossEnemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
