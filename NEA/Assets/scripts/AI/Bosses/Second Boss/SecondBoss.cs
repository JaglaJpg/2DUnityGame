using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SecondBoss : MonoBehaviour
{
    [SerializeField] public int switchState = 0;
    [SerializeField] public float radius;
    [SerializeField] public float attackRadius;
    [SerializeField] public LayerMask playerChar;
    public bool startFight = false;
    public Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] public Transform attackPos;
    public int damage = 1;
    [SerializeField] public Transform attackPos2;

    [SerializeField] float jumpHeight;
    [SerializeField] Transform target;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask terrain;
    private bool grounded;
    public bool inRange;
    [SerializeField] public bool attacking = false;

    [SerializeField] public Damage script;
    public StateMachine<SecondBoss> stateMachine { get; set; }



    void Start()
    {
        stateMachine = new StateMachine<SecondBoss>(this);
        stateMachine.ChangeState(zzz.Instance);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 9, true);
    }


    void Update()
    {
        if(startFight)
        {
            Physics2D.IgnoreLayerCollision(7, 9, false);
        }


        if (Physics2D.OverlapCircle(transform.position, radius, playerChar) != null)
        {
            startFight = true;
        }

        if (script.health > 10 && startFight)
        {
            switchState = 1;
        }

        if (script.health <= 10)
        {
            switchState = 2;
        }
        stateMachine.Update();
        
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, terrain);
        inRange = Physics2D.OverlapCircle(attackPos.position, attackRadius, playerChar);

        if (stateMachine.currentState == Jeden.Instance)
        {
            if (!attacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if (inRange && !attacking)
            {
                StartCoroutine(attack());
            }
        }


        if (stateMachine.currentState == Dwa.Instance)
        {

            StartCoroutine(JumpAttack());
        }
    }

    private IEnumerator attack()
    {
        attacking = true;
        yield return new WaitForSeconds(3f);
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, 2f, playerChar);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Damage>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    private IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(2f);
        float distance = target.position.x - transform.position.x;

        if (grounded)
        {
            rb.AddForce(new Vector2(distance, jumpHeight), ForceMode2D.Impulse);
        }

        if (grounded)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(attackPos2.position, 2f, playerChar);
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<Damage>().TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(5f);

    }
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(attackPos.position, 2f);
        Gizmos.DrawWireSphere(attackPos2.position, 2f);
    }
}
