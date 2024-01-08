using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class NotifyLoading : BaseNotify
{
    public TextMeshProUGUI tmpLoading;
    public Slider slProgress;
    private string loadingText = "Loading";
    public override void Hide()
    {
        base.Hide();
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public void SetProgress(float value)
    {
        this.slProgress.value = value;
    }
    public void AnimationLoadingText(float dt)
    {
        DOTween.Kill(this.tmpLoading.GetInstanceID().ToString());
        Sequence seq = DOTween.Sequence();
        seq.SetId(this.tmpLoading.GetInstanceID().ToString());
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText;
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + ".";
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + "..";
        });
        seq.AppendInterval(dt / 4f);
        seq.AppendCallback(() =>
        {
            this.tmpLoading.text = loadingText + "...";
        });
        seq.AppendInterval(dt / 4f);
        seq.SetId(-1);
    }

}
