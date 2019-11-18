using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallingState : IState
{
    float timeToStall;
    StateMachine stateMachine;
    private float timeSpentStalling;

    private System.Action<StallingResults> stallingResultsCallback;

    public StallingState(StateMachine stateMachine, System.Action<StallingResults> stallingResultsCallback, float timeToStall = 0f)
    {
        this.stateMachine = stateMachine;
        this.stallingResultsCallback = stallingResultsCallback;
        this.timeToStall = timeToStall;
    }

    public void Enter()
    {
        timeSpentStalling = 0f;
        //Debug.Log("New state - Stalling - " + timeToStall + " seconds.");
    }

    public void Execute()
    {
        if (timeToStall != 0f)
        {
            if (timeSpentStalling < timeToStall)
            {
                timeSpentStalling += Time.deltaTime;
            }
            else
            {
                var stallingResults = new StallingResults(timeSpentStalling);
                if (stallingResultsCallback != null)
                {
                    stallingResultsCallback(stallingResults);
                }
                else
                {
                    Debug.LogWarning("Stalling State was not properly exited");
                    stateMachine.SwitchToPreviousState();
                }
            }
        }
    }

    public void Exit()
    {
        //stateMachine.currentlyRunningState = stateMachine.previouslyRunningState;
        //Debug.Log("leaving state - Stalling - Resetting Previous State");
        //Debug.Log("leaving state - StallingState");
    }
}

public class StallingResults
{
    public float timeStalled;

    public StallingResults(float timeStalled)
    {
        this.timeStalled = timeStalled;
    }
}
