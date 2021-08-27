using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public float force;
    public float chargeTime = 2;
    private float _chargeTime;

[SerializeField]
    private Color color;

    private new MeshRenderer renderer;

[SerializeField]
    private ParticleSystem particles;

    private void Start() {
        renderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        if(chargeTime > 0)
            _chargeTime -= Time.deltaTime;
        renderer.materials[1].SetColor("_EmissionColor", color * (chargeTime - _chargeTime) / chargeTime * 4);
    }

    private void OnTriggerEnter(Collider other) {
        if(_chargeTime > 0) return;
        
        if(other.GetComponent<Rigidbody>()) {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            _chargeTime = chargeTime;
            particles.Play();
        }
    }
}
