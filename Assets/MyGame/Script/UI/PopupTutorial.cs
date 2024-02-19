using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : BasePopup
{
    public TextMeshProUGUI tutorialText;
    public Image tutorialBoard;
    public override void Init()
    {
        base.Init();
        StartCoroutine(FadeText(3, 3));
        StartCoroutine(FadeImage(3,3 ));
    }
    public override void Show(object data)
    {
        base.Show(data);
        StartCoroutine(FadeText(3, 3));
        StartCoroutine(FadeImage(3, 3));
    }
    public override void Hide()
    {
        base.Hide();
    }

    private void SetAlpha(float alp)
    {
        Color cl = this.tutorialText.color;
        cl.a = alp;
        this.tutorialText.color = cl;

        Color cl1 = this.tutorialBoard.color;
        cl1.a = alp;
        this.tutorialBoard.color = cl1;
    }
    public IEnumerator FadeText(float fadeInTime, float fadeOutTime)
    {
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.tutorialText.DOFade(1f, fadeInTime));
        yield return new WaitForSeconds(5f);
        seq.Append(this.tutorialText.DOFade(0f, fadeOutTime));
    }
    public IEnumerator FadeImage(float fadeInTime, float fadeOutTime)
    {
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.tutorialBoard.DOFade(1f, fadeInTime));
        yield return new WaitForSeconds(5f);
        seq.Append(this.tutorialBoard.DOFade(0f, fadeOutTime));
    }
}
