using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagerPlayer : MonoBehaviour
{
    public Slider playerHealthBar;
    private void Start()
    {
        playerHealthBar.maxValue = GetComponent<HealthManager>().maxHealth;
        playerHealthBar.value = GetComponent<HealthManager>().currentHealth;
    }
    private void Update()
    {
        playerHealthBar.value= GetComponent<HealthManager>().currentHealth;
    }
}
