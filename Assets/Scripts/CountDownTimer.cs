using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public float timerDelay;
    private float timeUntilNextEvent;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextEvent = timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // When timer reaches zero, timer is reset
        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent <= 0)
        {
            Debug.Log("Countdown Expired");
            // Reset timer
            timeUntilNextEvent = timerDelay;
        }
    }
}
