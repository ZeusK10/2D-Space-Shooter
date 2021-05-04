using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Bar : MonoBehaviour
{

    private float _maxSpeedTime = 100f;
    private float _currentSpeedTime = 100f;
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Player is null");
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
