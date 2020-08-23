using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight: MonoBehaviour, IDamageable
{
    public float health {get; set;} = 2000f;
    public GameObject gObj {get; set;}
    public Rigidbody rb;
    public CapsuleCollider col;
    public Vector3 inputDir;
    private float gravity = -0.5f;
    private float friction = 25f;
    private float airAccelerate = 10f;
    private float groundAccelerate = 200f;
    private float maxVelocityAir = 10;
    private float maxVelocityGround = 5;
    private float maxFallSpeed = -30;
    public LayerMask jumpMask;

    public Vector3 lastPos;
    public Vector3 nextPos;
    public GameObject player;
    private NavMeshPath path;

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
        if( speed != 0 )
        {
            float drop = speed * friction * Time.fixedDeltaTime;
            prevVelocity *= Mathf.Max(speed - drop, 0) / speed;
        }
        Vector3 newVel = new Vector3();
        newVel = Accelerate(accelDir, prevVelocity, groundAccelerate, maxVelocityGround);
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

    public void Die()
    {
        Destroy(this.gObj);
    }

    public void Hit(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }

    void Awake()
    {
        gObj = this.gameObject;
        col = this.GetComponent<CapsuleCollider>();
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("player");
        lastPos = transform.position;
        nextPos = Vector3.zero;
        path = new NavMeshPath();
        InvokeRepeating("FindPath", 0f, 1f);
    }

    void FixedUpdate()
    {
        if(nextPos != Vector3.zero)
        {
            inputDir = Vector3.Normalize(nextPos - transform.position);
        }
        if(Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.1f, jumpMask))
        {
            rb.velocity = MoveGround(inputDir, rb.velocity);
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

    void FindPath()
    {
        NavMeshHit targetPos;
        NavMesh.SamplePosition(player.transform.position, out targetPos, 500f, NavMesh.AllAreas);
        if(NavMesh.CalculatePath(transform.position, targetPos.position, NavMesh.AllAreas, path))
        {
            lastPos = transform.position;
            nextPos = new Vector3(path.corners[1].x, transform.position.y, path.corners[1].z);
        }
        else
        {
            inputDir = Vector3.zero;
        }
    }

}

