using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _spriteImage;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private GameObject _restartMessage;
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private Text _waveText;

    [SerializeField]
    private Text _missileText;
   
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _ammoText.text = "15/15";
        _waveText.text = "";
        _missileText.text = "Missiles: 0/3";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void UpdatePlayerScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdatePlayerMissiles(int missilecount)
    {
        _missileText.text = "Missiles: " + missilecount;
    }

    public void UpdatePlayerAmmo(int ammo)
    {
        _ammoText.text = ammo+"/15";
    }

    public void UpdatePlayerLives(int playerLife)
    {
        if(playerLife==0)
        {
            GameOverSequence();

        }
        _spriteImage.sprite = _livesSprite[playerLife];
    }

    public void UpdateWave(int waveNumber)
    {
        _waveText.text = "Wave " + waveNumber;
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        yield return new WaitForSeconds(2);
        _waveText.text = "";
    }
    void GameOverSequence()
    {
        _isGameOver = true;
        _restartMessage.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine()); 
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }   
}
