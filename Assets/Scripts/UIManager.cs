using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private CanvasGroup death;
    
    [SerializeField] private GameObject waveAnnouncer;
    [SerializeField] private TextMeshProUGUI waveText;

    private static UIManager instance;

    private Tween<float> tween;

    public static UIManager Instance {
        get {
            if(instance == null)
                instance = GameObject.FindObjectOfType<UIManager>();
            return instance;
        }
    }

    public void ShowDeath(string message = "<size=162>b</size>URWA") {
        death.TweenCanvasGroupAlpha(1,1.5f).SetUseUnscaledTime(true);
    }

    public void AnnounceNewWave(int waveNum, int enemyCount) {
        
        
        Announce($"<b>Wave {waveNum}</b>");
    }

    public void Announce(string text) {
        if(tween != null)
            tween.Cancel();
        waveText.text = text;
        tween = waveAnnouncer.TweenAnchoredPositionY(-40, 0.1f).SetOnComplete(()=>waveAnnouncer.TweenAnchoredPositionY(40, 0.1f).SetDelay(2));
    }
}