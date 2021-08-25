using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    protected Rigidbody rb;

    private void Start() {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot(Vector3 direction, float force) {
        if(rb != null) rb.AddForce(direction.normalized * force);
    }

    protected virtual void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}