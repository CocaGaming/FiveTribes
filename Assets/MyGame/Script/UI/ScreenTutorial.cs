using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTutorial : BaseScreen
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
    public void OnClickBackButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
        if (AudioManager.HasInstance)
        Hide();
    }
    public void OnClickNextButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenTribeSelect>();
        }
        Hide();
    }
}
