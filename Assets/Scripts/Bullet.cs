using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour {
    [SerializeField][FormerlySerializedAs("rb")] protected Rigidbody m_rb;
    [SerializeField][FormerlySerializedAs("particles")] protected GameObject m_particles;
    [SerializeField][FormerlySerializedAs("damage")] protected float m_damage;

    private void Start() {
        if (m_rb == null) m_rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot(Vector3 direction, float force, float damage = -1) {
        if (m_rb != null) m_rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        if (damage != -1) this.m_damage = damage;
    }

    protected virtual void OnCollisionEnter(Collision other) {
        if (m_particles != null) Instantiate(m_particles, transform.position, Quaternion.LookRotation(other.contacts[0].normal));
        Destroy(gameObject);
    }
}