using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject leaderboard;
    public GameObject gameOverMenu;
    
    public TextMeshProUGUI scoreText;

    private GameManager gameManager;

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PlayGame()
    {
        gameManager.gameState = GameManager.GameState.PLAYING;

        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameManager.ResetSun();
        ResetScore();
        scoreText.gameObject.SetActive(true);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowLeaderboard()
    {
        startMenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ResetScore()
    {
        Score = 0;
        scoreText.text = "Score: " + Score;
    }

    public void UpdateScore(int amount)
    {
        Score += amount;
        scoreText.text = "Score: " + Score;
    }
}
