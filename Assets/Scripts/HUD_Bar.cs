using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Bar : MonoBehaviour
{

    private float _maxSpeedTime = 100f;
    private float _currentSpeedTime = 100f;
    private Player player;
    private GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player=playerObj.GetComponent<Player>();
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(1.0f, _currentSpeedTime / _maxSpeedTime, 1.0f);
        if(_currentSpeedTime<=_maxSpeedTime)
        {
            _currentSpeedTime += 0.05f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _currentSpeedTime > 0.0f)
        {
            _currentSpeedTime -= 0.5f;
            player.SpeedPowerup();

        }
        else if (_currentSpeedTime <= 0f)
        {
            player.SpeedPowerDown();
        }

    }


}
