using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    GameManager gameManager;
    public float speed = 5.0f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameState == GameManager.GameState.PLAYING)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * speed);
            transform.LookAt(Vector3.zero);
        }
    }
}
