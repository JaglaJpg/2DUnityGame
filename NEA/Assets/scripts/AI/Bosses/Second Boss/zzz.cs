using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class zzz : State<SecondBoss>
{
    //THe patrolling state for my basic enemy
    private static zzz _instance;

    //creates only one instance of the state to reduce computational expense
    private zzz()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static zzz Instance
    {
        get
        {
            if (_instance == null)
            {
                new zzz();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(SecondBoss _owner)
    {
        Debug.Log("zzz");
    }
    //tells me when state has been exited
    public override void ExitState(SecondBoss _owner)
    {
        Debug.Log("!!!");
    }
    //switches to other state when condition is met
    public override void UpdateState(SecondBoss _owner)
    {
        if (_owner.switchState == 1)
        {
            _owner.stateMachine.ChangeState(Jeden.Instance);

        }

    }
}
