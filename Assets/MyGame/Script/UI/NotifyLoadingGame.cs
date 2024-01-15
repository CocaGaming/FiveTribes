using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class NotifyLoadingGame : BaseNotify
{
    public TextMeshProUGUI loadingPercentText;
    public Slider loadingSlider;
    public override void Init()
    {
        base.Init();
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("FiveTribes");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingPercentText.SetText($"LOADING: {asyncOperation.progress * 100}%");
            if (asyncOperation.progress >= 0.9f)
            {
                loadingSlider.value = 1f;
                loadingPercentText.SetText($"LOADING: {loadingSlider.value * 100}%");
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                }
                yield return new WaitForSeconds(2f);//nghĩa là trong 1s ko làm gì hết
                asyncOperation.allowSceneActivation = true;
                this.Hide();
            }
            yield return null;//để thoát ra khỏi vòng while
        }
    }
}
