using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupPause : BasePopup
{
    public PlayerInputManager[] playerList;
    private GameObject gameTime;
    public Slider bgmSlider;
    public Slider seSlider;
    private float bgmValue;
    private float seValue;
    private void Start()
    {
        playerList = FindObjectsOfType<PlayerInputManager>();
        gameTime = GameObject.FindGameObjectWithTag("GameTime");

        if (AudioManager.HasInstance)
        {
            bgmValue = AudioManager.Instance.AttachBGMSource.volume;
            seValue = AudioManager.Instance.AttachSESource.volume;
            bgmSlider.value = bgmValue;
            seSlider.value = seValue;
        }
    }
    private void OnEnable()
    {
        bgmValue = AudioManager.Instance.AttachBGMSource.volume;
        seValue = AudioManager.Instance.AttachSESource.volume;
        bgmSlider.value = bgmValue;
        seSlider.value = seValue;
    }
    public override void Init()
    {
        base.Init();
    }
    public override void Show(object data)
    {
        base.Show(data);
        playerList = FindObjectsOfType<PlayerInputManager>();
        gameTime = GameObject.FindGameObjectWithTag("GameTime");
    }
    public override void Hide()
    {
        base.Hide();
    }
    public void OnClickResumeButton()
    {
        Time.timeScale = 1f;
        Hide();
    }
    public void OnClickHomeButton()
    {
        StartCoroutine(LoadHomeScene());
        Hide();
    }
    public void OnClickQuitButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupQuit>();
        }
    }
    public void OnSliderChangeBGMValue(float v)
    {
        bgmValue = v;
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeBGMVolume(bgmValue);
            AudioManager.Instance.SetCacheBGMVolume(bgmValue);
        }
    }
    public void OnSliderChangeSEValue(float v)
    {
        seValue = v;
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeSEVolume(seValue);
            AudioManager.Instance.SetCacheSEVolume(seValue);
        }
    }
    private IEnumerator LoadHomeScene()
    {
        Time.timeScale = 1f;

        foreach (PlayerInputManager player in playerList)//tắt player để tắt UI thanh máu, chỉ số...
        {
            player.gameObject.SetActive(false);
        }

        gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowOverlap<OverlapFade>();
        }
        yield return new WaitForSeconds(3f);

        //Load lại scene loading
        SceneManager.LoadScene(0);

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
        }
    }
}
