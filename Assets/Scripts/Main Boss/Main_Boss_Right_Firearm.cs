using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Boss_Right_Firearm : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private SpawnManager player;
    private Player _playerScript;

    [SerializeField]
    private GameObject intPos;
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private int _armHealth = 10;

    private bool test;

    void Start()
    {
        
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.Log("Player is null");
        }

        _playerScript = GameObject.Find("Player").GetComponent<Player>();
        if(_playerScript==null)
        {
            Debug.LogError("Player script not found");
        }

        player = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(player==null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        StartCoroutine(FireBulletRoutine());
    }

    void Update()
    {
        test = player._isPlayerAlive;
        if (test == true && _player.transform.position.x > 0)
        {
            RotateTowardsPlayer();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (_armHealth < 1)
        {
            Destroy(gameObject);
        }
    }

    void RotateTowardsPlayer()
    {

        Vector3 offset = transform.position - _player.transform.position;

        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), offset);
    }

    IEnumerator FireBulletRoutine()
    {
        yield return new WaitForSeconds(2);
        while (player._isPlayerAlive)
        {
            GameObject blt = Instantiate(_bulletPrefab, intPos.transform.position, transform.rotation);
            
            yield return new WaitForSeconds(Random.Range(1,4));
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _playerScript.UpdateScore(25);
            _armHealth -= 1;
            Destroy(other.gameObject);
        }
    }
}
