using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public ArrowControllerAI arrowPrefab;
    public Transform shootPoint;

    public Transform baseCamp;
    public float baseCampZone;
    public Transform arena;
    public float arenaZone;

    private float resetWalkPointSet;
    private bool isGoHome;

    private Vector3 aiStartPosition;

    private void Start()
    {
        aiStartPosition= this.transform.position;

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

        resetWalkPointSet = 20f;
        
    }
    private void Update()
    {
        yForce += Physics.gravity.y * Time.deltaTime;
        enemyInSightRange = Physics.CheckSphere(transform.position, sightZone, enemyLayer);
        enemyInAttackRange=Physics.CheckSphere(transform.position, attackRange, enemyLayer);
        enemyInSkillRange=Physics.CheckSphere(transform.position,skillRange, enemyLayer);
        aiInBaseCamp = Physics.CheckSphere(baseCamp.position, baseCampZone,allyLayer);
        aiInArena = Physics.CheckSphere(arena.position, arenaZone,allyLayer);

        //AI còn ở camp và chưa tới arena hoặc ko ở camp và chưa tới arena
        if (aiInBaseCamp)//đúng giờ mới chạy ra arena
        {
            if (GameManager.Instance.hour == 10f)
            {
                isGoHome = false;
                RunToArenaPoint();
            }

            if (GameManager.Instance.hour != 10f)//ngoài 9h thì mọi AI đều ở trạng thái idle khi ở trong camp
            {
                Vector3 distanceToStartPoint = this.transform.position - aiStartPosition;

                if (distanceToStartPoint.magnitude <= 5f)
                {
                    if (ListenerManager.HasInstance)
                    {
                        ListenerManager.Instance.BroadCast(ListenType.PLAYER_IDLE, aiAnim);
                    }
                }
            }
        }
        if (GameManager.Instance.hour == 18f)
        {
            RunToBaseCamp();
        }
        //AI đã vào arena
        if (aiInArena && !isGoHome)
        {
            if (GameManager.Instance.hour == 18f)
            {
                isGoHome = true;
            }
            resetWalkPointSet -=Time.deltaTime;
            if (!enemyInSightRange && !enemyInAttackRange)//chưa thấy enemy và chưa vào tầm đánh
            {
                if (resetWalkPointSet <= 0)//tự động set lại walkpoint sau 20s để tránh tình trạng AI bị kẹt
                {
                    resetWalkPointSet = 20f;
                    walkPointSet = false;
                }
                Patroling();
            }
            if (enemyInSightRange)//đã thấy enemy
            {
                if(this.gameObject.CompareTag("Viking")//riêng tộc tungus sẽ ko chase vì sẽ đứng bắn từ xa
                || this.gameObject.CompareTag("Asian") 
                || this.gameObject.CompareTag("Orc") 
                || this.gameObject.CompareTag("Titan"))
                {
                    ChaseEnemy();
                }
            }
            attackSpeed -= Time.deltaTime;
            if (enemyInSightRange && enemyInAttackRange)//đã thấy enemy và đã vào tầm đánh
            {
                if (this.gameObject.CompareTag("Viking")//các tộc cận chiến sẽ đứng yên đánh nhau
                || this.gameObject.CompareTag("Asian")
                || this.gameObject.CompareTag("Orc")
                || this.gameObject.CompareTag("Titan"))
                {
                    if (attackSpeed <= 0)
                    {
                        attackSpeed = waitToNextAttack;
                        AttackEnemy();
                    }
                }
                if (this.gameObject.CompareTag("Tungus"))//tộc tungus sẽ hit and run
                {
                    if (resetWalkPointSet <= 0)//tự động set lại walkpoint sau 20s để tránh tình trạng AI bị kẹt
                    {
                        resetWalkPointSet = 20f;
                        walkPointSet = false;
                    }
                    Patroling();
                    if (attackSpeed <= 0)
                    {
                        attackSpeed = waitToNextAttack;
                        AttackEnemy();
                    }
                }
            }
            skillSpeed -= Time.deltaTime;
            if (enemyInSightRange && enemyInSkillRange)//đã thấy enemy và đã vào tầm skill
            {
                if (skillSpeed <= 0)
                {
                        skillSpeed = waitToNextSkill;
                        SkillEnemy();
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
    private void RunToBaseCamp()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLAYER_FAST_RUN, aiAnim);
        }
        agent.SetDestination(aiStartPosition);
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
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemy)
        {
            transform.LookAt(enemy.transform.position);//nhìn đúng hướng rồi mới đánh
            if (this.gameObject.CompareTag("Asian"))
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
                }
                enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                getHitEffect = Instantiate(getHitByPunchEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
            if (this.gameObject.CompareTag("Viking"))
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
                }
                enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
            if (this.gameObject.CompareTag("Orc"))
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
                }
                enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                getHitEffect = Instantiate(getHitByWeaponEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
            if (this.gameObject.CompareTag("Titan"))
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
                }
                enemy.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, enemy.GetComponentInParent<PlayerAttributes>().defencePoint);
                getHitEffect = Instantiate(getHitByPunchEffect, enemy.transform);
                Destroy(getHitEffect, 2f);
            }
        }
        if (this.gameObject.CompareTag("Tungus"))
        {
            bool tungusHitEnemy = Physics.Raycast(shootPoint.position, shootPoint.forward, attackRange, enemyLayer);
            if (tungusHitEnemy)
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, aiAnim);
                }
                Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation).SetMoveDirection(shootPoint.forward);
            }
        }
        if (this.gameObject.CompareTag("Titan"))
        {
            ListenerManager.Instance.BroadCast(ListenType.TITAN_ATTACK_EFFECT, this.gameObject);
        }
    }
    private void SkillEnemy()
    {
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
            if (chieftainType == ChieftainType.ASIAN)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_ASIAN_CHIEFTAIN_AI_EFFECT, this.gameObject);
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
            if (chieftainType == ChieftainType.TITAN_ASIAN)
            {
                ListenerManager.Instance.BroadCast(ListenType.SKILL_TITAN_ASIAN_EFFECT, this.gameObject);
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
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        if (skillPoint != null)
        {
            Gizmos.DrawWireSphere(skillPoint.position, skillRange);
        }
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
