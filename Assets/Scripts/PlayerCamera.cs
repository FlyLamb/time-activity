using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour {
    public float sensitivity = 10f;
    public bool invertY;

    [FormerlySerializedAs("playerController")][SerializeField] private Rigidbody m_playerController;
    [SerializeField] private Vector2 m_yRange = new Vector2(-90, 90);
    [FormerlySerializedAs("fovChangeSpeed")][SerializeField] private float m_fovChangeSpeed = 5;
    [FormerlySerializedAs("fovKick")][SerializeField] private AnimationCurve m_fovKick;


    private Camera m_camera;
    private Vector2 m_rotation;
    private float m_shakeTime, m_shakeStr;
    private Vector3 m_referencePosition;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        m_camera = GetComponent<Camera>();
        m_referencePosition = transform.localPosition;

        m_rotation.x = m_playerController.transform.rotation.eulerAngles.y;
    }



    private void Update() {
        if (Cursor.lockState == CursorLockMode.None) return;
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        m_rotation += input * sensitivity;
        m_rotation.y = Mathf.Clamp(m_rotation.y, m_yRange.x, m_yRange.y);

        m_playerController.transform.rotation = Quaternion.Euler(0, m_rotation.x, 0);
        transform.localRotation = Quaternion.Euler(m_rotation.y, 0, 0);
        m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_fovKick.Evaluate(m_playerController.velocity.magnitude) * 70, Time.deltaTime * m_fovChangeSpeed);

        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore)) {
                if (hit.collider.GetComponent<Interactable>())
                    hit.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    private void FixedUpdate() {
        if (m_shakeTime > 0) {
            transform.localPosition = m_referencePosition + new Vector3(Random.Range(-m_shakeStr, m_shakeStr), Random.Range(-m_shakeStr, m_shakeStr), 0);
            m_shakeTime -= Time.fixedDeltaTime;
        } else transform.localPosition = m_referencePosition;
    }

    public void Shake(float strength, float duration) {
        m_shakeTime = duration;
        m_shakeStr = strength;
    }
}