using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class PatrolState : State<NormalEnemy>
{

    //THe patrolling state for my basic enemy
    private static PatrolState _instance;
    
    //creates only one instance of the state to reduce computational expense
    private PatrolState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static PatrolState Instance
    {
        get
        {
            if (_instance == null)
            {
                new PatrolState();
            }
            return _instance;
        }
    }

    //tells me when the state has been entered 
    public override void EnterState(NormalEnemy _owner)
    {
        Debug.Log("Hi patrol");
    }
    //tells me when state has been exited
    public override void ExitState(NormalEnemy _owner)
    {
        Debug.Log("Bye patrol");
    }
    //switches to other state when condition is met
    public override void UpdateState(NormalEnemy _owner)
    {
        if (_owner.switchState == false)
        {
            _owner.stateMachine.ChangeState(FollowState.Instance);
        }
    }
}
