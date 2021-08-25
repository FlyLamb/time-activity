using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class Indicator : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;
    private float lifetime;
    [SerializeField]
    private TMPro.TextMeshProUGUI textMeshProUGUI;

    public void Spawn(string text, Color color, float lifetime) {
        Vector3 force = new Vector3(Random.Range(-10,10),20,Random.Range(-10,10));
        GetComponent<Rigidbody>().AddForce(force);
        this.lifetime = lifetime;
        textMeshProUGUI.text = text;
        textMeshProUGUI.color = color;
        canvasGroup.TweenCanvasGroupAlpha(0, lifetime);
        Destroy(gameObject, lifetime);
    }

    private void LateUpdate() {
        transform.LookAt(Camera.main.transform.position);
    }


}