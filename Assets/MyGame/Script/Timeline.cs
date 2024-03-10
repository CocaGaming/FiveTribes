using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public bool isClicked;
    public void OnClickSkipButton()
    {
        if (isClicked == false)//đặt dk để nút skip ko bị bấm nhiều lần sẽ làm load scene game nhiều lần
        {
            isClicked = true;
            GameManager.Instance.LoadSceneGame();
            this.gameObject.SetActive(false);
        }
    }
}
