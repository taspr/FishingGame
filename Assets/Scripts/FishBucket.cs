using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBucket : MonoBehaviour
{
    public GameObject fish;

    private void OnMouseDown()
    {
        fish.SetActive(false);
    }
}
