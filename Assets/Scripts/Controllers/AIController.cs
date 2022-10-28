using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    #region Variables
    /// <summary>
    /// The last time we changed states
    /// </summary>
    public float lastStateChangeTime;

    public enum AIState { Guard, Idle, Seek, Attack, Chase };

    /// <summary>
    /// The state that our FSM is currently in 
    /// </summary>
    public AIState currentState;

    /// <summary>
    /// The AIController's current target
    /// </summary>
    public GameObject target;

    /// <summary>
    /// The distance the AI will get from the player
    /// </summary>
    public float followDistance;

    /// <summary>
    /// The distance the AI will get from the player when chasing
    /// </summary>
    public float fleeDistance;

    /// <summary>
    /// The waypoints we patrol between
    /// </summary>
    public Transform[] waypoints;

    /// <summary>
    /// The distance we get from the next waypoint before stopping
    /// </summary>
    public float waypointStopDistance;

    /// <summary>
    /// The waypoint we are currently at
    /// </summary>
    private int currentWaypoint = 0;
    #endregion Variables

    #region MonoBehaviour
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //ChangeState(AIState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDecisions();

        base.Update();
    }
    #endregion MonoBehaviour

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do the actions of the idle state
                DoIdleState();
                // Check for transitions 
                if (IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                // Do the actions of the chase state
                DoChaseState();
                // Check for transitions
                /*if (!IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Idle);
                }*/
                break;
            default:
                Debug.LogError("The switch could not determine the current state.");
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        // Change the current state
        currentState = newState;

        // Savethe last time that we changed states
        lastStateChangeTime = Time.time;
    }

    #region Actions
    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);

        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.gameObject);
    }

    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.gameObject);
    }

    public void Seek(Vector3 targetPos)
    {
        pawn.RotateTowards(targetPos);
        pawn.MoveForward();
    }
    protected void Flee()
    {
        //Find the vector to the target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        //Find the vector away from the target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        //Find the vector we travel down to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * flippedPercentOfFleeDistance;
        //Seek the point we want to move to flee from the target
        Seek(fleeVector);
      
    }

    protected virtual void Patrol()
    {
        if (waypoints.Length > currentWaypoint)
        {
            Seek(waypoints[currentWaypoint]);

            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }
    #endregion Attacks

    #region Targeting
    protected virtual void TargetPlayerOne()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                if (GameManager.instance.players.Count > 0)
                {
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected virtual void TargetHarvestTank()
    {
        Pawn closestTank;
        float closestTankDistance;

        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                if (GameManager.instance.players.Count > 0)
                {
                    closestTank = GameManager.instance.players[0].pawn;
                    closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

                    foreach (PlayerController player in GameManager.instance.players)
                    {
                        if (Vector3.Distance(pawn.transform.position, player.pawn.transform.position) <= closestTankDistance)
                        {
                            closestTank = player.pawn;
                            closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                        }
                    }

                    target = closestTank.gameObject;
                }
            }
        }
    }
    #endregion Targeting

    #region States
    public void DoSeekState()
    {
        Seek(target);
    }
    protected void DoChaseState()
    {
        Seek(target);
    }

    protected void DoIdleState()
    {
        // Do nothing
        Debug.Log("Idle.state...");
    }

    protected virtual void DoAttackState()
    {
        //Chase
        Seek(target);
        //Shoot
        pawn.Shoot();
        Debug.Log("Attack!");
    }
    #endregion States

    #region Conditions/Transitions
    protected virtual void DoFleeState()
    {

    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            Debug.Log("Trigger");
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool IsHasTarget()
    {
        return target != null;
    }
    #endregion Conditions/Transitions
}
