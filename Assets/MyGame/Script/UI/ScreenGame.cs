using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenGame : BaseScreen
{
    public float timeLeft;
    public TextMeshProUGUI time;
    public override void Init()
    {
        base.Init();
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);
        }
        else
        {
            timeLeft = 0;
        }
    }
    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes=Mathf.FloorToInt(currentTime/60);
        float seconds=Mathf.FloorToInt(currentTime%60);
        time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
