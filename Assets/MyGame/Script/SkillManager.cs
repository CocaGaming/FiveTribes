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

    public GameObject titanAttackEffect;
    public GameObject kickEffect;
    public GameObject titanKickEffect;
    public GameObject roarEffect;
    public GameObject stunEffect;
    public GameObject roarTitanEffect;
    public GameObject fireHellEffect;
    public GameObject fireHellTitanEffect;
    public GameObject buffEffect;
    public GameObject buffTitanEffect;
    public GameObject getHitByKickEffect;
    private GameObject getHitEffect;
    private GameObject skillEffect;
    private GameObject attackEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.SKILL_ASIAN_ENEMY_EFFECT,OnSkillAsianEnemyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_ASIAN_CHIEFTAIN_EFFECT, OnSkillAsianChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_ASIAN_CHIEFTAIN_AI_EFFECT, OnSkillAsianChieftainAIEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_VIKING_ENEMY_EFFECT, OnSkillVikingEnemyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, OnSkillVikingChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, OnSkillOrcChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TUNGUS_CHIEFTAIN_EFFECT, OnSkillTungusChieftainEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TUNGUS_ALLY_EFFECT, OnSkillTungusAllyEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_ASIAN_EFFECT, OnSkillTitanAsianEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_ORC_EFFECT, OnSkillTitanOrcEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_TUNGUS_EFFECT, OnSkillTitanTungusEffect);
            ListenerManager.Instance.Register(ListenType.SKILL_TITAN_VIKING_EFFECT, OnSkillTitanVikingEffect);
            ListenerManager.Instance.Register(ListenType.TITAN_ATTACK_EFFECT, OnTitanAttackEffect);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.SKILL_ASIAN_ENEMY_EFFECT, OnSkillAsianEnemyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_ASIAN_CHIEFTAIN_EFFECT, OnSkillAsianChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_ASIAN_CHIEFTAIN_AI_EFFECT, OnSkillAsianChieftainAIEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_VIKING_ENEMY_EFFECT, OnSkillVikingEnemyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, OnSkillVikingChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, OnSkillOrcChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TUNGUS_CHIEFTAIN_EFFECT, OnSkillTungusChieftainEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TUNGUS_ALLY_EFFECT, OnSkillTungusAllyEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_ASIAN_EFFECT, OnSkillTitanAsianEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_ORC_EFFECT, OnSkillTitanOrcEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_TUNGUS_EFFECT, OnSkillTitanTungusEffect);
            ListenerManager.Instance.Unregister(ListenType.SKILL_TITAN_VIKING_EFFECT, OnSkillTitanVikingEffect);
            ListenerManager.Instance.Unregister(ListenType.TITAN_ATTACK_EFFECT, OnTitanAttackEffect);
        }
    }
    private void OnTitanAttackEffect(object value)
    {
        if (value != null)
        {
            if(value is GameObject titan)
            {
                attackEffect = Instantiate(titanAttackEffect, titan.transform);
                Destroy(attackEffect, 2f);
            }
        }
    }
    private void OnSkillAsianEnemyEffect(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                if (enemy.GetComponentInParent<HealthManager>().isInvincible == false)
                {
                    enemy.GetComponentInParent<HealthManager>().GetHitBySkill(skillAsianDamage);
                    getHitEffect = Instantiate(getHitByKickEffect, enemy.transform);
                    Destroy(getHitEffect, 2f);
                }
            }
        }
    }
    private void OnSkillAsianChieftainEffect(object value)
    {
        if(value != null)
        {
            if(value is GameObject asianChieftain)
            {
                skillEffect = Instantiate(kickEffect, asianChieftain.GetComponent<PlayerInputManager>().skillPoint);
                Destroy(skillEffect,3f);
            }
        }
    }
    private void OnSkillAsianChieftainAIEffect(object value)//do AI ko có component Input nên phải tạo riêng sự kiện khác để get skillpoint từ enemyAI
    {
        if (value != null)
        {
            if (value is GameObject asianChieftain)
            {
                skillEffect = Instantiate(kickEffect, asianChieftain.GetComponent<EnemyAI>().skillPoint);
                Destroy(skillEffect, 3f);
            }
        }
    }
    private void OnSkillTitanAsianEffect(object value)
    {
        if (value != null)
        {
            if(value is GameObject titanAsian)
            {
                skillEffect = Instantiate(titanKickEffect, titanAsian.GetComponent<EnemyAI>().skillPoint);
                Destroy(skillEffect, 3f);
            }
        }
    }
    private void OnSkillVikingEnemyEffect(object value)
    {
        if (value != null)
        {
            if(value is Collider enemy)
            {
                if (enemy.GetComponentInParent<HealthManager>().isInvincible == false)
                {
                    skillEffect = Instantiate(stunEffect, enemy.transform.position + new Vector3(0, 1.7f, 0), enemy.transform.rotation);
                    Destroy(skillEffect,skillVikingEffectTime);

                    if (ListenerManager.HasInstance)
                    {
                        Animator enemyAnim = enemy.GetComponentInParent<Animator>();
                        ListenerManager.Instance.BroadCast(ListenType.PLAYER_IDLE, enemyAnim);
                    }

                    if(enemy.GetComponentInParent<PlayerInputManager>() != null)
                    {
                        enemy.GetComponentInParent<PlayerInputManager>().enabled = false;
                    }

                    //enemy.GetComponentInParent<CharacterController>().enabled = false;

                    if (enemy.GetComponentInParent<NavMeshAgent>() != null)
                    {
                        enemy.GetComponentInParent<NavMeshAgent>().enabled = false;
                    }

                    if (enemy.GetComponentInParent<EnemyAI>() != null)
                    {
                        enemy.GetComponentInParent<EnemyAI>().enabled = false;
                    }
                   
                    StartCoroutine(SetDefaultSkillViking(enemy));
                }
            }
        }
    }
    private IEnumerator SetDefaultSkillViking(Collider enemy)
    {
        yield return new WaitForSeconds(skillVikingEffectTime);

        if (enemy != null && enemy.GetComponentInParent<HealthManager>().currentHealth>0)//nếu còn sống thì mới bật lại
        {
            if (enemy.GetComponentInParent<PlayerInputManager>() != null)
            {
                enemy.GetComponentInParent<PlayerInputManager>().enabled = true;
            }

            //enemy.GetComponentInParent<CharacterController>().enabled = true;

            if (enemy.GetComponentInParent<NavMeshAgent>() != null)
            {
                enemy.GetComponentInParent<NavMeshAgent>().enabled = true;
            }

            if (enemy.GetComponentInParent<EnemyAI>() != null)
            {
                enemy.GetComponentInParent<EnemyAI>().enabled = true;
            }
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
            if (value is GameObject titanViking)
            {
                skillEffect = Instantiate(roarTitanEffect, titanViking.transform);
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
