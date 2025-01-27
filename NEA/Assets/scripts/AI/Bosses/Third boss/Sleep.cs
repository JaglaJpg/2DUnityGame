using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Sleep : State<ThirdBoss>
{
    //THe patrolling state for my basic enemy
    private static Sleep _instance;

    //creates only one instance of the state to reduce computational expense
    private Sleep()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Sleep Instance
    {
        get
        {
            if (_instance == null)
            {
                new Sleep();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(ThirdBoss _owner)
    {
        Debug.Log("zzz");
    }
    //tells me when state has been exited
    public override void ExitState(ThirdBoss _owner)
    {
        Debug.Log("!!!");
    }
    //switches to other state when condition is met
    public override void UpdateState(ThirdBoss _owner)
    {
        if (_owner.switchState == 1)
        {
            _owner.stateMachine.ChangeState(Uno.Instance);

        }

    }
}
