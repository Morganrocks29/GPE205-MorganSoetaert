using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    public override void Start()
    {
        m_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Get the direction and magnitude of our movement and add it to the RigidBody
    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + moveVector);
    }

    public override void Rotate(float turnSpeed)
    {
        transform.Rotate(0, turnSpeed, 0);
    }
}
