using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    GameManager gameManager;

    public float speed = 5.0f;

    public Quaternion sunPrevRotation;
    public Vector3 sunPrevPosition;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        sunPrevRotation = transform.rotation;
        sunPrevPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.PLAYING)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * speed);
            transform.LookAt(Vector3.zero);
        }
    }

    public void ResetSun()
    {
        transform.rotation = sunPrevRotation;
        transform.position = sunPrevPosition;
    }
}
