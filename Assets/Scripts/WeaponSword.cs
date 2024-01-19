using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSword : Weapon {
    [Header("Sword Params")][SerializeField][FormerlySerializedAs("hitbox")] protected MeleeWeaponHitbox m_hitbox;
    [SerializeField][FormerlySerializedAs("damage")] private float m_damage;
    [SerializeField][FormerlySerializedAs("attackInterval")] private float m_attackCooldown;
    [SerializeField][FormerlySerializedAs("pushForce")] private float m_slashDashStrength = 3;

    public override void Fire1() {
        if (!ReadyToUse) return;
        base.Fire1();
        Attack();
    }

    public override void Fire2() {
        if (!ReadyToUse) return;
        base.Fire2();
        Attack();
        Manager.PlayerController.rb
            .AddForce(Manager.CameraAim * m_slashDashStrength, ForceMode.Impulse);
    }

    protected void Attack() {
        m_hitbox.Hit(m_damage, DamageType.Melee);
        m_cooldown = m_attackCooldown;
    }

}