using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class UIManager : MonoBehaviour {
    public CanvasGroup death;

    public void ShowDeath(string message = "<size=162>b</size>URWA") {
        death.TweenCanvasGroupAlpha(1,1.5f).SetUseUnscaledTime(true);
    }
}