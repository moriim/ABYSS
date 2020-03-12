using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public GameObject grenadeExplosion;
    public SphereCollider col;
    public Rigidbody rb;
    public GameObject owner;
    private float gravity = -0.75f;

    void Awake()
    {
        Destroy(this.gameObject, 2.5f);
    }

    void FixedUpdate()
    {
        rb.velocity += new Vector3(0, gravity, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3( rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z * 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        GameObject explosion = Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<GrenadeExplosion>().owner = owner;
    }
}
