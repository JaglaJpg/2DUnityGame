using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class NormalEnemy : MonoBehaviour
{
    //Code for my normal enemy's AI

    //all my variables
    public bool switchState = false;
    [SerializeField] public Transform target;
    [SerializeField] public float speed;
    [SerializeField] public float minDistance;
    [SerializeField] public Transform[] patrolPoints;
    [SerializeField] public float waitTime;
    int currentPointIndex;
    bool once;
    public SpriteRenderer demon;

    [SerializeField] public Damage script;

    public StateMachine<NormalEnemy> stateMachine { get; set; }

    //Starts in patrol state 
    void Start()
    {
        stateMachine = new StateMachine<NormalEnemy>(this);
        stateMachine.ChangeState(PatrolState.Instance);
        currentPointIndex = 0;
        demon = GetComponent<SpriteRenderer>();
    }

    //Update method updates every frame
    void Update()
    {
        //selection for switching state depending on distance from player
        if (Vector2.Distance(transform.position, target.position) > minDistance)
        {
            switchState = true;
        }

        else if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            switchState = false;
        }

        stateMachine.Update();

        //What to do in each state
        if (stateMachine.currentState == FollowState.Instance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (stateMachine.currentState == PatrolState.Instance)
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, (speed / 2) * Time.deltaTime);
            }
            else
            {
                if (once == false)
                {
                    once = true;
                    StartCoroutine(Wait());
                }
            }
        }

    }
    //method for taking damage
   
    //coroutine for changing color when taking damage
    IEnumerator damageEffect()
    {
        demon.color = new Color(150f, 5f, 5f, 255f);
        yield return new WaitForSeconds(.5f);
        demon.color = new Color(255f, 255f, 255f, 255f);
    }
    //coroutine for waiting when patroling between patrol points
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;

    }


}
