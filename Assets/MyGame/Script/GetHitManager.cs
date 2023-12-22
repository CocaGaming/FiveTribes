using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitManager : MonoBehaviour
{
    public GameObject getHitByWeaponEffect;
    public GameObject getHitByPunchEffect;
    private GameObject getHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.GET_HIT_BY_ASIAN, OnPlayerGetHitByAsian);
            ListenerManager.Instance.Register(ListenType.GET_HIT_BY_VIKING, OnPlayerGetHitByViking);
            ListenerManager.Instance.Register(ListenType.GET_HIT_BY_ORC, OnPlayerGetHitByOrc);
            ListenerManager.Instance.Register(ListenType.GET_HIT_BY_TUNGUS, OnPlayerGetHitByTungus);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.GET_HIT_BY_ASIAN, OnPlayerGetHitByAsian);
            ListenerManager.Instance.Unregister(ListenType.GET_HIT_BY_VIKING, OnPlayerGetHitByViking);
            ListenerManager.Instance.Unregister(ListenType.GET_HIT_BY_ORC, OnPlayerGetHitByOrc);
            ListenerManager.Instance.Unregister(ListenType.GET_HIT_BY_TUNGUS, OnPlayerGetHitByTungus);
        }
    }
    private void OnPlayerGetHitByAsian(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                getHitEffect = Instantiate(getHitByPunchEffect, enemy.transform);
                Destroy(getHitEffect,2f);
            }
        }
    }
    private void OnPlayerGetHitByViking(object value)
    {
        if (value != null)
        {
            if (value is Collider enemy)
            {
                getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                Destroy(getHitEffect,2f);
            }
        }
    }
    private void OnPlayerGetHitByOrc(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
        }
    }
    private void OnPlayerGetHitByTungus(object value)
    {
       if(value != null)
        {
            if(value is Collider enemy)
            {
                getHitEffect=Instantiate(getHitByWeaponEffect,enemy.transform);
                Destroy(getHitEffect, 2f);
            }
        }
    }
}
