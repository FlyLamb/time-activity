using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float force = 200;
    public float radius = 5;
    public float damage = 10;

    public bool explodeOnPlay = true;

    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        var w = ps.main;
        w.playOnAwake = false;
        if(explodeOnPlay) Explode();
    }

[ContextMenu("Boom")]
    public void Explode() {
        ps.Play();
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in cols)
        {
            if(item.GetComponent<Rigidbody>())
                item.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position, radius, 1.2f);
            if(item.GetComponent<Enemy>())
                item.GetComponent<Enemy>().Hit(damage);
        }
    }
}

