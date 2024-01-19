using UnityEngine;

public class WeaponRocketLauncher : WeaponGun {
    [SerializeField] protected float reloadDelay;
    [SerializeField] protected int m_magSize;
    private int m_inMag;

    public override void Fire1() {
        if (m_cooldown > 0 || m_inMag <= 0) return;
        m_animator.StopPlayback();
        base.Fire1Fx();
        base.Shoot();
        m_inMag--;
        m_cooldown = m_shotCooldown;
    }

    public override void Fire2() {
        if (m_cooldown > 0) return;
        base.Fire2Fx();
        m_cooldown = reloadDelay;
        m_inMag = m_magSize;
    }
}

