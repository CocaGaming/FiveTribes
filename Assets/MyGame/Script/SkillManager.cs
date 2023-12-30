using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkillManager : MonoBehaviour
{
    public int skillAsianDamage;
    public float skillVikingEffectTime;
    public float skillOrcEffectTime;
    public float skillTungusEffectTime;

    public GameObject roarEffect;
    public GameObject roarTitanEffect;
    public GameObject fireHellEffect;
    public GameObject fireHellTitanEffect;
    public GameObject buffEffect;
    public GameObject buffTitanEffect;
    public GameObject getHitByKickEffect;
    private GameObject getHitEffect;
    private GameObject skillEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.SKILL_ASIAN_ENEMY_EFFECT,OnSkillAsianEnemyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_VIKING_ENEMY_EFFECT, OnSkillVikingEnemyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, OnSkillVikingChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, OnSkillOrcChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TUNGUS_CHIEFTAIN_EFFECT, OnSkillTungusChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TUNGUS_ALLY_EFFECT, OnSkillTungusAllyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_ORC_EFFECT, OnSkillTitanOrcEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_TUNGUS_EFFECT, OnSkillTitanTungusEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_VIKING_EFFECT, OnSkillTitanVikingEffect);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.SKILL_ASIAN_ENEMY_EFFECT, OnSkillAsianEnemyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_VIKING_ENEMY_EFFECT, OnSkillVikingEnemyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, OnSkillVikingChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, OnSkillOrcChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TUNGUS_CHIEFTAIN_EFFECT, OnSkillTungusChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TUNGUS_ALLY_EFFECT, OnSkillTungusAllyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_ORC_EFFECT, OnSkillTitanOrcEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_TUNGUS_EFFECT, OnSkillTitanTungusEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_VIKING_EFFECT, OnSkillTitanVikingEffect);
        }
    }
    private void OnSkillAsianEnemyEffect(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                enemy.GetComponentInParent<HealthManager>().GetHitBySkill(skillAsianDamage);
                getHitEffect = Instantiate(getHitByKickEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
        }
    }
    private void OnSkillVikingEnemyEffect(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                if (ListenerManager.HasInstance)
                {
                    Animator enemyAnim = enemy.GetComponentInParent<Animator>();
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_IDLE, enemyAnim);
                }
                enemy.GetComponentInParent<CharacterController>().enabled = false;
                enemy.GetComponentInParent<NavMeshAgent>().enabled = false;
                enemy.GetComponentInParent<EnemyAI>().enabled = false;
                StartCoroutine(SetDefaultSkillViking(enemy));
            }
        }
    }
    private IEnumerator SetDefaultSkillViking(Collider enemy)
    {
        yield return new WaitForSeconds(skillVikingEffectTime);
        if (enemy != null)
        {
            enemy.GetComponentInParent<CharacterController>().enabled = true;
            enemy.GetComponentInParent<NavMeshAgent>().enabled = true;
            enemy.GetComponentInParent<EnemyAI>().enabled = true;
        }
    }
    private void OnSkillVikingChieftainEffect(object value)
    {
        if(value != null)
        {
            if (value is GameObject vikingChieftain)
            {
                skillEffect = Instantiate(roarEffect, vikingChieftain.transform);
                Destroy(skillEffect, skillVikingEffectTime);
            }
        }
    }
    private void OnSkillTitanVikingEffect(object value)
    {
        if (value != null)
        {
            if (value is GameObject vikingChieftain)
            {
                skillEffect = Instantiate(roarTitanEffect, vikingChieftain.transform);
                Destroy(skillEffect, skillVikingEffectTime);
            }
        }
    }
    
    private void OnSkillOrcChieftainEffect(object value)
    {
        if (value != null)
        {
            if(value is GameObject orcChieftain)
            {
                skillEffect= Instantiate(fireHellEffect, orcChieftain.transform);
                Destroy(skillEffect, skillOrcEffectTime);
            }
        }
    }
    private void OnSkillTitanOrcEffect(object value)
    {
        if (value != null)
        {
            if (value is GameObject titanOrc)
            {
                skillEffect = Instantiate(fireHellTitanEffect, titanOrc.transform);
                Destroy(skillEffect, skillOrcEffectTime);
            }
        }
    }
    private void OnSkillTungusAllyEffect(object value)
    {
        if (value != null)
        {
            if(value is Collider ally)
            {
                ally.GetComponentInParent<HealthManager>().isInvincible = true;
                StartCoroutine(SetDefaultSkillTungus(ally));
            }
        }
    }
    private IEnumerator SetDefaultSkillTungus(Collider ally)
    {
        yield return new WaitForSeconds(skillTungusEffectTime);
        if(ally != null)
        {
            ally.GetComponentInParent<HealthManager>().isInvincible = false;
        }
    }
    private void OnSkillTungusChieftainEffect(object value)
    {
        if(value != null)
        {
            if(value is GameObject tungusChieftain)
            {
                skillEffect = Instantiate(buffEffect, tungusChieftain.transform);
                Destroy(skillEffect, skillTungusEffectTime);
            }
        }
    }
    private void OnSkillTitanTungusEffect(object value)
    {
        if (value != null)
        {
            if (value is GameObject titanTungus)
            {
                skillEffect = Instantiate(buffTitanEffect, titanTungus.transform);
                Destroy(skillEffect, skillTungusEffectTime);
            }
        }
    }
}
