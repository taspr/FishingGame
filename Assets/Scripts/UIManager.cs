using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject leaderboard;
    public GameObject gameOverMenu;
    public GameObject gamePlay;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI endScoreText;
    public Image fishImage;
    public List<Image> fishingLines = new List<Image>();

    private GameManager gameManager;
    public int Score { get; set; }

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
        gamePlay.SetActive(true);
        gameManager.fishingLines = 3;

        foreach (Image fishingLine in fishingLines)
        {
            fishingLine.gameObject.SetActive(true);
        }
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

    public void HideFishingLine()
    {
        foreach (Image fishingLine in fishingLines)
        {
            if (fishingLine.gameObject.activeSelf)
            {
                fishingLine.gameObject.SetActive(false);
                break;
            }
        }
    }

}