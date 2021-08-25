using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChaosBlade : Weapon {
    [SerializeField] private MeleeWeaponHitbox hitbox;
    [SerializeField] private float damage, attackInterval;
    private float _attackInterval; // TODO: make attack interval a player variable, so the interval cannot be avoided by switching weapons
    public override void Fire1() {
        if(_attackInterval > 0) return;
        base.Fire1();
        Attack();
    }

    public override void Fire2() {
        if(_attackInterval > 0) return;
        base.Fire2();
        Attack();
    }

    private void Attack() {
        
        hitbox.Hit(damage);
        _attackInterval = attackInterval;
    }

    public override void WeaponUpdate() {
        if(attackInterval >= 0)
        _attackInterval -= Time.deltaTime;
    }
}