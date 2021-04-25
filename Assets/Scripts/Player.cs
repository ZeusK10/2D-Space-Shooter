using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private int _Lives = 3;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Set the starting position to the origin
        transform.position = new Vector3(0, 0, 0);

        float randomNumber = Random.Range(-9.0f, 9.1f);
        Instantiate(_enemyPrefab, new Vector3(randomNumber, 9, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
        {
            FireLaser();
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);
        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {   
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _Lives -= 1;

        if(_Lives<1)
        {
            Destroy(this.gameObject);
        }
    }
}



