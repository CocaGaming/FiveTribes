using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHellTitan : MonoBehaviour
{
    public int skillOrcDamage;
    public float delayOrcSkillDamage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tungus")|| other.CompareTag("Asian")|| other.CompareTag("Viking") || other.CompareTag("Orc"))
        {
            StartCoroutine(DelayOrcSkillDamage(other));
        }
    }
    private IEnumerator DelayOrcSkillDamage(Collider enemy)
    {
        if (enemy != null)
        {
            for (int i = 0; ; i++)
            {
                enemy.GetComponentInParent<HealthManager>().GetHitBySkill(skillOrcDamage);//mất máu theo thời gian
                yield return new WaitForSeconds(delayOrcSkillDamage);
                if (enemy.GetComponentInParent<HealthManager>().currentHealth <= 0)
                {
                    break;
                }
            }
        }
    }
}
