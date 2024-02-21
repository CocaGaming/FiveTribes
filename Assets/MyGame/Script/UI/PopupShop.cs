using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShop : BasePopup
{
    private ShopCollider shopCollider;
    private AttributesManagerPlayer attributesManagerPlayer;
    private PlayerInputManager playerInputManager;
    private void Start()
    {
        shopCollider = FindObjectOfType<ShopCollider>();
        attributesManagerPlayer = FindObjectOfType<AttributesManagerPlayer>();
    }
    public override void Init()
    {
        base.Init();
    }
    public override void Show(object data)
    {
        base.Show(data);
        shopCollider = FindObjectOfType<ShopCollider>();
        attributesManagerPlayer = FindObjectOfType<AttributesManagerPlayer>();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public void OnClickBackButton()
    {
        Hide();

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyShop>();
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        ShopCollider shopCollider= FindObjectOfType<ShopCollider>();
        if (shopCollider != null)
        {
            shopCollider.isOnShop = false;
        }
    }
    public void OnBuyAttackPoint()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        if (shopCollider != null && attributesManagerPlayer!=null)
        {
            if (shopCollider.isOnShop && attributesManagerPlayer.goldAmount>=100)
            {
                attributesManagerPlayer.goldAmount -= 100;
                if (attributesManagerPlayer.gameObject.CompareTag("Viking"))
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerInputManager>().attackDamage += 2;//thế mạnh nghề nghiệp của Viking
                }
                else
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerInputManager>().attackDamage += 1;
                }
            }
        }
    }

    public void OnBuyDefencePoint()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        if (shopCollider != null && attributesManagerPlayer!=null)
        {
            if(shopCollider.isOnShop && attributesManagerPlayer.goldAmount >= 100)
            {
                attributesManagerPlayer.goldAmount -= 100;
                if (attributesManagerPlayer.gameObject.CompareTag("Orc"))
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerAttributes>().defencePoint+=2;//thế mạnh nghề nghiệp của Orc
                }
                else
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerAttributes>().defencePoint += 1;
                }
            }
        }
    }

    public void OnBuyHealthPoint()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        if (shopCollider!=null && attributesManagerPlayer != null)
        {
            if(shopCollider.isOnShop && attributesManagerPlayer.goldAmount >= 100)
            {
                attributesManagerPlayer.goldAmount -= 100;
                if (attributesManagerPlayer.gameObject.CompareTag("Tungus"))
                {
                    attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth += 10;//thế mạnh nghề nghiệp của Tungus
                    if(attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth>= attributesManagerPlayer.gameObject.GetComponent<HealthManager>().maxHealth)
                    {
                        attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth = attributesManagerPlayer.gameObject.GetComponent<HealthManager>().maxHealth;
                    }
                }
                else
                {
                    attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth += 5;
                    if (attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth >= attributesManagerPlayer.gameObject.GetComponent<HealthManager>().maxHealth)
                    {
                        attributesManagerPlayer.gameObject.GetComponent<HealthManager>().currentHealth = attributesManagerPlayer.gameObject.GetComponent<HealthManager>().maxHealth;
                    }
                }
            }
        }
    }

    public void OnBuyStaminaPoint()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
        }
        if (shopCollider != null && attributesManagerPlayer != null)
        {
            if(shopCollider.isOnShop && attributesManagerPlayer.goldAmount >= 100)
            {
                attributesManagerPlayer.goldAmount -= 100;
                if (attributesManagerPlayer.gameObject.CompareTag("Asian"))
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerInputManager>().waitToRegenStamina -= 0.2f;//thế mạnh của Asian
                }
                else
                {
                    attributesManagerPlayer.gameObject.GetComponent<PlayerInputManager>().waitToRegenStamina -= 0.1f;
                }
            }
        }
    }
}
