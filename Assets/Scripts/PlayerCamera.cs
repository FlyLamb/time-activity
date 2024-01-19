using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public float sensitivity = 10f;

    [SerializeField]
    private bool invertY;

    [SerializeField]
    private Rigidbody playerController;

    private Vector2 rotation;
    [SerializeField]
    private float minY = -90, maxY = 90, fovChangeSpeed = 5;

    [SerializeField]
    private AnimationCurve fovKick;

    private new Camera camera;

    private float shakeTime, shakeStr;
    private Vector3 originalPoint;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();
        originalPoint = transform.localPosition;
    }



    private void Update() {
        if (Cursor.lockState == CursorLockMode.None) return;


        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        rotation += input * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, minY, maxY);

        playerController.transform.rotation = Quaternion.Euler(0, rotation.x, 0);
        transform.localRotation = Quaternion.Euler(rotation.y, 0, 0);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, fovKick.Evaluate(playerController.velocity.magnitude) * 70, Time.deltaTime * fovChangeSpeed);

        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore)) {
                if (hit.collider.GetComponent<Interactable>())
                    hit.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    private void FixedUpdate() {
        if (shakeTime > 0) {
            transform.localPosition = originalPoint + new Vector3(Random.Range(-shakeStr, shakeStr), Random.Range(-shakeStr, shakeStr), 0);
            shakeTime -= Time.fixedDeltaTime;
        } else transform.localPosition = originalPoint;
    }

    public void Shake(float strength, float duration) {
        shakeTime = duration;
        shakeStr = strength;
    }
}