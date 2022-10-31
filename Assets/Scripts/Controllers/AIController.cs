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

    public enum AIState { Guard, Idle, Seek, Attack, Chase, Patrol, Flee };

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
    public int currentWaypoint = 0;

    // The area we can see in
    public float fieldOfView;

    // The maximum distance we can see the player
    public float maxViewDistance;

    // The distance the noise volume is
    public float volumeDistance;

    // The distance we can hear
    public float hearingDistance;

    #endregion Variables

    #region MonoBehaviour
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        ChangeState(AIState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    #endregion MonoBehaviour

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
        // Find the target's transform
        Seek(target.transform);
    }

    public void Seek(Pawn targetPawn)
    {
        // Find the target
        Seek(targetPawn.gameObject);
    }

    public void Seek(Transform targetTransform)
    {
        // Find the target's transform position
        Seek(targetTransform.position);
    }

    public void Seek(Vector3 targetPos)
    {
        // Rotate towards the target's position 
        pawn.RotateTowards(targetPos);
        // Move towards the target
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
        // If the waypoints distance is more than the current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Find the next waypoint from the current waypoint
            Seek(waypoints[currentWaypoint]);

            // If the distance of the pawn and the waypoint position's is less than the stop distance
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                // increment the current waypoint
                currentWaypoint++;
            }
        }
        else
        {
            // The current waypoint is zero
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }
    #endregion Attacks

    #region Targeting

    /// <summary>
    /// If there is more than one player, target the first player
    /// </summary>
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

    /// <summary>
    /// Target the closest tank to the AI tank
    /// </summary>
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

    protected virtual void DoGuardState()
    {
        //Shoot
        pawn.Shoot();
        Debug.Log("Guard");
    }
    #endregion States

    #region Conditions/Transitions
    protected virtual void DoFleeState()
    {

    }

    /// <summary>
    /// Check if the pawn and the target's postion and transform are less than the distance
    /// </summary>
    /// <param name="target"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
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

    #region Vision
    public bool CanSee(GameObject target)
    {

        // Find the vector from the agent to the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        // if that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion Vision

    #region Hearing 
    public bool CanHear(GameObject target)
    {
        // Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        // If they don't have one, they can't make noise, so return false
        if (noiseMaker == null)
        {
            return false;
        }
        // If they are making 0 noise, they also can't be heard
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // ... then we can hear the target
            Debug.Log("Hear");
            return true;
        }
        else
        {
            // Otherwise, we are too far away to hear them
            Debug.Log("Too Far");
            return false;
        }
    }
    #endregion Hearing
}


