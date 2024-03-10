using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OverlapFade1 : BaseOverlap
{
    [SerializeField]
    private Image imgFade;
    [SerializeField]
    private Color fadeColor;
    public override void Init()
    {
        base.Init();
        Fade(3, OnFinish);
    }
    public override void Show(object data)
    {
        base.Show(data);
        Fade(3, OnFinish);
    }
    public override void Hide()
    {
        base.Hide();
    }
    private void SetAlpha(float alp)
    {
        Color cl=this.imgFade.color;
        cl.a = alp;
        this.imgFade.color = cl;
    }
    public void Fade(float fadeTime, Action onFinish)
    {
        imgFade.color = fadeColor;
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.imgFade.DOFade(1f, fadeTime));
        seq.Append(this.imgFade.DOFade(0, fadeTime));
        seq.OnComplete(() =>
        {
            onFinish?.Invoke();
        });
    }
    private void OnFinish()
    {
        this.Hide();
    }
}
