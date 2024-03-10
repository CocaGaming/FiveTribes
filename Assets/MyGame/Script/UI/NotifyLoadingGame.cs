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
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }
    public override void Hide()
    {
        base.Hide();
    }
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Intro 1");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingPercentText.SetText($"LOADING: {asyncOperation.progress * 100}%");
            if (asyncOperation.progress >= 0.9f)
            {
                
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    loadingSlider.value = 1f;
                    loadingPercentText.SetText($"LOADING: {loadingSlider.value * 100}%");
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if (overlapFade != null)
                    {
                        overlapFade.Fade(3f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                            },
                            onFinish: () =>
                            {

                            });
                    }
                }
                yield return new WaitForSeconds(3f);//nghĩa là trong 1s ko làm gì hết
                //asyncOperation.allowSceneActivation = true;
                this.Hide();
            }
            yield return null;//để thoát ra khỏi vòng while
            
        }
    }
}
