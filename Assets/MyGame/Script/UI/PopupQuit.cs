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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        Application.Quit();
    }
    public void OnNoButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        Time.timeScale = 1f;
        Hide();
    }
}
