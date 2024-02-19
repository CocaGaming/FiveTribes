using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ArenaBGM : MonoBehaviour
{
    public TextMeshProUGUI arenaName;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInputManager>() != null)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayBGM(AUDIO.BGM_ARENA);
            }   
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInputManager>() != null)
        {
            StartCoroutine(Fade(3, 3));
        }
    }
    private void SetAlpha(float alp)
    {
        Color cl = this.arenaName.color;
        cl.a = alp;
        this.arenaName.color = cl;
    }
    public IEnumerator Fade(float fadeInTime, float fadeOutTime)
    {
        SetAlpha(0);
        Sequence seq = DOTween.Sequence();
        seq.Append(this.arenaName.DOFade(1f, fadeInTime));
        yield return new WaitForSeconds(5f);
        seq.Append(this.arenaName.DOFade(0f, fadeOutTime));
    }
}
