using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    public GameObject fish;
    public GameObject fishIndicator;
    public GameObject fishingLine;
    public GameObject sun;
    public int fishingLines = 3;
    public float fishingLineBreakTime = 2.0f;
    public float fishIndicatorMinTime = 0.5f;
    public float fishIndicatorMaxTime = 2.0f;
    private FishBucket fishBucket;
    private int fishIndex;

    private enum GameState
    {
        PREGAME,
        PLAYING,
        PAUSED
    }

    private GameState gameState = GameState.PREGAME;

    private void Start()
    {
        fishBucket = GameObject.Find("Bucket").GetComponent<FishBucket>();
        gameState = GameState.PLAYING;
    }

    private void Update()
    {
        if (sun.transform.eulerAngles.x > 340.0f && sun.transform.eulerAngles.x < 350.0f)
        {
            gameState = GameState.PAUSED;
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
}