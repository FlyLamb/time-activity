using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class PlayerPopierdalacz : MonoBehaviour {
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private SpringJoint m_springJoint;
    [SerializeField] private Transform m_groundPoint;

    [Header("Ground")][SerializeField] private LayerMask m_mask = int.MaxValue;
    [SerializeField] private float m_playerHeight, m_margin, m_rayOffset, m_radius;

    [Header("Control")][SerializeField] private float m_acceleration;
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private float m_jumpForce;

    [SerializeField] private bool m_isGrounded;


    private void Start() {
        m_springJoint.autoConfigureConnectedAnchor = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private float GetDistance(Vector3 atOffset) {
        RaycastHit hit;
        float dst = m_playerHeight + m_margin;
        if (Physics.Raycast(transform.position + atOffset - transform.up * m_rayOffset, -transform.up, out hit, m_playerHeight + m_margin - m_rayOffset, m_mask)) {
            dst = hit.distance + m_rayOffset;
        }
        Debug.DrawRay(transform.position + atOffset - transform.up * m_rayOffset, -transform.up * (m_playerHeight - m_rayOffset), Color.red, Time.fixedDeltaTime);
        Debug.DrawRay(transform.position + atOffset - transform.up * m_playerHeight, -transform.up * m_margin, Color.blue, Time.fixedDeltaTime);
        return dst;
    }

    private void FixedUpdate() {
        // float dst = 0;
        // dst += GetDistance(Vector3.forward * m_radius);
        // dst += GetDistance(Vector3.back * m_radius);
        // dst += GetDistance(Vector3.right * m_radius);
        // dst += GetDistance(Vector3.left * m_radius);
        // dst += GetDistance(Vector3.zero);
        // dst /= 5;
        float[] distances = {
            GetDistance(Vector3.forward * m_radius),
            GetDistance(Vector3.back * m_radius),
            GetDistance(Vector3.right * m_radius),
            GetDistance(Vector3.left * m_radius),
            GetDistance(Vector3.zero)
        };
        float dst = distances.Min();
        m_springJoint.connectedAnchor = transform.up * m_playerHeight;
        m_groundPoint.position = transform.position - transform.up * dst;

        m_isGrounded = dst != m_playerHeight + m_margin;

        //transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        //var inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        var inputVector = new Vector3(
            (Input.GetKey(KeyCode.D) ? 1f : 0f)
            + (Input.GetKey(KeyCode.A) ? -1f : 0f),
            0,
            (Input.GetKey(KeyCode.W) ? 1f : 0f)
            + (Input.GetKey(KeyCode.S) ? -1f : 0f)
        ).normalized;
        var velocity = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
        var desiredVelocity = transform.TransformDirection(inputVector) * m_walkSpeed;

        m_rb.AddForce(desiredVelocity - velocity, ForceMode.VelocityChange);
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) Jump();
    }

    private void Jump() {
        if (!m_isGrounded) return;
        m_rb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
    }
}
