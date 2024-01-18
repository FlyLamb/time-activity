using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Weapon : MonoBehaviour {
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;
    public AudioClip fire1, fire2;

    protected WeaponManager Manager => WeaponManager.Instance;

    [SerializeField] protected Animator animator;

    public virtual void Fire1() {
        if (animator != null) animator.Play("Fire1", 0);
        Manager.PlayAudio(fire1);
    }

    public virtual void Fire2() {
        if (animator != null) animator.Play("Fire2", 0);
        Manager.PlayAudio(fire2);
    }

    public virtual void Show() {
        if (animator != null) animator.Play("Show", 0);
    }

    public virtual void Hide() {
        if (animator != null) {
            animator.Play("Hide", 0);
            float d;
            try {
                d = animator.runtimeAnimatorController.animationClips.First(c => c.name == "Hide").averageDuration;
            } catch {
                d = 0;
            }
            Destroy(gameObject, d);

        } else Destroy(gameObject);
    }

    public virtual void WeaponUpdate() {

    }
}

