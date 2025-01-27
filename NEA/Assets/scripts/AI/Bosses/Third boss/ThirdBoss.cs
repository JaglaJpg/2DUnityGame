using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ThirdBoss : MonoBehaviour
{
    [SerializeField] public int switchState = 0;
    [SerializeField] public Transform bossRoom;
    [SerializeField] public Transform attackPos;
    [SerializeField] public float radius;
    public bool startFight = false;
    [SerializeField] public LayerMask playerChar;
    public int count = 0;

    [SerializeField] public GameObject bullet;
    float nextFire;
    float fireRate;

    public StateMachine<ThirdBoss> stateMachine { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<ThirdBoss>(this);
        stateMachine.ChangeState(Sleep.Instance);
        Physics2D.IgnoreLayerCollision(7, 8, true);

        fireRate = 1f;
        nextFire = Time.time;
    }

    void Update()
    {
        if(count >= 5)
        {
            count = 0;
        }

        if (Physics2D.OverlapCircle(bossRoom.position, radius, playerChar) != null)
        {
            startFight = true;
        }

        if (startFight)
        {
            switchState = 1;
        }

        if(stateMachine.currentState == Uno.Instance && count >= 5)
        {
            switchState = 2;
        }

        CheckIfTimeToFire();
        stateMachine.Update();
    }

    void FixedUpdate()
    {

    }

    void CheckIfTimeToFire()
    {
        count++;
        if(Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bossRoom.position, radius);
        Gizmos.DrawWireSphere(attackPos.position, 5f);
    }

}
