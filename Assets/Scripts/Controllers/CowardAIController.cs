using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardAIController : AIController
{
    #region MonoBehaviour
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        ChangeState(AIState.Patrol);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (pawn != null)
        {
            MakeDecisions();

            base.Update();

            CanHear(target);
        }
    }
    #endregion MonoBehaviour

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                // Do the actions of the patrol state
                Patrol();
                // Check for transitions 
                if (target == null)
                {
                    // Set the target to player one
                    TargetPlayerOne();
                }
                if (IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                // Do the actions of the flee state
                Flee();
                // Check for transitions
                if (target == null)
                {
                    // Set the target to player one
                    TargetPlayerOne();
                }
                if (!IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            default:
                Debug.LogError("No flee.");
                break;
        }
    }
}