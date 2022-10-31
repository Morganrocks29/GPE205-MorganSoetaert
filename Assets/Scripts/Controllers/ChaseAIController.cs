using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIController : AIController
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
        MakeDecisions();

        base.Update();
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
                if (IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                // Do the actions of the chase state
                DoChaseState();
                // Check for transitions
                if (!IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            default:
                Debug.LogError("The switch could not determine the current state.");
                break;
        }
    }
}
