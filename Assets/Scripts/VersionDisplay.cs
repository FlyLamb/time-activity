using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionDisplay : MonoBehaviour {
    private void Start() {
        GetComponent<TextMeshProUGUI>().text = "TAPS/" + Application.version;
    }
}
