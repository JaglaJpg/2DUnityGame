using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Dwa : State<SecondBoss>
{
    //THe patrolling state for my basic enemy
    private static Dwa _instance;

    //creates only one instance of the state to reduce computational expense
    private Dwa()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static Dwa Instance
    {
        get
        {
            if (_instance == null)
            {
                new Dwa();
            }
            return _instance;
        }
    }
    //tells me when the state has been entered 
    public override void EnterState(SecondBoss _owner)
    {
        Debug.Log("Second");
    }
    //tells me when state has been exited
    public override void ExitState(SecondBoss _owner)
    {
        Debug.Log("Bye second");
    }
    //switches to other state when condition is met
    public override void UpdateState(SecondBoss _owner)
    {

    }
}
