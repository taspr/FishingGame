using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject fishIndicator;
    public GameObject fish;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowFishIndicator());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (fishIndicator.activeSelf)
                fish.SetActive(true);

            if (fishIndicator.activeSelf)
                fishIndicator.SetActive(false);

            
        }    
    }

    IEnumerator ShowFishIndicator()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            if(!fishIndicator.activeSelf)
                fishIndicator.SetActive(true);
        }
        
    }
}
