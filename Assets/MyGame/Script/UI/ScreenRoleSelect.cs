using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRoleSelect : BaseScreen
{
    public RawImage chieftainPreview;
    public RawImage villagerPreview;

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
        GameObject[] preview = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject go in preview)
        {
            Destroy(go);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenTribeSelect>();
        }
        Hide();
    }
    public void OnClickHomeButton()
    {
        GameObject[] preview = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject go in preview)
        {
            Destroy(go);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
        Hide();
    }
    public void OnClickChieftainButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.roleType = RoleType.CHIEFTAIN;
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoadingGame>();
        }
        Hide();
    }
    public void OnClickVillagerButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.roleType = RoleType.VILLAGER;
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoadingGame>();
        }
        Hide();
    }
}