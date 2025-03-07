using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayController : MonoBehaviour
{
    public float gameTimer = 0f;
    public int dayIndicator = 1;
    public float cyclingSpeed = 1f;
    public Text dayText;

    private void Start()
    {
        gameTimer = Mathf.Clamp(gameTimer, 0, 360);
        UpdateDayText();
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
            UpdateDayText();
        }
    }

    private void UpdateDayText()
    {
        dayText.text = "Day : " + dayIndicator;
    }
}
