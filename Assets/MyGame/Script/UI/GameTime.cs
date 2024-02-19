using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance;
    public float intervalTime;
    private float interval;
    public float hour;
    public float minute;
    public TextMeshProUGUI time;

    private void Start()
    {
        Instance = this;
        interval = intervalTime;
        hour = 6f;
        minute = 0f;
        time.text = hour.ToString("00") + ":" + minute.ToString("00");
    }
    private void Update()
    {
        interval -= Time.deltaTime;
        if (interval <= 0)
        {
            minute += 12f;
            interval = intervalTime;
            UpdateTimer(hour, minute);
            if (minute == 60)
            {
                hour++;
                minute = 0;
                UpdateTimer(hour, minute);
            }
            if (hour == 24)
            {
                hour = 0;
                minute = 0;
                UpdateTimer(hour, minute);
            }
        }
    }
    private void UpdateTimer(float hour, float minute)
    {
        time.text = hour.ToString("00") + ":" + minute.ToString("00");
    }
}
