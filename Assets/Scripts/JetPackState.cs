using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackState : IState
{
    protected Vector2 jumpVector;
    Rigidbody2D jumpingBody;
    StateMachine stateMachine;
    PlayerController playerController;
    float movementSpeed;

    public JetPackState(Rigidbody2D jumpingBody, PlayerController playerController, Vector2 jumpVector, StateMachine stateMachine)
    {
        this.jumpingBody = jumpingBody;
        this.jumpVector = jumpVector;
        this.stateMachine = stateMachine;
        this.playerController = playerController;
        movementSpeed = playerController.movementSpeed;
    }

    public void Enter()
    {
        Debug.Log("New state - JetPackState");
        GameObject.FindObjectOfType<MonoScript>().StartCoroutine(JetPack());
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        playerController.playerRigidBody.AddForce(new Vector2(x, 0f));
    }

    public void Exit()
    {
        Debug.Log("Leaving state - JetPackState");
    }

    public IEnumerator JetPack()
    {
        PlayerController playerController = jumpingBody.GetComponent<PlayerController>();
        playerController.aerialStage += 5;

        for (int i = 0; i < 30; i++)
        {
            if (playerController.grounded) yield break;
            if (jumpingBody.velocity.y < 7f && jumpingBody.velocity.x < 7f)
            {
                Vector2 direction = playerController.activeWeaponSlot.right;
                playerController.hoverEffectPS.Play();
                jumpingBody.AddForce(direction * (jumpVector.y / 2));
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
