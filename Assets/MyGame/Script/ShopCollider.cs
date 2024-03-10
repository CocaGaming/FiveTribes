using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    public bool isOnShop;
    public int defencePoint;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.GetComponent<PlayerInputManager>()!=null && !isOnShop)//nếu là player thì mới dc mở shop
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowPopup<PopupShop>();
                UIManager.Instance.HideNotify<NotifyShop>();
            }
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_OPENSHOP);
            }
            defencePoint = other.GetComponent<PlayerAttributes>().defencePoint;
            isOnShop = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInputManager>() != null)
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowNotify<NotifyShop>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInputManager>() != null)
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.HideNotify<NotifyShop>();
            }
        }
    }
}
