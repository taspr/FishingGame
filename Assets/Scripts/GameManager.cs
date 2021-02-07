using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    public GameState gameState = GameState.PREGAME;

    public GameObject fish;
    public GameObject fishIndicator;
    public GameObject fishingLine;

    private Sun sun;
    private UIManager uIManager;
    private FishBucket fishBucket;
    private Fish fishScript;

    public int fishingLines = 3;
    public int fishRarity;
    public float fishingLineBreakTime = 2.0f;
    public float fishIndicatorMinTime = 0.5f;
    public float fishIndicatorMaxTime = 2.0f;

    public enum GameState
    {
        PREGAME,
        PLAYING,
        PAUSED
    }

    private void Start()
    {
        fishBucket = GameObject.Find("Bucket").GetComponent<FishBucket>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sun = GameObject.Find("Sun").GetComponent<Sun>();

        uIManager.ResetScore();
    }

    private void Update()
    {
        if (sun.transform.eulerAngles.x > 340.0f && sun.transform.eulerAngles.x < 350.0f)
        {
            gameState = GameState.PAUSED;
            uIManager.gameOverMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && gameState == GameState.PLAYING)
        {
            if (fish == null)
            {
                // make bigger fish appear less
                fishingLine.SetActive(true);

                fish = fishes[SelectFish()];
                fishScript = fish.GetComponent<Fish>();
                fish.transform.position = new Vector3(-0.57f, 1.36f, 0f); // fix numbers

                StartCoroutine(ShowFishIndicator());
            }
            else if (fishBucket.Fish == null && fishIndicator.activeSelf == true)
            {
                StopAllCoroutines();
                //StopCoroutine(BreakFishingLine());

                fishBucket.Fish = fish;
                fish.SetActive(true);
                fishIndicator.SetActive(false);

                StartCoroutine(BreakFishingLine((fishes.Count - fishScript.size) / 2.0f)); //fix numbers
            }
        }

        if (fishBucket.inBucket)
        {
            uIManager.UpdateScore(fishScript.size + 1);
            ResetFishing();
        }
    }

    private IEnumerator ShowFishIndicator()
    {
        fishingLineBreakTime = fishes.Count - fishScript.size;
        float fishIndicatorTime = Random.Range(fishIndicatorMinTime, fishIndicatorMaxTime);

        yield return new WaitForSeconds(fishIndicatorTime);

        fishIndicator.transform.localScale = new Vector3((fishScript.size + 1) / 2f, 0.01f, (fishScript.size + 1) / 2f); // fix numbers
        fishIndicator.SetActive(true);

        StartCoroutine(BreakFishingLine(fishingLineBreakTime));
    }

    private IEnumerator BreakFishingLine(float breakTime)
    {
        yield return new WaitForSeconds(breakTime);

        fishingLines--;
        ResetFishing();
    }

    private void ResetFishing()
    {
        StopAllCoroutines();

        fishingLine.SetActive(false);
        fishIndicator.SetActive(false);

        if (fish != null)
        {
            fish.SetActive(false);
        }
        fish = null;

        fishBucket.Fish = null;
        fishBucket.inBucket = false;
    }

    public void ResetSun()
    {
        sun.ResetSun();
    }

    private int SelectFish()
    {
        int selectedFish = -1;

        fishRarity = Random.Range(0, 101);

        if (fishRarity <= 42)
        {
            selectedFish = 0;
        }
        else if (fishRarity >= 43 && fishRarity <= 72)
        {
            selectedFish = 1;
        }
        else if (fishRarity >= 73 && fishRarity <= 88)
        {
            selectedFish = 2;
        }
        else if (fishRarity >= 89 && fishRarity <= 96)
        {
            selectedFish = 3;
        }
        else if (fishRarity >= 97)
        {
            selectedFish = 4;
        }

        return selectedFish;
    }
}