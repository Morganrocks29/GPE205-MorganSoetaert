using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    /// <summary>
    /// Value for movement speed
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Value for the turn speed
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// This pawn's starting health
    /// </summary>
    public float maxHealth;

    /// <summary>
    /// This pawn's current health
    /// </summary>
    protected float _currentHealth;

    /// <summary>
    /// The component that handles this pawn's movement
    /// </summary>
    protected Mover mover;

    protected Shooter shooter;

    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;

    // Start is called before the first frame update
   protected virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    /// <summary>
    /// Takes user input and tells the Mover how to move
    /// </summary>

    public abstract void MoveForward();

    public abstract void MoveBackward();

    public abstract void RotateClockwise();

    public abstract void RotateCounterclockwise();

    public abstract void TakeDamage(float damage);

    public abstract void Shoot();

    public abstract void Die();
}
