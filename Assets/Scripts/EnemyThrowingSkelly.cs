using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class EnemyThrowingSkelly : EnemyWalkingPathfinder
{
    public Animator animator;
    public Bullet projectile;
    public float force = 1000;
    public float shootInterval = 0.4f;
    private float _shootInterval;
    public LayerMask aimMask;
    public Transform shootPoint;

    protected override void Spawn()
    {
        base.Spawn();
        target = new GameObject("P").transform;
    }

    protected override void Life()
    {
        base.Life();

        animator.SetBool("isGrounded", enemyController.isGrounded);
        animator.SetFloat("speed", enemyController.rb.velocity.magnitude);
        target.position = PlayerManager.Instance.controller.transform.position + PlayerManager.Instance.controller.transform.right * 20; 
        if(_shootInterval <= 0) {
            if(!Physics.Linecast(transform.position, PlayerManager.Instance.transform.position, aimMask))
            gameObject.TweenDelayedInvoke(1,()=>Fire());
            _shootInterval = shootInterval;  
            
        }

        if(shootInterval > 0)
            _shootInterval -= Time.deltaTime;

    }

    protected virtual void Fire() {
        animator.SetTrigger("cast");
        var w = Instantiate(projectile, shootPoint.position + transform.forward, Quaternion.identity).GetComponent<Bullet>();
        var dir = (PlayerManager.Instance.transform.position - transform.position).normalized;
        w.Shoot(dir, force );

        
    }

    protected override void Die() {
        DropCash();

        Destroy(gameObject);
    }
}
