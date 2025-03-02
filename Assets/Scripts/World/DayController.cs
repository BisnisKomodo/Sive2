using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public float gameTimer = 0f;
    public int dayIndicator = 1;
    public float cyclingSpeed = 1f; 

    private void Start()
    {
        gameTimer = Mathf.Clamp(gameTimer, 0, 360);
    }

    void Update()
    {
        TimerStart();
    }

    private void TimerStart()
    {
        gameTimer += Time.deltaTime * cyclingSpeed;
        if (gameTimer >= 360)
        {
            gameTimer = 0;
            dayIndicator++;
            Debug.Log("Day right now : " + dayIndicator);
        }
    }
}
