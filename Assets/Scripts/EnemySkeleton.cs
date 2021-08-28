using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class EnemySkeleton : EnemyWalkingPathfinder {
    public Animator animator;
    public float damage = 20, knockback = 5, knockbackVerical = 2;
    protected override void Spawn()
    {
        base.Spawn();
        target = PlayerManager.Instance.controller.transform;
    }

    protected override void Life()
    {
        base.Life();

        animator.SetBool("isGrounded", enemyController.isGrounded);
        animator.SetFloat("speed", enemyController.rb.velocity.magnitude);

    }

    protected virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 6) {
            Slap();
        }
    }

    protected virtual void Slap() {
        animator.SetTrigger("attack");
        gameObject.TweenDelayedInvoke(0.2f,()=>PlayerManager.Instance.Hit(damage));
        PlayerManager.Instance.controller.rb.AddForce(transform.forward * knockback + Vector3.up * knockbackVerical, ForceMode.Impulse);
    }

    protected override void Die() {
        DropCash();

        Destroy(gameObject);
    }
}