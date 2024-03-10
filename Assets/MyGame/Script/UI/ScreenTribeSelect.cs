using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject[] preview = GameObject.FindGameObjectsWithTag("FirePreview");
        foreach (GameObject go in preview)
        {
            Destroy(go);
        }

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
    public void OnClickHomeButton()
    {

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        Hide();
    }
    public void OnClickTungusButton()
    {

        if (GameManager.HasInstance)
        {
           GameManager.Instance.tribeType = TribeType.TUNGUS;
        }
        Instantiate(tungusChieftainPrefab);
        Instantiate(tungusVillagerPrefab);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenRoleSelect>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        Hide();
    }
}
