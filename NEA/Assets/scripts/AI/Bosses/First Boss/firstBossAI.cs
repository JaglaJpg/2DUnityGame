using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class firstBossAI : MonoBehaviour
{
    [SerializeField] public int switchState = 0;
    [SerializeField] public Transform player;
    [SerializeField] public Transform bossRoom;
    [SerializeField] public LayerMask playerChar;
    [SerializeField] public float radius;
    public bool startFight = false;
    [SerializeField] public Transform spike;
    public Vector3 curPos1;
    public Vector3 curPos2;
    public Vector3 curPos3;
   // public bool check = false;
    public Rigidbody2D rb;
    public bool attacking = false;
    [SerializeField] public Transform hitBox;
    public int damage = 1;
    

    [SerializeField] public Damage script;

    public StateMachine<firstBossAI> stateMachine { get; set; }

    void Start()
    {
        stateMachine = new StateMachine<firstBossAI>(this);
        stateMachine.ChangeState(SleepState.Instance);
        rb = GetComponent<Rigidbody2D>();
        curPos2 = spike.position;
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }


    void Update()
    {
        if (Physics2D.OverlapCircle(bossRoom.position, radius, playerChar) != null)
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
        if (stateMachine.currentState == FirstPhase.Instance)
        {
            if (!attacking)
            {
                spike.position = Vector2.MoveTowards(spike.position, new Vector2(player.position.x, spike.position.y), 2f * Time.deltaTime);
            }

            if (spike.position.x == player.position.x)
            {
                //check = true;
                StartCoroutine(spikeAttack());
            }
        }

        if (stateMachine.currentState == SecondPhase.Instance)
        {
            //check = true;
            spike.Translate(0f, -5f, 0f);
            if (!attacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), 3f * Time.deltaTime);
            }
            
            if (transform.position.x == player.position.x)
            {
                StartCoroutine(slamAttack());
            }
        }

    }



    private IEnumerator spikeAttack()
    {
        attacking = true;
        checkPos();
        yield return new WaitForSeconds(2f);
        spike.position = curPos1;
        Collider2D[] player = Physics2D.OverlapCircleAll(spike.position, 1f,  playerChar);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Damage>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(1f);
        spike.position = curPos2; 
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    private IEnumerator slamAttack()
    {
        attacking = true;
        checkPos();
        yield return new WaitForSeconds(1.5f);
        rb.velocity = new Vector2(0f, -30f);
        Collider2D[] player = Physics2D.OverlapCircleAll(hitBox.position, 2.5f, playerChar);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Damage>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(3f);
        while(transform.position != curPos3)
        {
            transform.position = Vector2.MoveTowards(transform.position, curPos3, 1f);
        }
        attacking = false;
    }

    void checkPos()
    {
        curPos1 = player.position;
        curPos2 = spike.position;
        curPos3 = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bossRoom.position, radius);
        Gizmos.DrawWireSphere(spike.position, 1f);
        Gizmos.DrawWireSphere(hitBox.position, 2.5f);
    }


}
