using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public Pawn pawn;

    public float fieldOfView;

    public float maxViewDistance;
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
}
