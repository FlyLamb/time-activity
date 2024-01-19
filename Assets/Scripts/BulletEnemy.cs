using UnityEngine;

public class BulletEnemy : Bullet {
    protected override void OnCollisionEnter(Collision other) {

        if (other.collider.GetComponent<PlayerManager>())
            PlayerManager.Instance.Hit(m_damage);

        base.OnCollisionEnter(other);
    }
}
