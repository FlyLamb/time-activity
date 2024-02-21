using System;
using ElRaccoone.Tweens;
using UnityEngine;


// THIS IS TERRIBLE AND SHOULD NOT BE DONE LIKE THIS!!!
// DONT LET IT OUT OF INDEV

public class Fader : MonoBehaviour {
    [System.Serializable]
    public enum FadeAction {
        Nothing, FadeIn, FadeOut
    }

    public FadeAction startAction = FadeAction.Nothing;
    public float duration = 0.1f;

    public static Fader Instance => GameObject.FindObjectOfType<Fader>();

    private void Start() {
        switch (startAction) {
            case FadeAction.FadeIn:
                FadeIn();
                break;
            case FadeAction.FadeOut:
                FadeOut();
                break;
            default:
                break;
        }
    }


    public void FadeIn() {
        GetComponent<CanvasGroup>().TweenCanvasGroupAlpha(1, duration).SetFrom(0);
    }

    public void FadeIn(Action after) {
        GetComponent<CanvasGroup>().TweenCanvasGroupAlpha(1, duration).SetFrom(0).SetOnComplete(after);
    }

    public void FadeOut() {
        GetComponent<CanvasGroup>().TweenCanvasGroupAlpha(0, duration).SetFrom(1);
    }

    public void FadeOut(Action after) {
        GetComponent<CanvasGroup>().TweenCanvasGroupAlpha(0, duration).SetFrom(1).SetOnComplete(after);
    }
}
