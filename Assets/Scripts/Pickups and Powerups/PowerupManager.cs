using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    public List<Powerup> removePowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removePowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerups.Count > 0)
        {
            DecrementPowerupTimers();
        }
    }

    void LateUpdate()
    {
        if (removePowerupQueue.Count > 0)
            ApplyRemovePowerupsQueue();    
    }

    public void Add(Powerup powerupToAdd)
    {
        // Apply the powerup
        powerupToAdd.Apply(this);

        // Add the powerup to the list
        powerups.Add(powerupToAdd);
    }

    public void Remove(Powerup powerupToRemove)
    {
        // Remove the powerup
        powerupToRemove.Remove(this);

        // Remove from list 
        removePowerupQueue.Add(powerupToRemove);
    }

    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            // If this powerup is timed
            if (!powerup.isPermanent)
            {
                // Decrease the timer
                powerup.duration -= Time.deltaTime;

                // Check if it's time to remove it
                if (powerup.duration <= 0)
                {
                    Remove(powerup);
                }
            }
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        foreach (Powerup powerup in removePowerupQueue)
        {
            powerups.Remove(powerup);
        }

        removePowerupQueue.Clear();
    }
}
