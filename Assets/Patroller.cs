using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public BoxCollider2D xPlusTrigger;
    public BoxCollider2D xMinusTrigger;

    WeaponController weaponController;
    Rigidbody2D patrollerRigidBody;

    public bool goRight;
    public float patrollingSpeed;
    private float patrollingInput;

    private int objectsToTheRight;
    private int objectsToTheLeft;

    private StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        patrollerRigidBody = GetComponent<Rigidbody2D>();

        stateMachine.ChangeState(new IdleState(5, StopIdling));
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea") && collision.name != "BitzBox")
    //    {
    //        if (collision.IsTouching(xPlusTrigger))
    //        {
    //            goRight = false;
    //            patrollerRigidBody.transform.Translate(-patrollingSpeed / 10f, 0, 0);
    //        }
    //        if (collision.IsTouching(xMinusTrigger))
    //        {
    //            goRight = true;
    //            patrollerRigidBody.transform.Translate(patrollingSpeed / 10f, 0, 0);
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea") && collision.name != "BitzBox")
        {
            if (collision.IsTouching(xPlusTrigger))
            {
                //objectsToTheRight++;
                // TODO if projectile - new State evade(directionOfProjectile, Investigate - patroll in the direction the shot came from)
                //if (collision.attachedRigidbody.velocity.x < 0f)
                if (CheckForClearPatrollingDirection()) StartPatrolling();
            }
            if (collision.IsTouching(xMinusTrigger))
            {
                //objectsToTheLeft++;
                if (CheckForClearPatrollingDirection()) StartPatrolling();
            }
        }
    }

    private bool CheckForClearPatrollingDirection()
    {
        patrollingInput = 0f;

        //if (objectsToTheLeft < 1)
        //{
        //    patrollingInput = -1f;
        //    Debug.Log("Objects to the left: " + objectsToTheLeft);
        //    return true;
        //}
        //else if (objectsToTheRight < 1)
        //{
        //    patrollingInput = 1f;
        //    Debug.Log("Objects to the right: " + objectsToTheRight);
        //    return true;
        //}
        //else return false;

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.NameToLayer("default")); // <---- TODO This isn't really done properly. NameToLayer(köttbulle) works just as well.
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;
        Collider2D[] collidersToTheLeft = new Collider2D[5];
        Collider2D[] collidersToTheRight = new Collider2D[5];
        //if (xMinusTrigger.OverlapCollider(contactFiler, collidersToTheLeft) < 1 )
        int gosLeft = Physics2D.OverlapCollider(xMinusTrigger, contactFilter, collidersToTheLeft);
        int gosRight = Physics2D.OverlapCollider(xPlusTrigger, contactFilter, collidersToTheRight);
        //Debug.Log("gosLeft: " + gosLeft + " | gosRight: " + gosRight);
        if (gosLeft < 1)
        {
            patrollingInput = -1f;
            return true;
        }
        //else if (xPlusTrigger.OverlapCollider(contactFiler, collidersToTheRight) < 1)
        else if (gosRight < 1)
        {
            patrollingInput = 1f;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartPatrolling()
    {
        //Debug.Log("Starting new patroll, input: " + patrollingInput);
        stateMachine.ChangeState(new PatrollingState(patrollerRigidBody, patrollingInput, patrollingSpeed));
    }

    private void StopIdling(IdlingResults idlingResults)
    {
        //Debug.Log(gameObject.name + " spent " + idlingResults.timeIdled + " seconds idling.");

        if (CheckForClearPatrollingDirection())
        {
            StartPatrolling();
        }
        else
        {
            //Debug.Log("No clear area to partoll, idling again...");
            stateMachine.ChangeState(new IdleState(5f, StopIdling));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea") && collision.name != "BitzBox")
        {
            if (collision.IsTouching(xPlusTrigger)) // <--- ofc this doesnt work :(
            {
                //objectsToTheRight--;
            }
            if (collision.IsTouching(xMinusTrigger))
            {
                //objectsToTheLeft--;
            }
        }
    }


    private void Update()
    {
        if (weaponController.acquiredTarget == null)
        {
            stateMachine.ExecuteStateUpdate();
        }

    }
}
