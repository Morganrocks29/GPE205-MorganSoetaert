using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAIController : AIController
{
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
                    ChangeState(AIState.Guard);
                }
                break;
            case AIState.Guard:
                // Do the actions of the guard state
                DoGuardState();
                // Check for transitions
                if (!IsDistanceLessThan(target, followDistance))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            default:
                Debug.LogError("The switch could not determine the current state.");
                break;
        }
    }
}

