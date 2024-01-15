using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int healthPoint;
    public int staminaPoint;
    public int attackPoint;
    public int defencePoint;
    public TribeType tribeType;

    private void Awake()
    {
        if (tribeType == TribeType.ASIAN)
        {
            healthPoint = DataManager.Instance.playerDataAsian.playerData.healthPoint;
            staminaPoint = DataManager.Instance.playerDataAsian.playerData.staminaPoint;
            attackPoint = DataManager.Instance.playerDataAsian.playerData.attackPoint;
            defencePoint = DataManager.Instance.playerDataAsian.playerData.defencePoint;
        }
        if (tribeType == TribeType.TUNGUS)
        {
            healthPoint = DataManager.Instance.playerDataTungus.playerData.healthPoint;
            staminaPoint = DataManager.Instance.playerDataTungus.playerData.staminaPoint;
            attackPoint = DataManager.Instance.playerDataTungus.playerData.attackPoint;
            defencePoint = DataManager.Instance.playerDataTungus.playerData.defencePoint;
        }
        if (tribeType == TribeType.VIKING)
        {
            healthPoint = DataManager.Instance.playerDataViking.playerData.healthPoint;
            staminaPoint = DataManager.Instance.playerDataViking.playerData.staminaPoint;
            attackPoint = DataManager.Instance.playerDataViking.playerData.attackPoint;
            defencePoint = DataManager.Instance.playerDataViking.playerData.defencePoint;
        }
        if (tribeType == TribeType.ORC)
        {
            healthPoint = DataManager.Instance.playerDataOrc.playerData.healthPoint;
            staminaPoint = DataManager.Instance.playerDataOrc.playerData.staminaPoint;
            attackPoint = DataManager.Instance.playerDataOrc.playerData.attackPoint;
            defencePoint = DataManager.Instance.playerDataOrc.playerData.defencePoint;
        }
        if (tribeType == TribeType.TITAN)
        {
            healthPoint = DataManager.Instance.playerDataTitan.playerData.healthPoint;
            staminaPoint = DataManager.Instance.playerDataTitan.playerData.staminaPoint;
            attackPoint = DataManager.Instance.playerDataTitan.playerData.attackPoint;
            defencePoint = DataManager.Instance.playerDataTitan.playerData.defencePoint;
        }
    }
}
