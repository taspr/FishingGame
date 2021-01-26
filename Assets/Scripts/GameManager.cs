using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    GameObject fish;
    public GameObject fishIndicator;
    FishBucket fishBucket;
    int fishIndex;

    private void Start()
    {
        fishBucket = GameObject.Find("Bucket").GetComponent<FishBucket>();
        StartCoroutine(ShowFishIndicator());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (fish == null && fishIndicator.activeSelf == true)
            {
                fishIndex = Random.Range(0, fishes.Count);
                fish = fishes[fishIndex];
                Instantiate(fish, new Vector3(0.73f, 1.42f, 0), fish.transform.rotation);
                fishIndicator.SetActive(false);
            }
        }

        if(fishBucket.inBucket)
        {
            fish = null;
            fishBucket.inBucket = false;
        }
    }

    IEnumerator ShowFishIndicator()
    {
        while (true)
        {
            if (fishIndicator.activeSelf == false)
            {
                // size of previous fish instead of current
                fishIndicator.transform.localScale = new Vector3((fishIndex + 1)/2f, 0.01f, (fishIndex + 1)/2f);
                fishIndicator.SetActive(true);
            }
            yield return new WaitForSeconds(3);
        }
    }
}