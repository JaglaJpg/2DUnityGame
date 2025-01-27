using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Jeden : State<SecondBoss>
{
    //THe patrolling state for my basic enemy
    private static Jeden _instance;

    //creates only one instance of the state to reduce computational expense
    private Jeden()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Jeden Instance
    {
        get
        {
            if (_instance == null)
            {
                new Jeden();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(SecondBoss _owner)
    {
        Debug.Log("First");
        
    }
    //tells me when state has been exited
    public override void ExitState(SecondBoss _owner)
    {
        Debug.Log("Bye first");
    }
    //switches to other state when condition is met
    public override void UpdateState(SecondBoss _owner)
    {
        if (_owner.switchState == 2)
        {
            _owner.stateMachine.ChangeState(Dwa.Instance);

        }
    }
}

