using UnityEngine;
using UnityEngine.Serialization;

public class WeaponGun : Weapon {

    [Header("Gun Params")][SerializeField][FormerlySerializedAs("shootPoint")] protected Transform m_shootPoint;
    [SerializeField][FormerlySerializedAs("bullet")] protected Bullet m_bullet;
    [SerializeField][FormerlySerializedAs("aimMask")] protected LayerMask m_aimMask = int.MaxValue;
    [SerializeField][FormerlySerializedAs("shootDelay")] protected float m_shotCooldown;
    [SerializeField][FormerlySerializedAs("spread")] protected float m_shotSpread = 1;
    [SerializeField][FormerlySerializedAs("damage")] protected float m_bulletDamage;
    [SerializeField][FormerlySerializedAs("bulletForce")] protected float m_bulletForce;
    [SerializeField][FormerlySerializedAs("tripleUltra")] protected bool m_tripleUltra = true;

    protected bool m_reloadedRecently = false;

    public override void Fire1() {
        if (m_cooldown > 0) return;
        m_animator.StopPlayback();
        base.Fire1Fx();

        Shoot();
        if (m_reloadedRecently && m_tripleUltra) {
            Shoot();
            Shoot();
            Shoot();
            m_reloadedRecently = false;
        }

        m_cooldown = m_shotCooldown;
    }

    protected void Shoot() {
        var bullet = Instantiate(this.m_bullet.gameObject, m_shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
        Vector3 dir = Manager.CameraAim;
        RaycastHit aimTarget;
        if (Physics.Raycast(Manager.CameraPosition, Manager.CameraAim, out aimTarget, 1000, m_aimMask, QueryTriggerInteraction.Ignore)) {
            dir = aimTarget.point - m_shootPoint.position;
            dir.Normalize();
            dir += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * m_shotSpread * 0.05f;
            Debug.DrawLine(transform.position, aimTarget.point, Color.red, 1);
        }
        bullet.Shoot(dir, m_bulletForce, m_bulletDamage);
    }

    public override void Fire2() {
        if (m_cooldown > 0) return;
        base.Fire2();
        m_cooldown = m_shotCooldown * 3;
        m_reloadedRecently = true;
    }

    public override void WeaponUpdate() {
        base.WeaponUpdate();
        m_cooldown -= Time.deltaTime;
    }
}