using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroids : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 40.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.2f);
        }
       
    }
}
