using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    
    public void SwitchState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter();
        }

   }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime);
        }
    }
}
