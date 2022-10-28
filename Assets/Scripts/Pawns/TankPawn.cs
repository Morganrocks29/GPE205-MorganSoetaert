using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // A move forward function
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    // A move backward function
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    // A rotate clockwise function
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    // A rotate counterclockwise function
    public override void RotateCounterclockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        // Find the vector to our target position 
        Vector3 vectorToTarget = targetPosition - transform.position;

        // Find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        // Rotate closer to that vector
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed = Time.deltaTime);
    }

    // A function for taking damage
    public override void TakeDamage(float damage)
    {
        // The current health will equal the current health at that time minus the damage
        _currentHealth -= damage;

        // If the current health is equal to or less than 0, destroy this object
        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Shoot()
    {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
    }

    // When an object calls this function, the object will be destroyed
    public override void Die()
    {
        Destroy(this.gameObject);
    }

}
