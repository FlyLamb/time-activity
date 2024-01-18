using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRocketLauncher : Weapon {


    [SerializeField] private float shootDelay, reloadDelay;
    [SerializeField] private float bulletForce;
    [SerializeField] private float damage;
    [SerializeField] private float spread = 1;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask aimMask = int.MaxValue;
    [SerializeField] private Bullet bullet;
    private float m_shootDelay;
    private bool m_reloadedRecently = false;

    public override void Fire1() {

        if (m_shootDelay > 0 || !m_reloadedRecently) return;
        animator.StopPlayback();
        base.Fire1();

        if (m_reloadedRecently) {
            Shoot();
            m_reloadedRecently = false;
        }

        m_shootDelay = shootDelay;
    }

    protected void Shoot() {
        var bullet = Instantiate(this.bullet.gameObject, shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
        Vector3 direction = PlayerManager.Instance.camera.transform.forward;
        RaycastHit aimTarget;
        if (Physics.Raycast(PlayerManager.Instance.camera.transform.position, PlayerManager.Instance.camera.transform.forward, out aimTarget, 1000, aimMask, QueryTriggerInteraction.Ignore)) {
            direction = aimTarget.point - shootPoint.position;
            direction.Normalize();
            direction += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized * spread * 0.05f;
            Debug.DrawLine(transform.position, aimTarget.point, Color.red, 1);
        }
        bullet.Shoot(direction, bulletForce, damage);
    }



    public override void Fire2() {
        if (m_shootDelay > 0) return;
        base.Fire2();
        m_shootDelay = reloadDelay;
        m_reloadedRecently = true;
    }

    public override void WeaponUpdate() {
        base.WeaponUpdate();
        m_shootDelay -= Time.deltaTime;
    }
}

