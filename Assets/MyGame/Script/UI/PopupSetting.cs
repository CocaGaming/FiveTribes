using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopup
{
    public Slider bgmSlider;
    public Slider seSlider;
    private float bgmValue;
    private float seValue;

    private void Awake()
    {
        if (AudioManager.HasInstance)
        {
            bgmValue = AudioManager.Instance.AttachBGMSource.volume;
            seValue=AudioManager.Instance.AttachSESource.volume;
            bgmSlider.value= bgmValue;
            seSlider.value= seValue;
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
    }
    public override void Hide()
    {
        base.Hide();
    }
    public void OnClickQuitButton()
    {
        Hide();
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
}
