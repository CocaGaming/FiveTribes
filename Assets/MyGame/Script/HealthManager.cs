using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Animator playerAnim;
    public bool isInvincible;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttributes playerAttributes = GetComponent<PlayerAttributes>();
        if (playerAttributes != null)
        {
            maxHealth = playerAttributes.healthPoint;
        }
        currentHealth = maxHealth;
        playerAnim = GetComponent<Animator>();
        isInvincible = false;
    }
    public void GetHitBySkill(int attackDamage)
    {
        if (isInvincible == false)
        {
            currentHealth -= attackDamage;
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_GET_HIT, playerAnim);
            }
            if (currentHealth <= 0)
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_DEATH, playerAnim);
                }
                //sau khi chết thì tắt hết
                OnDeath();
            }
        }
    }
    public void GetHitByAttack(int attackDamage, int enemyArmor)
    {
        if (isInvincible==false)
        {
            if (attackDamage - enemyArmor <= 1)
            {
                currentHealth -= 1;
            }
            if(attackDamage - enemyArmor >= 2 && attackDamage - enemyArmor <= 4)
            {
                currentHealth -= 2;
            }
            if (attackDamage - enemyArmor >= 5 && attackDamage - enemyArmor <= 7)
            {
                currentHealth -= 3;
            }
            if (attackDamage - enemyArmor >= 8)
            {
                currentHealth -= 4;
            }
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_GET_HIT, playerAnim);
            }
            if (currentHealth <= 0)
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_DEATH, playerAnim);
                }
                //sau khi chết thì tắt hết
                OnDeath();
            }
        }
    }
    public void OnDeath()
    {
        this.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        if(this.GetComponent<PlayerInputManager>() != null)
        {
            GetComponent<PlayerInputManager>().enabled = false;
        }
        GetComponentInChildren<CapsuleCollider>().enabled = false;
    }
}
