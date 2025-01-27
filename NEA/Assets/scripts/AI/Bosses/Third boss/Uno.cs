using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Uno : State<ThirdBoss>
{
    //THe patrolling state for my basic enemy
    private static Uno _instance;

    //creates only one instance of the state to reduce computational expense
    private Uno()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Uno Instance
    {
        get
        {
            if (_instance == null)
            {
                new Uno();
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
        if (_owner.switchState == 2)
        {
            _owner.stateMachine.ChangeState(Dos.Instance);

        }

    }
}
