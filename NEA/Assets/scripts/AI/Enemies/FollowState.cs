using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FollowState : State<NormalEnemy>
{
    //THe patrolling state for my basic enemy
    private static FollowState _instance;

    //creates only one instance of the state to reduce computational expense
    private FollowState()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FollowState Instance
    {
        get
        {
            if(_instance == null)
            {
                new FollowState();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(NormalEnemy _owner)
    {
        Debug.Log("Hi Follow");
    }
    //tells me when state has been exited
    public override void ExitState(NormalEnemy _owner)
    {
        Debug.Log("Bye Follow");
    }
    //switches to other state when condition is met
    public override void UpdateState(NormalEnemy _owner)
    {
        if (_owner.switchState == true)
        {
            _owner.stateMachine.ChangeState(PatrolState.Instance);
            
        }
    }
}
