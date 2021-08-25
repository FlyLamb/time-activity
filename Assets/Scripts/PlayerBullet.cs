using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet{
    [SerializeField]
    protected DamageType type;


    

    protected override void OnCollisionEnter(Collision other) {
        
        if(other.collider.GetComponent<Enemy>())
            other.collider.GetComponent<Enemy>().Hit(damage, type);

        base.OnCollisionEnter(other);
    }
}