using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider col;
    public Vector3 inputDir;
    public bool isJumping;
    private float gravity = -0.5f;
    private float friction = 25f;
    private float airAccelerate = 10f;
    private float groundAccelerate = 200;
    private float maxVelocityAir = 50;
    private float maxVelocityGround = 50;
    private float jumpVelocity = 7;
    private float maxFallSpeed = -30;
    public LayerMask jumpMask;

    private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float maxVelocity)
    {
        float projVel = Vector3.Dot(prevVelocity, accelDir);
        float accelVel = accelerate * Time.fixedDeltaTime;

        if(projVel + accelVel > maxVelocity)
        {
            accelVel = maxVelocity - projVel;
        }
        return prevVelocity + accelDir * accelVel;
    }

    private Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
    {
        float speed = prevVelocity.magnitude;
        if( speed != 0 && !isJumping)
        {
            float drop = speed * friction * Time.fixedDeltaTime;
            prevVelocity *= Mathf.Max(speed - drop, 0) / speed;
        }
        Vector3 newVel = new Vector3();

        newVel = isJumping == true ? Accelerate(accelDir, prevVelocity, 0f , maxVelocityGround) 
                                   : Accelerate(accelDir, prevVelocity, groundAccelerate, maxVelocityGround);
        return newVel;
    }

    private Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
    {
        Vector3 newVel = Accelerate(accelDir, prevVelocity, airAccelerate, maxVelocityAir);
        if(newVel.y >= maxFallSpeed)
        {
            newVel += new Vector3(0, gravity, 0);
        }
        return newVel;
    }

    void Awake()
    {
        col = this.GetComponent<CapsuleCollider>();
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.1f, jumpMask))
        {
            rb.velocity = MoveGround(inputDir, rb.velocity);
            if(isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x,jumpVelocity,rb.velocity.z);
            }
        }
        else
        {
            rb.velocity = MoveAir(inputDir, rb.velocity);
        }
        if(rb.velocity.magnitude < 0.01f)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
