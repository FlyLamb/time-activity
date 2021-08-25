using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour {
    private static IndicatorManager instance;
    public static IndicatorManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<IndicatorManager>();
            }
            return instance;
        }
    }

[SerializeField]
    private Indicator indicator;

    public void Indicate(Vector3 position, string text, Color color, float duration = 1) {
        var idi = Instantiate(indicator.gameObject, position, Quaternion.identity).GetComponent<Indicator>();
        idi.Spawn(text, color, duration);
    }
}