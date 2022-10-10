using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

  public void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the colliding object
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // deal damage to the object if it has a Health component
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone, owner);
        }

        Destroy(gameObject);
    }

}
