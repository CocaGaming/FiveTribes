using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTribeSelect : BaseScreen
{
    public GameObject tungusChieftainPrefab;
    public GameObject tungusVillagerPrefab;
    public GameObject asianChieftainPrefab;
    public GameObject asianVillagerPrefab;
    public GameObject orcChieftainPrefab;
    public GameObject orcVillagerPrefab;
    public GameObject vikingChieftainPrefab;
    public GameObject vikingVillagerPrefab;

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
            UIManager.Instance.ShowScreen<ScreenTutorial>();
        }
        Hide();
    }
    public void OnClickHomeButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
        Hide();
    }
    public void OnClickTungusButton()
    {
        if(GameManager.HasInstance)
        {
           GameManager.Instance.tribeType = TribeType.TUNGUS;
        }
        Instantiate(tungusChieftainPrefab);
        Instantiate(tungusVillagerPrefab);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenRoleSelect>();
        }
        Hide();
    }
    public void OnClickOrcButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.tribeType = TribeType.ORC;
        }
        Instantiate(orcChieftainPrefab);
        Instantiate(orcVillagerPrefab);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenRoleSelect>();
        }
        Hide();
    }
    public void OnClickVikingButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.tribeType = TribeType.VIKING;
        }
        Instantiate(vikingChieftainPrefab);
        Instantiate(vikingVillagerPrefab);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenRoleSelect>();
        }
        Hide();
    }
    public void OnClickAsianButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.tribeType = TribeType.ASIAN;
        }
        Instantiate(asianChieftainPrefab);
        Instantiate(asianVillagerPrefab);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenRoleSelect>();
        }
        Hide();
    }
}