using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class EnemyGliderCat : EnemyFlyingPathfinder
{

    public float speed;
    public float upwardModifier;
    public new Rigidbody rigidbody;

    public float bombInterval;
    private float _bombInterval;

    public GameObject bombObject;

    protected override void Spawn()
    {
        base.Spawn();
        target = new GameObject("C").transform;
        target.position = DumbPathing.instance.GetRandomPoint() + Vector3.up * upwardModifier
;
        _bombInterval = bombInterval;
    }

    protected override void Life()
    {
        base.Life();
        if (_bombInterval <= 0)
            DropBomb();
        else
            _bombInterval -= Time.deltaTime;
    }

    protected void DropBomb()
    {
        _bombInterval = bombInterval;
        Instantiate(bombObject, transform.position - transform.up, Quaternion.identity);
    }


    override protected void FixedUpdate()
    {
        if (health < 0) return;

        base.FixedUpdate();

        Vector3 direction = next - transform.position;
        Vector3 playerDirection = target.position - transform.position;
        playerDirection.Normalize();
        direction.Normalize();

        if (Vector3.Distance(transform.position, target.transform.position) < 5)
        {
            target.position = DumbPathing.instance.GetRandomPoint() + Vector3.down * upwardModifier;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime);

        rigidbody.MovePosition(rigidbody.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    protected override void Die()
    {
        rigidbody.useGravity = true;
        gameObject.TweenLocalScale(Vector3.zero, 2);
        DropCash();
        Destroy(gameObject, 2);
    }
}
