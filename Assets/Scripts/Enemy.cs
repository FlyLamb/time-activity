using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float maxHealth;
    protected float health;

    private void Start() {
        Spawn();
    }

    private void Update() {
        Life();
    }

    protected void Critical() {
        IndicatorManager.Instance.Indicate(transform.position, "<b>Critical!</b>", Color.red, 2);
    }

    protected virtual void Spawn() {
        health = maxHealth;
    }

    protected virtual void Life() {

    }

    public virtual void Hit(float damage, DamageType damageType = DamageType.Normal) {
        health -= damage;
        if(health <= 0) Die();
        IndicatorManager.Instance.Indicate(transform.position, damage.ToString(), Color.white, 1);
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    protected void Unregister() {
        WaveManager.Instance.UnregisterEnemy(gameObject);
    }
}