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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
    }
    public void OnClickHomeButton()
    {
        StartCoroutine(LoadHomeScene());
        Hide();
        if(UIManager.HasInstance)
        {
            UIManager.Instance.HideNotify<NotifyShop>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
    }
    public void OnClickQuitButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupQuit>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
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

        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Loading");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if (overlapFade != null)
                    {
                        overlapFade.Fade(3f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                                if (UIManager.HasInstance)
                                {
                                    UIManager.Instance.ShowScreen<ScreenHome>();
                                }

                                if (AudioManager.HasInstance)
                                {
                                    AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
                                }
                            },
                            onFinish: () =>
                            {
                              
                            });
                    }
                }
                yield return new WaitForSeconds(3f);//nghĩa là trong 1s ko làm gì hết
            }
            yield return null;//để thoát ra khỏi vòng while
        }
    }
}
