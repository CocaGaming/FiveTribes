using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterSelect : BaseManager<CharacterSelect>
{
    public GameObject tungusChieftain;
    public GameObject tungusVillager;
    public GameObject orcChieftain;
    public GameObject orcVillager;
    public GameObject asianChieftain;
    public GameObject asianVillager;
    public GameObject vikingChieftain;
    public GameObject vikingVillager;

    public GameObject aiTungusChieftain;
    public GameObject aiTungusVillager;
    public GameObject aiOrcChieftain;
    public GameObject aiOrcVillager;
    public GameObject aiAsianChieftain;
    public GameObject aiAsianVillager;
    public GameObject aiVikingChieftain;
    public GameObject aiVikingVillager;

    public GameObject asianShop;
    public GameObject orcShop;
    public GameObject tungusShop;
    public GameObject vikingShop;
    public CinemachineFreeLook freelookCamera;

    public float stamina;
    public float maxHealth;
    public float currentHealth;
    public int attack;
    public int defence;


public void SetCharacter()
{
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.TUNGUS && GameManager.Instance.roleType == RoleType.CHIEFTAIN)
        {
            tungusChieftain.SetActive(true);
            aiTungusChieftain.SetActive(false);
            tungusVillager.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            asianShop.SetActive(false);
            orcShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = tungusChieftain.transform.Find("CameraFocus");
            freelookCamera.LookAt = tungusChieftain.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.TUNGUS && GameManager.Instance.roleType == RoleType.VILLAGER)
        {
            tungusVillager.SetActive(true);
            aiTungusVillager.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            asianShop.SetActive(false);
            orcShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = tungusVillager.transform.Find("CameraFocus");
            freelookCamera.LookAt = tungusVillager.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.ORC && GameManager.Instance.roleType == RoleType.CHIEFTAIN)
        {
            orcChieftain.SetActive(true);
            aiOrcChieftain.SetActive(false);
            tungusChieftain.SetActive(false);
            tungusVillager.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            asianShop.SetActive(false);
            tungusShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = orcChieftain.transform.Find("CameraFocus");
            freelookCamera.LookAt = orcChieftain.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.ORC && GameManager.Instance.roleType == RoleType.VILLAGER)
        {
            orcVillager.SetActive(true);
            aiOrcVillager.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            tungusVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            asianShop.SetActive(false);
            tungusShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = orcVillager.transform.Find("CameraFocus");
            freelookCamera.LookAt = orcVillager.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.ASIAN && GameManager.Instance.roleType == RoleType.CHIEFTAIN)
        {
            asianChieftain.SetActive(true);
            aiAsianChieftain.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            tungusVillager.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            orcShop.SetActive(false);
            tungusShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = asianChieftain.transform.Find("CameraFocus");
            freelookCamera.LookAt = asianChieftain.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.ASIAN && GameManager.Instance.roleType == RoleType.VILLAGER)
        {
            asianVillager.SetActive(true);
            aiAsianVillager.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            tungusVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            vikingVillager.SetActive(false);

            orcShop.SetActive(false);
            tungusShop.SetActive(false);
            vikingShop.SetActive(false);

            freelookCamera.Follow = asianVillager.transform.Find("CameraFocus");
            freelookCamera.LookAt = asianVillager.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.VIKING && GameManager.Instance.roleType == RoleType.CHIEFTAIN)
        {
            vikingChieftain.SetActive(true);
            aiVikingChieftain.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            tungusVillager.SetActive(false);
            vikingVillager.SetActive(false);

            orcShop.SetActive(false);
            tungusShop.SetActive(false);
            asianShop.SetActive(false);

            freelookCamera.Follow = vikingChieftain.transform.Find("CameraFocus");
            freelookCamera.LookAt = vikingChieftain.transform.Find("CameraFocus");
        }
    }
    if (GameManager.HasInstance)
    {
        if (GameManager.Instance.tribeType == TribeType.VIKING && GameManager.Instance.roleType == RoleType.VILLAGER)
        {
            vikingVillager.SetActive(true);
            aiVikingVillager.SetActive(false);
            tungusChieftain.SetActive(false);
            orcChieftain.SetActive(false);
            orcVillager.SetActive(false);
            asianChieftain.SetActive(false);
            asianVillager.SetActive(false);
            vikingChieftain.SetActive(false);
            tungusVillager.SetActive(false);

            orcShop.SetActive(false);
            tungusShop.SetActive(false);
            asianShop.SetActive(false);

            freelookCamera.Follow = vikingVillager.transform.Find("CameraFocus");
            freelookCamera.LookAt = vikingVillager.transform.Find("CameraFocus");
        }
    }
}
}
