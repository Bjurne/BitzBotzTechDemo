using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timeToIdle;
    private float timeSpentIdling;

    private System.Action<IdlingResults> idlingResultsCallback;

    public IdleState(float timeToIdle = 0f, System.Action<IdlingResults> idlingResultsCallback = null)
    {
        this.timeToIdle = timeToIdle;
        this.idlingResultsCallback = idlingResultsCallback;
    }

    public void Enter()
    {
        timeSpentIdling = 0f;
        //Debug.Log("New state - Idle - " + timeToIdle + " seconds.");
    }

    public void Execute()
    {
        if (timeToIdle != 0f)
        {
            if (timeSpentIdling < timeToIdle)
            {
                timeSpentIdling += Time.deltaTime;
            }
            else
            {
                var idlingResults = new IdlingResults(timeSpentIdling);
                idlingResultsCallback(idlingResults);
            }
        }
    }

    public void Exit()
    {
        //Debug.Log("leaving state - Idle");
    }

}

public class IdlingResults
{
    public float timeIdled;

    public IdlingResults(float timeIdled)
    {
        this.timeIdled = timeIdled;
    }
}
