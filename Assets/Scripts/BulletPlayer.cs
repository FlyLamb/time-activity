using UnityEngine;

public class BulletPlayer : Bullet {

    [SerializeField] protected DamageType type;

    protected override void OnCollisionEnter(Collision other) {

        if (other.collider.GetComponent<Enemy>())
            other.collider.GetComponent<Enemy>().Hit(m_damage, type);

        base.OnCollisionEnter(other);
    }
}