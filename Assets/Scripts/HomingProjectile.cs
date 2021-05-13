using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{

    private GameObject _enemy;
    private int _speed = 10;
    void Start()
    {
        _enemy = FindClosestEnemy();
    }

    void Update()
    {
        if(_enemy==null)
        {
            _enemy = FindClosestEnemy();
        }
        if(_enemy!=null)
        {
            MoveTowardsEnemy();
        }
        else
        {
            transform.Translate(Vector3.up * (_speed / 2) * Time.deltaTime);
        }
        if(transform.position.y>15)
        {
            Destroy(gameObject);
        }
        
    }

    private void MoveTowardsEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
        Vector3 offset = transform.position - _enemy.transform.position;

        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), offset);

        if (Vector3.Distance(transform.position, _enemy.transform.position) < 0.001f)
        {
            _enemy.transform.position *= -1.0f;
        }
    }

    private GameObject FindClosestEnemy()
    {
        try
        {
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject enemy in enemies)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
            return closest;
        }
        catch
        {
            return null;
        }
        
    }

}
