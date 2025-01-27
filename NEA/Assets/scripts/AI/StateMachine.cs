using UnityEngine;

namespace StateStuff
{
    //main code for a state machine for all AI in my game
    public class StateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T Owner;

        
        public StateMachine(T _o)
        {
            Owner = _o;
            currentState = null;
        }

        //method for change the state of the state machine
        public void ChangeState(State<T> _newstate)
        {
            if(currentState != null)
                currentState.ExitState(Owner);
            currentState = _newstate;
            currentState.EnterState(Owner);
        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState(Owner);

        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T _owner);

        public abstract void ExitState(T _owner);

        public abstract void UpdateState(T _owner);
    }
}
