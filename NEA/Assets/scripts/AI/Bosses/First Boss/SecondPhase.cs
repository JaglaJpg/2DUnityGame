using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SecondPhase : State<firstBossAI>
{
    //THe patrolling state for my basic enemy
    private static SecondPhase _instance;

    //creates only one instance of the state to reduce computational expense
    private SecondPhase()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondPhase Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondPhase();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(firstBossAI _owner)
    {
        Debug.Log("Second");
    }
    //tells me when state has been exited
    public override void ExitState(firstBossAI _owner)
    {
        Debug.Log("Bye second");
    }
    //switches to other state when condition is met
    public override void UpdateState(firstBossAI _owner)
    {

    }
}
