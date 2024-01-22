using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributesManagerPlayer : MonoBehaviour
{
    public static AttributesManagerPlayer Instance;

    public Slider playerHealthBar;
    public Slider playerManaBar;
    public Slider playerStaminaBar;

    private float regenManaTime;

    public TextMeshProUGUI attackPoint;
    public TextMeshProUGUI defencePoint;
    public TextMeshProUGUI goldText;

    public int goldAmount;
    public HealthManager[] aiHealthList;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    { 
        playerManaBar.value = 0f;
        goldAmount = 0;

        PlayerAttributes playerAttributes = GetComponent<PlayerAttributes>();
        if(playerAttributes != null)
        {
            playerHealthBar.maxValue = playerAttributes.healthPoint;
            playerStaminaBar.maxValue = playerAttributes.staminaPoint;
            attackPoint.text=playerAttributes.attackPoint.ToString();
            defencePoint.text=playerAttributes.defencePoint.ToString();
        }
        playerStaminaBar.value = playerStaminaBar.maxValue;
        playerHealthBar.value= playerHealthBar.maxValue;
    }
    private void Update()
    {
        playerHealthBar.value= GetComponent<HealthManager>().currentHealth;

        playerStaminaBar.value = GetComponent<PlayerInputManager>().currentStamina;

        regenManaTime -= Time.deltaTime;
        if (regenManaTime <= 0)
        {
            regenManaTime = 1f;
            playerManaBar.value += 1f / GetComponent<PlayerInputManager>().waitToNextSkill;
        }

        aiHealthList =FindObjectsOfType<HealthManager>();//cứ mỗi AI chết thì player sẽ dc cộng tiền
        foreach(var ai in aiHealthList)
        {   
            if (ai.GetComponent<HealthManager>().currentHealth <= 0 && !ai.GetComponent<HealthManager>().isDeath)
            {
                goldAmount += Random.Range(2,5);
                ai.GetComponent<HealthManager>().isDeath = true;
            }
        }
        goldText.text=goldAmount.ToString();
    }
}
