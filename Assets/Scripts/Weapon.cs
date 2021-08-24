using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;
    

[SerializeField]
    protected Animator animator;

    public virtual void Fire1() {
        if(animator != null)
        animator.Play("Fire1");
    }

    public virtual void Fire2() {
        if(animator != null) animator.Play("Fire2");
    }

    public virtual void Show() {
        if(animator != null) animator.Play("Show");
    }

    public virtual void Hide() {
        if(animator != null) {
            animator.Play("Hide");
            Destroy(gameObject,animator.GetCurrentAnimatorClipInfo(0).Length);
        } else Destroy(gameObject);
    }

    public virtual void WeaponUpdate() {

    }
}

