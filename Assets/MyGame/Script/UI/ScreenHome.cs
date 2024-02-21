using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHome : BaseScreen
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
    public void OnClickSettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
    }
    public void OnClickStartButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenTutorial>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        Hide();
    }
    public void OnClickQuitButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupQuit>();
        }
    }
}
