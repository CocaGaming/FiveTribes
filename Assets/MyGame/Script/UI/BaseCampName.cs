using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCampName : MonoBehaviour
{
    public TextMeshProUGUI baseCampName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInputManager>() != null)
        {
            StartCoroutine(Fade(3, 3));
        }
    }
    private void SetAlpha(float alp)
    {
        Color cl = this.baseCampName.color;
        cl.a = alp;
        this.baseCampName.color = cl;
    }
    public IEnumerator Fade(float fadeInTime, float fadeOutTime)
    {
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.baseCampName.DOFade(1f, fadeInTime));
        yield return new WaitForSeconds(5f);
        seq.Append(this.baseCampName.DOFade(0f, fadeOutTime));
    }
}
