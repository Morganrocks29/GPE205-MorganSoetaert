using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{

    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterclockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null && GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this);
            }

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        ProcessInputs();
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.players != null)
        {
            GameManager.instance.players.Remove(this);
        }
    }

    public override void ProcessInputs()
    {
        // If the move forward key is pressed, move the pawn forward
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        // If the move backward key is pressed, move the pawn backward
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        // If the rotate clockwise key is pressed, have the pawn rotate clockwise
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        // If the rotate counterclockwise key is pressed, have the pawn rotate counterclockwise
        if (Input.GetKey(rotateCounterclockwiseKey))
        {
            pawn.RotateCounterclockwise();
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }

}
