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
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _spriteImage;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private GameObject _restartMessage;
    [SerializeField]
    private bool _isGameOver;
   
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
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

    public void UpdatePlayerLives(int playerLife)
    {
        if(playerLife==0)
        {
            GameOverSequence();

        }
        _spriteImage.sprite = _livesSprite[playerLife];
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
