using UnityEngine;
using ElRaccoone.Tweens;

public class BulletGrenade : Bullet {
    [SerializeField] private float m_timeToExplode = 2f;
    [SerializeField] private GameObject m_explosion;

    protected override void OnCollisionEnter(Collision other) {
        gameObject.TweenDelayedInvoke(m_timeToExplode, () => Explode());
    }

    private void Explode() {
        Instantiate(m_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
