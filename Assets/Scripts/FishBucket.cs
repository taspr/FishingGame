using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBucket : MonoBehaviour
{
    public GameObject Fish { get; set; }
    public bool inBucket;

    private void OnMouseDown()
    {
        if (Fish != null)
        {
            Fish.SetActive(false);
            inBucket = true;
        }
    }
}
