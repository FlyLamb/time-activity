using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public float force;
    public float chargeTime = 2;
    [SerializeField][ColorUsage(false, true)] private Color m_color;
    [SerializeField] private ParticleSystem m_particles;
    private float m_chargeTime;
    private MeshRenderer m_renderer;

    private void Start() {
        m_renderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        if (m_chargeTime > 0)
            m_chargeTime -= Time.deltaTime;
        m_renderer.materials[1].SetColor("_EmissionColor", m_color * (chargeTime - m_chargeTime) / chargeTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (m_chargeTime > 0) return;

        if (other.GetComponent<Rigidbody>()) {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            m_chargeTime = chargeTime;
            m_particles.Play();
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}
