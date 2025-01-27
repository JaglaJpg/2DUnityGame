using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D box;

    private float attackCool = 0f;
    [SerializeField] public float attackStart;

    [SerializeField] public Transform attackPos;
    [SerializeField] public LayerMask whatIsEnemies;
    [SerializeField] public float attackRange;
    [SerializeField] public int damage;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    //if left click is pressed the character attacks and takes health from enemy which a cooldown
    void Update()
    {

        if (attackCool <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Debug.Log(enemiesToDamage);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Damage>().TakeDamage(damage);
                }
                attackCool = attackStart;
            }
             
        }
        else
        {
            attackCool -= Time.deltaTime;
        }

    }


    //uses an attached empty gameobject's position as position for an attack hitbox
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
