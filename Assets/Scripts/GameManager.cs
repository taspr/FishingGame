using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    public GameObject fish;
    public GameObject fishIndicator;
    public GameObject fishingLine;
    public GameObject sun;
    public GameObject startMenu;
    public GameObject leaderboard;
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText;
    public int fishingLines = 3;
    public float fishingLineBreakTime = 2.0f;
    public float fishIndicatorMinTime = 0.5f;
    public float fishIndicatorMaxTime = 2.0f;
    private FishBucket fishBucket;
    private int fishIndex;
    private Quaternion sunPrevRotation;
    private Vector3 sunPrevPosition;
    private int score;

    public enum GameState
    {
        PREGAME,
        PLAYING,
        PAUSED
    }

    public GameState gameState = GameState.PREGAME;

    private void Start()
    {
        fishBucket = GameObject.Find("Bucket").GetComponent<FishBucket>();
        sunPrevRotation = sun.transform.rotation;
        sunPrevPosition = sun.transform.position;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if (sun.transform.eulerAngles.x > 340.0f && sun.transform.eulerAngles.x < 350.0f)
        {
            gameState = GameState.PAUSED;
            gameOverMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && gameState == GameState.PLAYING)
        {
            if (fish == null)
            {
                // make bigger fish appear less
                fishingLine.SetActive(true);
                fishIndex = Random.Range(0, fishes.Count);
                fish = fishes[fishIndex];
                fish.transform.position = new Vector3(-0.57f, 1.36f, 0f);

                StartCoroutine(ShowFishIndicator());
            }
            else if (fishBucket.Fish == null && fishIndicator.activeSelf == true)
            {
                StopAllCoroutines();
                //StopCoroutine(BreakFishingLine());
                fishBucket.Fish = fish;
                fish.SetActive(true);
                fishIndicator.SetActive(false);
                StartCoroutine(BreakFishingLine((fishes.Count - fishIndex) / 2.0f));
            }
        }

        if (fishBucket.inBucket)
        {
            score += fishIndex + 1;
            scoreText.text = "Score: " + score;
            StopAllCoroutines();
            fishingLine.SetActive(false);
            fish = null;
            fishBucket.Fish = null;
            fishBucket.inBucket = false;
        }
    }

    private IEnumerator ShowFishIndicator()
    {
        fishingLineBreakTime = fishes.Count - fishIndex;
        float fishIndicatorTime = Random.Range(fishIndicatorMinTime, fishIndicatorMaxTime);
        yield return new WaitForSeconds(fishIndicatorTime);
        fishIndicator.transform.localScale = new Vector3((fishIndex + 1) / 2f, 0.01f, (fishIndex + 1) / 2f);
        fishIndicator.SetActive(true);
        StartCoroutine(BreakFishingLine(fishingLineBreakTime));
    }

    private IEnumerator BreakFishingLine(float breakTime)
    {
        yield return new WaitForSeconds(breakTime);
        fishingLines--;

        if (fish != null)
        {
            fish.SetActive(false);
        }

        fish = null;
        fishBucket.Fish = null;
        fishIndicator.SetActive(false);
        fishingLine.SetActive(false);
    }

    public void PlayGame()
    {
        gameState = GameState.PLAYING;
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        sun.transform.rotation = sunPrevRotation;
        sun.transform.position = sunPrevPosition;
        score = 0;
        scoreText.text = "Score: " + score;
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
}