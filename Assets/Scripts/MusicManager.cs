using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ElRaccoone.Tweens;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance {
        get {
            if(instance == null) instance = GameObject.FindObjectOfType<MusicManager>();
            return instance;
        }
    }

    private static MusicManager instance;
    

    public enum MusicState {
        Lobby,
        Riser,
        Wave
    }

    [SerializeField]
    private AudioClip lobby, riser, wave;
    private MusicState state;

[SerializeField]
    private AudioSource source;

    public void StartWave() {
        StartCoroutine(WaitAndSwitchMusic(riser, false, ()=>{
            StartCoroutine(WaitAndSwitchMusic(wave,true));
        }));

    }

    private IEnumerator WaitAndSwitchMusic(AudioClip ac, bool enableLoop, Action onFinish = null) {
        print("Request switch music to " + ac.name);
        yield return new WaitWhile(() => source.isPlaying && !source.loop);
        if(source.loop) {
            source.TweenAudioSourceVolume(0,1).SetOnComplete(()=>enO(ac,enableLoop,onFinish));
        } else {
            enO(ac,enableLoop, onFinish);
        }
        
    }

    private void enO(AudioClip ac, bool enableLoop, Action onFinish) {
        source.clip = ac;
        source.loop = enableLoop;
        source.Play();
        source.volume = 0.35f;
        print("Set music to " + ac.name);
        if(onFinish != null) onFinish.Invoke();
    }

    public void StopWave() {
        StartCoroutine(WaitAndSwitchMusic(lobby,true));
    }
}
