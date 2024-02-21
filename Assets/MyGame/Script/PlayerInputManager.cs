using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float waitToNextAttack;
    public float waitToNextSkill;
    public Transform cameraTransform;

    private float horizontalInput;
    private float verticalInput;
    private float yForce;

    private bool isMoving;
    private bool isRunning;

    public Transform attackPoint;
    public float attackRange;
    public float attackSpeed;

    public Transform skillPoint;
    public float skillRange;  
    public float skillSpeed;

    public LayerMask enemyLayer;
    public LayerMask allyLayer;
    public ChieftainType chieftainType;

    private CharacterController characterController;

    public ArrowController arrowPrefab;
    public Transform shootPoint;

    public float maxStamina;
    public float currentStamina;
    public float regenStaminaTime;
    public float waitToRegenStamina;
    public int attackDamage;

    public GameObject getHitByWeaponEffect;
    public GameObject getHitByPunchEffect;
    private GameObject getHitEffect;

    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttributes playerAttributes = GetComponent<PlayerAttributes>();
        if(playerAttributes != null )
        {
            maxStamina = playerAttributes.staminaPoint;
            attackDamage = playerAttributes.attackPoint;
        }
        currentStamina = maxStamina;
        playerAnim=GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        attackSpeed = waitToNextAttack;
        skillSpeed = waitToNextSkill;

        waitToRegenStamina = 1f;
        regenStaminaTime = waitToRegenStamina;
    }

    // Update is called once per frame
    void Update()
    {
        regenStaminaTime -= Time.deltaTime;
        if (regenStaminaTime <= 0)
        {
            regenStaminaTime = waitToRegenStamina;
            currentStamina += 0.25f;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        playerAnim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        //print($"Vector Magnitude after normalize: {movementDirection.magnitude}");
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        yForce += Physics.gravity.y * Time.deltaTime;

        if (movementDirection != Vector3.zero)
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_SLOW_RUN, playerAnim);
            }
            isMoving = true;

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift) && currentStamina>0)//còn thể lực thì mới dc chạy
            {
                currentStamina-=Time.deltaTime;
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_FAST_RUN, playerAnim);
                }
                isRunning = true;
            }
            else
            {
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_SLOW_RUN, playerAnim);
                }
                isRunning = false;
            }
        }
        else
        {
            if(ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_IDLE,playerAnim);
            }
            isMoving = false;//nếu ngưng moving thì cũng sẽ tự động ngưng running
            isRunning = false;
        }
        
        attackSpeed-=Time.deltaTime;
        if (attackSpeed <=0 )
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                attackSpeed = waitToNextAttack;
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_HIT, playerAnim);
                }
                Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
                foreach (Collider enemy in hitEnemy)
                {
                    if (this.gameObject.CompareTag("Asian"))
                    {
                        if (AudioManager.HasInstance)
                        {
                            AudioManager.Instance.PlaySE(AUDIO.SE_PUNCH);
                        }
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
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_ARROWFIRING);
                    }
                    Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation).SetMoveDirection(shootPoint.forward);
                }
                if (this.gameObject.CompareTag("Titan"))
                {
                    ListenerManager.Instance.BroadCast(ListenType.TITAN_ATTACK_EFFECT, this.gameObject);
                }
                if (this.gameObject.CompareTag("Asian"))
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_PUNCH);
                    }
                }
                if (this.gameObject.CompareTag("Viking") || this.gameObject.CompareTag("Orc"))
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_AXE);
                    }
                }
            }
        }
        skillSpeed -= Time.deltaTime;
        if (skillSpeed <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z) && chieftainType!=ChieftainType.UNKNOWN)
            {
                GetComponent<AttributesManagerPlayer>().playerManaBar.value = 0f;//dùng skill xong thì mana hồi lại từ đầu
                skillSpeed = waitToNextSkill;
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.PLAYER_SKILL, playerAnim);
                }
                Collider[] skillEnemy = Physics.OverlapSphere(skillPoint.position, skillRange, enemyLayer);
                foreach (Collider enemy in skillEnemy)
                {
                    if (chieftainType == ChieftainType.ASIAN || chieftainType==ChieftainType.TITAN_ASIAN)
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
                        if(ListenerManager.HasInstance)
                        {
                            ListenerManager.Instance.BroadCast(ListenType.SKILL_TUNGUS_ALLY_EFFECT, ally);
                        }
                    }
                }
                if (chieftainType == ChieftainType.ASIAN)
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_KICK);
                    }
                    ListenerManager.Instance.BroadCast(ListenType.SKILL_ASIAN_CHIEFTAIN_EFFECT, this.gameObject);
                }
                if (chieftainType == ChieftainType.VIKING)
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_ROAR);
                    }
                    ListenerManager.Instance.BroadCast(ListenType.SKILL_VIKING_CHIEFTAIN_EFFECT, this.gameObject);
                }
                if (chieftainType == ChieftainType.ORC)
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_FIRE);
                    }
                    ListenerManager.Instance.BroadCast(ListenType.SKILL_ORC_CHIEFTAIN_EFFECT, this.gameObject);
                }
                if (chieftainType == ChieftainType.TUNGUS)
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_HEAL);
                    }
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
    }
    private void OnAnimatorMove()
    {
            if (isRunning)
            {
                Vector3 velocity = playerAnim.deltaPosition * runSpeed;
                velocity.y = yForce * Time.deltaTime;
                characterController.Move(velocity);
            }
            if (isMoving)
            {
                Vector3 velocity = playerAnim.deltaPosition * moveSpeed;
                velocity.y = yForce * Time.deltaTime;
                characterController.Move(velocity);
            }
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;//ẩn và lock chuột tại ví trí trung tâm của scene
    //    }
    //    else
    //    {
    //        Cursor.lockState= CursorLockMode.None;
    //    }
    //}
    private void OnDrawGizmosSelected()
    {
        //if (attackPoint != null)
        //{
        //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //}
        //if (skillPoint != null)
        //{
        //    Gizmos.DrawWireSphere(skillPoint.position, skillRange);
        //}
    }
}
