using TMPro;
using UnityEngine;
using ElRaccoone.Tweens;

public class StatsDisplay : MonoBehaviour {
    public TextMeshProUGUI health;

    public void SetHealth(float health) {
        this.health.TweenLocalScaleY(0.4f,0.05f).SetOnComplete(() => this.health.TweenLocalScaleY(1,0.05f));
        this.health.text = health.ToString("0.0");
        this.health.color = Color.Lerp(Color.red, Color.white, health/100);
    }
}

