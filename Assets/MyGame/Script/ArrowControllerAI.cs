using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControllerAI : MonoBehaviour
{
    [SerializeField]
    private Rigidbody arrowRB;
    public float arrowSpeed;
    public Vector3 moveDirection;

    public int attackDamage;
    private GameObject getHitEffect;
    public GameObject getHitByWeaponEffect;

    // Start is called before the first frame update
    void Start()
    {
        arrowRB =GetComponent<Rigidbody>();

        attackDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        arrowRB.velocity=moveDirection*arrowSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("OrcArrow")|| other.CompareTag("AsianArrow")|| other.CompareTag("VikingArrow")|| other.CompareTag("TitanArrow"))
        {
            other.GetComponentInParent<HealthManager>().GetHitByAttack(attackDamage, other.GetComponentInParent<PlayerAttributes>().defencePoint);
            getHitEffect = Instantiate(getHitByWeaponEffect, other.transform);
            Destroy(getHitEffect, 2f);
        }
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject,5f);
    }
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection=direction;
    }
}
