using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{


    

    protected override void OnCollisionEnter(Collision other) {
        
        if(other.collider.GetComponent<PlayerManager>())
            PlayerManager.Instance.Hit(damage);

        base.OnCollisionEnter(other);
    }
}
