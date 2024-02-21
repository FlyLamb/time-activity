using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSword : Weapon {
    [Header("Sword Params")][SerializeField][FormerlySerializedAs("hitbox")] protected MeleeWeaponHitbox m_hitbox;
    [SerializeField][FormerlySerializedAs("damage")] private float m_damage;
    [SerializeField][FormerlySerializedAs("attackInterval")] private float m_attackCooldown;
    [SerializeField] private float m_knockback = 1;
    [SerializeField][FormerlySerializedAs("pushForce")] private float m_slashDashStrength = 3;

    public override void Fire1() {
        if (!ReadyToUse) return;
        if (m_animator != null)
            m_animator.Play("Fire1");
        Manager.PlayAudio(m_fire1);
        Attack();
    }

    public override void Fire2() {
        if (!ReadyToUse) return;
        if (m_animator != null)
            m_animator.Play("FireAlt");
        Manager.PlayAudio(m_fire2);
        Attack();
        Manager.PlayerController.rb
            .AddForce(Manager.CameraAim * m_slashDashStrength, ForceMode.Impulse);
    }

    protected void Attack() {
        m_hitbox.Hit(m_damage, Manager.PlayerController.transform.forward * m_knockback, DamageType.Melee);
        m_cooldown = m_attackCooldown;
    }

}