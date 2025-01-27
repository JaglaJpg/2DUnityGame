using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SleepState : State<firstBossAI>
{
    //THe patrolling state for my basic enemy
    private static SleepState _instance;

    //creates only one instance of the state to reduce computational expense
    private SleepState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SleepState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SleepState();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(firstBossAI _owner)
    {
        Debug.Log("zzz");
    }
    //tells me when state has been exited
    public override void ExitState(firstBossAI _owner)
    {
        Debug.Log("!!!");
    }
    //switches to other state when condition is met
    public override void UpdateState(firstBossAI _owner)
    {
        if (_owner.switchState == 1)
        {
            _owner.stateMachine.ChangeState(FirstPhase.Instance);

        }

    }
}
