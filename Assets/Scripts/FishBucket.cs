using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBucket : MonoBehaviour
{
    GameObject fish;
    public bool inBucket;
    private void OnMouseDown()
    {
        if (GameObject.FindGameObjectsWithTag("Fish") != null)
        {
            fish = GameObject.FindGameObjectWithTag("Fish").gameObject;
        }

        Destroy(fish);
        inBucket = true;
    }
}
