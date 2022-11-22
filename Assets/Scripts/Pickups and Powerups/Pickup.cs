using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float rotateSpeed;
    public float bobSpeed;
    public AnimationCurve bobCurve;

    // Start is called before the first frame update
    protected virtual void Start()
    {
       
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Rotate();
        Bob();
    }
    protected virtual void Rotate()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    protected virtual void Bob()
    {
        transform.position = new Vector3(transform.position.x, (bobSpeed * bobCurve.Evaluate((Time.time % bobCurve.length))), transform.position.z); 
    }

   /* public void OnTriggerEnter(Collider other)
    {
        // varaiable to store other object's PowerupController - if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        if (powerupManager != null)
        {
            // Add the pickup
            powerupManager.Add(powerup);

            // Destroy this pickup
            Destroy(gameObject);
        }
    } */
}
