using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircleTungus : MonoBehaviour
{
    public float delayTungusSkillBuff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tungus"))
        {
            StartCoroutine(DelayTungusSkillBuff(other));
        }
    }
    private IEnumerator DelayTungusSkillBuff(Collider ally)
    {
        if (ally != null)
        {
            if (ally.GetComponentInParent<HealthManager>().currentHealth < ally.GetComponentInParent<HealthManager>().maxHealth)
            {
                for (int i = 0; ; i++)
                {
                    ally.GetComponentInParent<HealthManager>().currentHealth += 1;
                    yield return new WaitForSeconds(delayTungusSkillBuff);
                    if (ally.GetComponentInParent<HealthManager>().currentHealth == ally.GetComponentInParent<HealthManager>().maxHealth)
                    {
                        ally.GetComponentInParent<HealthManager>().currentHealth = ally.GetComponentInParent<HealthManager>().maxHealth;
                        break;
                    }
                }
            }
            if (ally.GetComponentInParent<HealthManager>().currentHealth == ally.GetComponentInParent<HealthManager>().maxHealth)
            {
                ally.GetComponentInParent<HealthManager>().currentHealth = ally.GetComponentInParent<HealthManager>().maxHealth;
            }
        }
    }
}
