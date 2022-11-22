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
            /*case AIState.ChooseTarget:
                // Do the actions of the choose target state
                DoChooseTargetState();
                // Check for transition
                if (target != null)
                {
                    ChangeState(AIState.ChooseTarget);
                }*/

            case AIState.Idle:
                // Do the actions of the idle state
                DoIdleState();
                // Check for transitions 
                if (target == null)
                {
                    // Set the target to player one
                    TargetPlayerOne();
                }
                if (IsDistanceLessThan(target, followDistance))
                { 
                    ChangeState(AIState.Guard);
                }
                break;
            case AIState.Guard:
                // Do the actions of the guard state
                DoGuardState();
                // Check for transitions
                if (target == null)
                {
                    // Set the target to player one
                    TargetPlayerOne();
                }
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

