using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator aiAnim;
    public CharacterController characterController;

    public LayerMask enemyLayer;
    public LayerMask allyLayer;
    public ChieftainType chieftainType;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float moveSpeed;
    public float sightZone;
    
    public bool enemyInSightRange;
    public bool enemyInAttackRange;
    public bool enemyInSkillRange;
    public bool aiInBaseCamp;
    public bool aiInArena;

    public Transform attackPoint;
    public float attackRange;
    public float attackSpeed;
    public float waitToNextAttack;

    public Transform skillPoint;
    public float skillRange;
    public float skillSpeed;
    public float waitToNextSkill;

    private int attackDamage;
    private float yForce;

    public GameObject getHitByWeaponEffect;
    public GameObject getHitByPunchEffect;
    private GameObject getHitEffect;

    public ArrowController arrowPrefab;
    public Transform shootPoint;

    public Transform baseCamp;
    public float baseCampZone;
    public Transform arena;
    public float arenaZone;

    private void Start()
    {
        PlayerAttributes playerAttributes = GetComponent<PlayerAttributes>();
        if (playerAttributes != null)
        {
            attackDamage = playerAttributes.attackPoint;
        }
        agent= GetComponent<NavMeshAgent>();
        aiAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        attackSpeed = waitToNextAttack;
        skillSpeed = waitToNextSkill;
    }
    private void Update()
    {
        yForce += Physics.gravity.y * Time.deltaTime;
        enemyInSightRange = Physics.CheckSphere(transform.position, sightZone, enemyLayer);
        enemyInAttackRange=Physics.CheckSphere(transform.position, attackRange, enemyLayer);
        enemyInSkillRange=Physics.CheckSphere(transform.position,skillRange, enemyLayer);
        aiInBaseCamp = Physics.CheckSphere(baseCamp.position, baseCampZone,allyLayer);
        aiInArena = Physics.CheckSphere(arena.position, arenaZone,allyLayer);
        Debug.Log($"Basecamp: {aiInBaseCamp}");
        Debug.Log($"Arena: {aiInArena}");

        if ((aiInBaseCamp && !aiInArena) || (!aiInBaseCamp && !aiInArena))
        {
            RunToArenaPoint();
        }

        if(aiInArena)
        {
            if (!enemyInSightRange && !enemyInAttackRange)
            {
                Patroling();
            }
            if (enemyInSightRange)
            {
                ChaseEnemy();
            }
            attackSpeed -= Time.deltaTime;
            if (enemyInSightRange && enemyInAttackRange)
            {
                if (attackSpeed <= 0)
                {
                    attackSpeed = waitToNextAttack;
                    AttackEnemy();
                }
            }
            skillSpeed -= Time.deltaTime;
            if (enemyInSightRange && enemyInSkillRange)
            {
                if (skillSpeed <= 0)
                {
                    if (chieftainType != ChieftainType.UNKNOWN)
                    {
                        skillSpeed = waitToNextSkill;
                        SkillEnemy();
                    }
                }
            }
        }
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();  
        }
        if (walkPointSet)
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_FAST_RUN, aiAnim);
            }
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint= transform.position- walkPoint;

        if (distanceToWalkPoint.magnitude <= 20f)//nếu AI đã tới gần walkpoint thì set false để tự động tìm point khác di chuyển
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomX= Random.Range(-arenaZone+20f, arenaZone-20f);//+- bớt 20 đơn vị để đảm bảo walkpoint nằm trong arenazone
        float randomZ= Random.Range(-arenaZone+20f,arenaZone-20f);
        Debug.Log($"RandomX: {randomX}");
        Debug.Log($"RandomZ: {randomZ}");

        walkPoint =new Vector3(arena.position.x+ randomX, arena.position.y, arena.position.z+ randomZ);

        if (Physics.Raycast(walkPoint, -arena.up, 1f))//ktra vùng di chuyển có phải là mặt đất ko
        {
            walkPointSet= true;
        }
    }
    private void RunToArenaPoint()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLAYER_FAST_RUN, aiAnim);
        }
        agent.SetDestination(arena.position);
    }
    private void ChaseEnemy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLAYER_FAST_RUN, aiAnim);
        }
        Collider[] findEnemy = Physics.OverlapSphere(transform.position, sightZone, enemyLayer);
        foreach (Collider enemy in findEnemy)
        {
            if (this.GetComponent<NavMeshAgent>().enabled == true)
            {
                Transform enemyTransform = enemy.GetComponent<Transform>();
                agent.SetDestination(enemyTransform.position);
            }
        }
    }
    private void AttackEnemy()
    {
        //if (this.GetComponent<NavMeshAgent>().enabled == true)
        //{
        //    agent.SetDestination(transform.position);//đảm bảo AI ko di chuyển
        //}

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
        }
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemy)
        {
                if (this.gameObject.CompareTag("Asian"))
                {
                    enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                    getHitEffect = Instantiate(getHitByPunchEffect, enemy.transform);
                    Destroy(getHitEffect, 2f);
                }
                if (this.gameObject.CompareTag("Viking"))
                {
                    enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                    getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                    Destroy(getHitEffect, 2f);
                }
                if (this.gameObject.CompareTag("Orc"))
                {
                    enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                    getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                    Destroy(getHitEffect, 2f);
                }
                if (this.gameObject.CompareTag("Titan"))
                {
                    enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                    getHitEffect = Instantiate(getHitByPunchEffect, enemy.transform);
                    Destroy(getHitEffect, 2f);
                }
        }
        if (this.gameObject.CompareTag("Tungus"))
        {
            Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation).SetMoveDirection(shootPoint.forward);
        }
    }
    private void SkillEnemy()
    {
        //if (this.GetComponent<NavMeshAgent>().enabled == true)
        //{
        //    agent.SetDestination(transform.position);//đảm bảo AI ko di chuyển
        //}

        if (chieftainType != ChieftainType.UNKNOWN)
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_SKILL, aiAnim);
            }
            Collider[] skillEnemy = Physics.OverlapSphere(skillPoint.position, skillRange, enemyLayer);
            foreach (Collider enemy in skillEnemy)
            {
                if (chieftainType == ChieftainType.ASIAN || chieftainType == ChieftainType.TITAN_ASIAN)
                {
                    if (ListenerManager.HasInstance)
                    {
                        ListenerManager.Instance.BroadCast(ListenType.SKILL_ASIAN_ENEMY_EFFECT, enemy);
                    }
                }
                if (chieftainType == ChieftainType.VIKING || chieftainType == ChieftainType.TITAN_VIKING)
                {
                    if (ListenerManager.HasInstance)
                    {
                        ListenerManager.Instance.BroadCast(ListenType.SKILL_VIKING_ENEMY_EFFECT, enemy);
                    }
                }
            }
            Collider[] buffAlly = Physics.OverlapSphere(skillPoint.position, skillRange, allyLayer);
            foreach (Collider ally in buffAlly)
            {
                if (chieftainType == ChieftainType.TUNGUS || chieftainType == ChieftainType.TITAN_TUNGUS)
                {
                    if (ListenerManager.HasInstance)
                    {
                        ListenerManager.Instance.BroadCast(ListenType.SKILL_TUNGUS_ALLY_EFFECT, ally);
                    }
                }
            }
            if (chieftainType == ChieftainType.VIKING)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, this.gameObject);
            }
            if (chieftainType == ChieftainType.ORC)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, this.gameObject);
            }
            if (chieftainType == ChieftainType.TUNGUS)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_TUNGUS_CHIEFTAIN_EFFECT, this.gameObject);
            }
            if (chieftainType == ChieftainType.TITAN_ORC)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_TITAN_ORC_EFFECT, this.gameObject);
            }
            if (chieftainType == ChieftainType.TITAN_TUNGUS)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_TITAN_TUNGUS_EFFECT, this.gameObject);
            }
            if (chieftainType == ChieftainType.TITAN_VIKING)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_TITAN_VIKING_EFFECT, this.gameObject);
            }
        }
    }
    private void OnAnimatorMove()
    {
        Vector3 velocity = aiAnim.deltaPosition * moveSpeed;
        velocity.y = yForce * Time.deltaTime;
        characterController.Move(velocity);
    }
    private void OnDrawGizmosSelected()
    {
        if (baseCamp != null)
        {
            Gizmos.DrawWireSphere(baseCamp.position, baseCampZone);
        }
        if (arena != null)
        {
            Gizmos.DrawWireSphere(arena.position, arenaZone);
        }
    }
}
