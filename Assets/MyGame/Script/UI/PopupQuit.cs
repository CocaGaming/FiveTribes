using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupQuit : BasePopup
{
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
    public void OnYesButton()
    {
        Application.Quit();
    }
    public void OnNoButton()
    {
        Time.timeScale = 1f;
        Hide();
    }
}
