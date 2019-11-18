using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringState : IState
{
    Vector2 jumpVector;
    Rigidbody2D jumpingBody;
    StateMachine stateMachine;
    PlayerController playerController;
    float movementSpeed;

    public HoveringState(Rigidbody2D jumpingBody, PlayerController playerController, Vector2 jumpVector, StateMachine stateMachine)
    {
        this.jumpingBody = jumpingBody;
        this.jumpVector = jumpVector;
        this.stateMachine = stateMachine;
        this.playerController = playerController;
        movementSpeed = playerController.movementSpeed;
    }

    public void Enter()
    {
        //Debug.Log("New state - HoveringState");
        GameObject.FindObjectOfType<MonoScript>().StartCoroutine(Hover());
        //hovering = false;
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        playerController.playerRigidBody.AddForce(new Vector2(x, 0f));

        //if (!hovering)
        //{
        //    hovering = true;

        //    playerController.hoveringStage += 5;

        //    float timer = 0f;
        //    float timerGoal = 0.1f;
        //    int numberOfThrustsTotal = 30;
        //    int numberOfThrustsDone = 0;


        //    if (timer < timerGoal)
        //    {
        //        timer += Time.deltaTime;
        //    }
        //    else
        //    {
        //        if (jumpingBody.velocity.y < 5f)
        //        {
        //            playerController.hoverEffectPS.Play();
        //            jumpingBody.AddForce(jumpVector / 4);
        //            numberOfThrustsDone++;
        //        }
        //    }


        //    for (int i = 0; i < numberOfThrusts; i++)
        //    {
        //        if (timer < timerGoal)
        //        {
        //            if (playerController.grounded) break;
        //            timer += Time.deltaTime;
        //        }
        //        else
        //        {
        //            if (jumpingBody.velocity.y < 5f)
        //            {
        //                playerController.hoverEffectPS.Play();
        //                jumpingBody.AddForce(jumpVector / 4);
        //            }
        //        }
        //    }

        //    //for (int i = 0; i < 30; i++)
        //    //{
        //    //    if (playerController.grounded) break;
        //    //    if (jumpingBody.velocity.y < 5f)
        //    //    {
        //    //        playerController.hoverEffectPS.Play();
        //    //        jumpingBody.AddForce(jumpVector / 4);
        //    //    }
        //    //    while (timer < 0.1f)
        //    //    {
        //    //        timer = timer + Time.deltaTime;
        //    //    }
        //    //    timer = 0f;
        //    //}
        //}
    }

    public void Exit()
    {
        //Debug.Log("Leaving state - HoveringState");
    }

    public IEnumerator Hover()
    {
        PlayerController playerController = jumpingBody.GetComponent<PlayerController>();
        playerController.aerialStage += 5;

        for (int i = 0; i < 30; i++)
        {
            if (playerController.grounded) yield break;
            if (jumpingBody.velocity.y < 5f)
            {
                playerController.hoverEffectPS.Play();
                jumpingBody.AddForce(jumpVector / 4);
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
