using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreWeThereYet : MonoBehaviour
{
    public float timeDelay;
    private float nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        // Add the current time to timeDelay, cannot do time event until current time + timeDelay time is met
        nextEventTime = Time.time + timeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // If the current time is greater than or equal to the time of the next event, print time is up
        if (Time.time >= nextEventTime)
        {
            Debug.Log("Time is up!");
            // Reset timer
            nextEventTime = Time.time + timeDelay;
        }
    }
}
