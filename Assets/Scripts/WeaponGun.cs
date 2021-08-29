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
    private float damage;

    [SerializeField]
    private float spread = 1;

[SerializeField]
    private Transform shootPoint;

[SerializeField]
    private LayerMask aimMask = int.MaxValue;

[SerializeField]
    private Bullet bullet;

    private bool reloadedRecently = false;

    public override void Fire1() {

        if(_shootDelay > 0) return;
        animator.StopPlayback();
        base.Fire1();
        
        Shoot();
        if(reloadedRecently) {
            Shoot();
            Shoot();
            Shoot();
            reloadedRecently = false;
        }
        
        _shootDelay = shootDelay;
    }

    protected void Shoot() {
        var go = Instantiate(bullet.gameObject, shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
        Vector3 dir = PlayerManager.Instance.camera.transform.forward;
        RaycastHit aimTarget;
        if(Physics.Raycast(PlayerManager.Instance.camera.transform.position, PlayerManager.Instance.camera.transform.forward, out aimTarget, 1000,aimMask, QueryTriggerInteraction.Ignore)) {
            dir = aimTarget.point - shootPoint.position ;
            dir.Normalize();
            dir += new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1)).normalized * spread * 0.05f;
            Debug.DrawLine(transform.position, aimTarget.point, Color.red, 1);
        }
        go.Shoot(dir, bulletForce, damage);
    }

    public override void Fire2() {
        if(_shootDelay > 0) return;
        base.Fire2();
        _shootDelay = shootDelay* 3;
        reloadedRecently = true;
    }

    public override void WeaponUpdate() {
        base.WeaponUpdate();
        _shootDelay -= Time.deltaTime;
    }
}