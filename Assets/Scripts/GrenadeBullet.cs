using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class GrenadeBullet : Bullet {
    public float timeToExplode = 2f;
    public GameObject explosion;

    protected override void OnCollisionEnter(Collision other) {
        gameObject.TweenDelayedInvoke(timeToExplode, ()=>Explode());
    }

    private void Explode() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
} 
