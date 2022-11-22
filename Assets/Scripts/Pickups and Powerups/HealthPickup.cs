using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public HealthPowerup powerup;

    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
    }
    public void OnTriggerEnter(Collider other)
    {
        // varaiable to store other object's PowerupController - if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        if (powerupManager != null)
        {
            // Add the pickup
            powerupManager.Add(powerup);
            Debug.Log("Add Pickup");

            // Destroy this pickup
            Destroy(gameObject);
            Debug.Log("Destroy Pickup");
        }
    }
}
