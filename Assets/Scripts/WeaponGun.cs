using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon {
[SerializeField]
    private float shootDelay;
[SerializeField]
    private float bulletForce;
    private float _shootDelay;

    

[SerializeField]
    private Transform shootPoint;



[SerializeField]
    private Bullet bullet;

    public override void Fire1() {

        if(_shootDelay > 0) return;
        base.Fire1();
        var go = Instantiate(bullet.gameObject, shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
        go.Shoot(PlayerManager.Instance.camera.transform.forward, bulletForce);
        _shootDelay = shootDelay;
    }

    public override void WeaponUpdate() {
        base.WeaponUpdate();
        _shootDelay -= Time.deltaTime;
    }
}