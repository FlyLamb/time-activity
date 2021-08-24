using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour {
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;
    

[SerializeField]
    protected Animator animator;

    public virtual void Fire1() {
        if(animator != null) animator.Play("Fire1", 0);
    }

    public virtual void Fire2() {
        if(animator != null) animator.Play("Fire2", 0);
    }

    public virtual void Show() {
        if(animator != null) animator.Play("Show", 0);
    }

    public virtual void Hide() {
        if(animator != null) {
            animator.Play("Hide", 0);
            Destroy(gameObject,animator.GetCurrentAnimatorClipInfo(0).First((w) => w.clip.name == "Hide").clip.length);
        } else Destroy(gameObject);
    }

    public virtual void WeaponUpdate() {

    }
}

