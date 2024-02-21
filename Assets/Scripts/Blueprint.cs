using UnityEngine;

public class Blueprint : MonoBehaviour {

    private void Start() {
        if (!GameManager.IsDebugModeOn) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6) {
            Fader.Instance.FadeIn(() => {
                GameManager.instance.NextArena();
            });
        }
    }
}
