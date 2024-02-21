using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    private float bgmFadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH;
    private string nextBGMName;
    private string nextSEName;
    private string nextLongSEName;
    private bool isFadeOut = false;//ktra nhạc chuyển nhỏ dần thì là true, nhanh là false
    public AudioSource AttachBGMSource;
    public AudioSource AttachSESource;
    public AudioSource AttachLongSESource;
    private Dictionary<string, AudioClip> bgmDic, seDic, longSEDic;//ví dụ tương ứng string BGM_01 thì sẽ gọi tới clip đó
    protected override void Awake()
    {
        base.Awake();
        bgmDic = new Dictionary<string, AudioClip>();//khởi tạo dictionary
        seDic = new Dictionary<string, AudioClip>();
        longSEDic = new Dictionary<string, AudioClip>();
        object[] bgmList = Resources.LoadAll("Audio/BGM");//tạo folder Resources trong Assets>>Audio>>BGM/SE để load theo đường dẫn
        object[] seList = Resources.LoadAll("Audio/SE");
        object[] longSEList = Resources.LoadAll("Audio/LONGSE");
        foreach (AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;//kiểu key-value, ứng với string BGM_01 có AudioClip BGM_01;
        }
        foreach (AudioClip se in seList)
        {
            seDic[se.name] = se;
        }
        foreach (AudioClip longSE in longSEList)
        {
            longSEDic[longSE.name] = longSE;
        }
    }
    private void Start()
    {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);//nếu get key ko dc thì sẽ get gtri default
        AttachSESource.volume = PlayerPrefs.GetFloat(CONST.SE_VOLUME_KEY, CONST.SE_VOLUME_DEFAULT);
        AttachLongSESource.volume = PlayerPrefs.GetFloat(CONST.LONGSE_VOLUME_KEY, CONST.LONGSE_VOLUME_DEFAULT);
    }
    public void PlayLongSE(string longSEName, float delay = 0.0f)
    {
        if (!longSEDic.ContainsKey(longSEName))//thêm dấu ! nghĩa là ngược lại, là ko có
        {
            Debug.LogError(longSEName + "There is no longSE named");
            return;
        }
        nextLongSEName = longSEName;
        Invoke("DelayPlayLongSE", delay);//gọi 1 hàm nào đó, delay 1 khoảng tgian 
    }
    private void DelayPlayLongSE()
    {
        AttachLongSESource.PlayOneShot(longSEDic[nextLongSEName] as AudioClip);
    }
    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!seDic.ContainsKey(seName))//thêm dấu ! nghĩa là ngược lại, là ko có
        {
            Debug.LogError(seName + "There is no SE named");
            return;
        }
        nextSEName = seName;
        Invoke("DelayPlaySE", delay);//gọi 1 hàm nào đó, delay 1 khoảng tgian 
    }
    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(seDic[nextSEName] as AudioClip);
    }
    public void PlayBGM(string bgmName, float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!bgmDic.ContainsKey(bgmName))
        {
            Debug.LogError(bgmName + "There is no BGM named");
            return;
        }
        //BGM is not currently playing
        if (!AttachBGMSource.isPlaying)//thêm ! nghĩa là false
        {
            nextBGMName = "";
            AttachBGMSource.clip = bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //BGM is playing
        else if (AttachBGMSource.clip.name != bgmName)
        {
            nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }
    private void FadeOutBGM(float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_LOW)
    {
        bgmFadeSpeedRate = fadeSpeedRate;
        isFadeOut = true;
    }
    private void Update()
    {
        if (!isFadeOut)
        {
            return;
        }
        AttachBGMSource.volume -= Time.deltaTime * bgmFadeSpeedRate;// nhạc giảm dần về 0
        if (AttachBGMSource.volume <= 0)//nếu nhạc tắt
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);//về lại gtri volume mới để play nhạc mới
            isFadeOut = false;
            if (!string.IsNullOrEmpty(nextBGMName))
            {
                PlayBGM(nextBGMName);
            }
        }
    }
    public void ChangeBGMVolume(float BGMVolume)//chỉnh volume cho BGM rồi lưu lại cho lần mở app kế
    {
        AttachBGMSource.volume = BGMVolume;
        //PlayerPrefs.SetFloat(CONST.BGM_VOLUME_KEY, BGMVolume);
    }
    public void SetCacheBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(CONST.BGM_VOLUME_KEY, volume);
    }
    public void ChangeSEVolume(float SEVolume)
    {
        AttachSESource.volume = SEVolume;
        //PlayerPrefs.SetFloat(CONST.SE_VOLUME_KEY, SEVolume);
    }
    public void SetCacheSEVolume(float volume)
    {
        PlayerPrefs.SetFloat(CONST.SE_VOLUME_KEY, volume);
    }
    public void ChangeLongSEVolume(float longSEVolume)
    {
        AttachLongSESource.volume = longSEVolume;
    }
    public void SetCacheLongSEVolume(float volume)
    {
        PlayerPrefs.SetFloat(CONST.LONGSE_VOLUME_KEY, volume);
    }
}
