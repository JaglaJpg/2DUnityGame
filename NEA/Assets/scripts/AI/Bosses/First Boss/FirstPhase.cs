using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FirstPhase : State<firstBossAI>
{
    //THe patrolling state for my basic enemy
    private static FirstPhase _instance;

    //creates only one instance of the state to reduce computational expense
    private FirstPhase()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstPhase Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstPhase();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(firstBossAI _owner)
    {
        Debug.Log("First");
        
    }
    //tells me when state has been exited
    public override void ExitState(firstBossAI _owner)
    {
        Debug.Log("Bye first");
        _owner.transform.Translate(0f, 3f, 0f);
    }
    //switches to other state when condition is met
    public override void UpdateState(firstBossAI _owner)
    {
        if (_owner.switchState == 2)
        {
            _owner.stateMachine.ChangeState(SecondPhase.Instance);

        }
    }
}
