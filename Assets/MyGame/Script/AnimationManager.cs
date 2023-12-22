using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.PLAYER_IDLE,OnListenPlayerIdle);
            ListenerManager.Instance.Register(ListenType.PLAYER_SLOW_RUN,OnListenPlayerSlowRun);
            ListenerManager.Instance.Register(ListenType.PLAYER_FAST_RUN,OnListenPlayerFastRun);
            ListenerManager.Instance.Register(ListenType.PLAYER_HIT,OnListenPlayerHit);
            ListenerManager.Instance.Register(ListenType.PLAYER_GET_HIT, OnListenPlayerGetHit);
            ListenerManager.Instance.Register(ListenType.PLAYER_DEATH, OnListenPlayerDeath);
            ListenerManager.Instance.Register(ListenType.PLAYER_SKILL, OnListenPlayerSkill);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.PLAYER_IDLE,OnListenPlayerIdle);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_SLOW_RUN,OnListenPlayerSlowRun);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_FAST_RUN,OnListenPlayerFastRun);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_HIT,OnListenPlayerHit);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_GET_HIT, OnListenPlayerGetHit);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_DEATH, OnListenPlayerDeath);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_SKILL, OnListenPlayerSkill);
        }
    }
    private void OnListenPlayerIdle(object value)
    {
        if(value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetBool("IsMoving", false);
                playerAnim.SetBool("IsRunning", false);
            }
        }
    }
    private void OnListenPlayerSlowRun(object value)
    {
        if (value != null)
        {
            if (value is Animator playerAnim)
            {
                playerAnim.SetBool("IsMoving", true);
                playerAnim.SetBool("IsRunning", false);
            }
        }
    }
    private void OnListenPlayerFastRun(object value)
    {
        if (value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetBool("IsRunning", true);
            }
        }
    }
    private void OnListenPlayerHit(object value)
    {
        if (value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetTrigger("IsAttacking");
            }
        }
    }
    private void OnListenPlayerGetHit(object value)
    {
        if(value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetTrigger("GetHit");
            }
        }
    }
    private void OnListenPlayerDeath(object value)
    {
        if (value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetBool("IsDeath", true);
            }
        }
    }
    private void OnListenPlayerSkill(object value)
    {
        if (value != null)
        {
            if(value is Animator playerAnim)
            {
                playerAnim.SetTrigger("IsSkill");
            }
        }
    }
}   
