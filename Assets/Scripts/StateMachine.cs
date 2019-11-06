using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentlyRunningState;
    public IState previouslyRunningState;


    public void ChangeState(IState newState)
    {
        if (currentlyRunningState != null)
        {
            currentlyRunningState.Exit();
        }

        previouslyRunningState = currentlyRunningState;

        currentlyRunningState = newState;
        currentlyRunningState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        IState runningState = currentlyRunningState;
        if (runningState != null)
        {
            runningState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        currentlyRunningState.Exit();
        currentlyRunningState = previouslyRunningState;
        currentlyRunningState.Enter();
    }
}
