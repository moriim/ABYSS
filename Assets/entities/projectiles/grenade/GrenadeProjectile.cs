using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public GameObject grenadeExplosion;
    public SphereCollider col;
    public Rigidbody rb;
    public GameObject owner;
    public LayerMask groundMask;
    private float gravity = -0.75f;

    void Awake()
    {
        Destroy(this.gameObject, 2.5f);
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(rb.velocity.y) < 5 && Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.01f, groundMask))
        {
            rb.velocity = new Vector3(0,0,0);
            gravity = 0f;
        }
        rb.velocity += new Vector3(0, gravity, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3( rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z * 0.5f);
        if(other.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            GameObject explosion = Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
            explosion.GetComponent<GrenadeExplosion>().owner = owner;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<GrenadeExplosion>().owner = owner;
        Destroy(this.gameObject);
    }
}
