using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public float sensitivity = 10f;

    [SerializeField]
    private bool invertY;

    [SerializeField]
    private BajtixPlayerController playerController;

    private Vector2 rotation;
    [SerializeField]
    private float minY = -90, maxY = 90, fovChangeSpeed = 5;

[SerializeField]
    private AnimationCurve fovKick;

    private new Camera camera;


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();
    }

    private void Update() {
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        rotation += input * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, minY, maxY);

        playerController.transform.rotation = Quaternion.Euler(0,rotation.x, 0);
        transform.localRotation = Quaternion.Euler(rotation.y, 0,0);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,fovKick.Evaluate(playerController.rb.velocity.magnitude) * 70,Time.deltaTime * fovChangeSpeed);
    }
}