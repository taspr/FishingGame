using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float speed = 5.0f;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * speed);
        transform.LookAt(Vector3.zero);
    }
}
