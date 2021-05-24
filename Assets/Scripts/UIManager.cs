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
    private GameObject _winnerText;

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
    private Text _levelText;

    [SerializeField]
    private Text _missileText;
   
    void Start()
    {
        _scoreText.text = "Score: 0";
        _ammoText.text = "30/30";
        _waveText.text = "";
        _missileText.text = "0/3";
        _levelText.text = "Level 1";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            Time.timeScale = 1.0f;
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
        _missileText.text = missilecount+"/3";
    }

    public void UpdatePlayerAmmo(int ammo)
    {
        _ammoText.text = ammo+"/30";
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
        _waveText.text = "Level " + (waveNumber+1);
        _levelText.text = "Level " + (waveNumber + 1);
        StartCoroutine(WaveRoutine());
    }

    public void GameWon()
    {
        _winnerText.SetActive(true);
        Time.timeScale = 0.0f;
        _restartMessage.SetActive(true);
        _isGameOver = true;
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
