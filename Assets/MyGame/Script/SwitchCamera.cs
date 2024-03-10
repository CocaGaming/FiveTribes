using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCamera : BaseManager<SwitchCamera>
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject camera4;
    public GameObject camera5;

    public bool isClicked;

    private void Start()
    {
        isClicked = false;
        StartCoroutine(Switch());
    }
    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(3f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowOverlap<OverlapFade>();
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            if (overlapFade != null)
            {
                overlapFade.Fade(3f);
            }
        }
        yield return new WaitForSeconds(3f);
        camera1.SetActive(false);
        camera2.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowOverlap<OverlapFade>();
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            if (overlapFade != null)
            {
                overlapFade.Fade(3f);
            }
        }
        yield return new WaitForSeconds(3f);
        camera2.SetActive(false);
        camera3.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowOverlap<OverlapFade>();
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            if (overlapFade != null)
            {
                overlapFade.Fade(3f);
            }
        }
        yield return new WaitForSeconds(3f);
        camera3.SetActive(false);
        camera4.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowOverlap<OverlapFade>();
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            if (overlapFade != null)
            {
                overlapFade.Fade(3f);
            }
        }
        yield return new WaitForSeconds(3f);
        camera4.SetActive(false);
        camera5.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.LoadSceneGame();
    }
    public void OnClickSkipButton()
    {
        if (isClicked == false)//đặt dk để nút skip ko bị bấm nhiều lần sẽ làm load scene game nhiều lần
        {
            isClicked = true;
            StopCoroutine(Switch());
            GameManager.Instance.LoadSceneGame();
            this.gameObject.SetActive(false);
        } 
    }
}
