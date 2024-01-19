using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour {
    [Header("Visuals")]
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;
    [SerializeField][FormerlySerializedAs("fire1")] protected AudioClip m_fire1;
    [SerializeField][FormerlySerializedAs("fire2")] protected AudioClip m_fire2;
    [SerializeField][FormerlySerializedAs("animator")] protected Animator m_animator;
    protected WeaponManager Manager => WeaponManager.Instance;
    protected bool ReadyToUse => m_cooldown <= 0;
    protected float m_cooldown;

    /// Called when Fire1 is pressed
    public virtual void Fire1() {
        Fire1Fx();
    }

    /// Basic effects for Fire2
    protected virtual void Fire1Fx() {
        if (m_animator != null) m_animator.Play("Fire1", 0);
        Manager.PlayAudio(m_fire1);
    }

    /// Called when Fire2 is pressed
    public virtual void Fire2() {
        Fire2Fx();
    }

    /// Basic effects for Fire2
    protected virtual void Fire2Fx() {
        if (m_animator != null) m_animator.Play("Fire2", 0);
        Manager.PlayAudio(m_fire2);
    }

    /// Called when the player switches to the weapon
    public virtual void Show() {
        if (m_animator != null) m_animator.Play("Show", 0);
    }

    /// Called when the player switches to another weapon
    public virtual void Hide() {
        if (m_animator != null) {
            m_animator.Play("Hide", 0);
            float d;
            try {
                d = m_animator.runtimeAnimatorController.animationClips.First(c => c.name == "Hide").averageDuration;
            } catch {
                d = 0;
            }
            Destroy(gameObject, d);

        } else Destroy(gameObject);
    }

    /// Gets called on Update when the weapon is equipped
    public virtual void WeaponUpdate() {
        if (m_cooldown > 0) m_cooldown -= Time.deltaTime;
    }
}

