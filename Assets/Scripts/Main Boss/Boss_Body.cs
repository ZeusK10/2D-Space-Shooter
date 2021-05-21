using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Body : MonoBehaviour
{
    [SerializeField]
    private int health = 20;


    [SerializeField]
    private GameObject _laserPrefab;

    private UIManager _uiManager;
    private Player player;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager==null)
        {
            Debug.LogError("UIManager is empty");
        }

        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }

        StartCoroutine(StartlaserRoutine());
    }
    void Update()
    {
        if(health<1)
        {
            _uiManager.GameWon();
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Laser")
        {
            player.UpdateScore(50);
            health -= 1;
            Destroy(other.gameObject);
        }
    }
    IEnumerator StartlaserRoutine()
    {
        yield return new WaitForSeconds(3);
        while (player._isPlayerAlive)
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + (-1), 0), Quaternion.identity);

            yield return new WaitForSeconds(7);
        }
    }
}
