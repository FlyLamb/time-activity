using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    protected Rigidbody rb;
 [SerializeField]
    protected GameObject particles;

    private void Start() {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot(Vector3 direction, float force) {
        if(rb != null) rb.AddForce(direction.normalized * force);
    }

    protected virtual void OnCollisionEnter(Collision other) {
        if(particles!=null) Instantiate(particles, transform.position, Quaternion.LookRotation(other.contacts[0].normal));
        Destroy(gameObject);
    }
}