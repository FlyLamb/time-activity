using System.Collections;
using UnityEngine;
using System;
using ElRaccoone.Tweens;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance;

    public AudioSource[] sources;

    public AudioMixer mixer;

    public float idleTime = 60;

    private float time;


    private void Awake() {
        if(Instance != this && Instance != null) Destroy(Instance);
            Instance = this;
    }

    private void Start() {
        foreach(var s in sources) s.Play();

        StopWave();
    }

    public void StartWave() {
        gameObject.TweenValueFloat(-80, 1, (w)=>mixer.SetFloat("Chill",w)).SetUseUnscaledTime(true);
        gameObject.TweenValueFloat(0, 0.5f, (w)=>mixer.SetFloat("Battle",w)).SetFrom(-80).SetUseUnscaledTime(true);
        gameObject.TweenValueFloat(-80, 1, (w)=>mixer.SetFloat("Idle",w)).SetUseUnscaledTime(true);
        time = float.PositiveInfinity;
    }

    public void StopWave() {
        gameObject.TweenValueFloat(-80, 1, (w)=>mixer.SetFloat("Battle",w)).SetUseUnscaledTime(true);
        gameObject.TweenValueFloat(0, 1, (w)=>mixer.SetFloat("Chill",w)).SetFrom(-80).SetUseUnscaledTime(true);
        time = Time.time + idleTime;
    }

    
    private void Update() {
        if(Time.time > time) {
            gameObject.TweenValueFloat(0, 1, (w)=>mixer.SetFloat("Idle",w)).SetFrom(-80).SetUseUnscaledTime(true);
            time = float.PositiveInfinity;
        }
    }
}
