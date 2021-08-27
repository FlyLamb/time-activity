using TMPro;
using UnityEngine;
using ElRaccoone.Tweens;
using System.Collections;

public class StatsDisplay : MonoBehaviour {
    public TextMeshProUGUI health, money;

    public void SetHealth(float health) {
        this.health.TweenLocalScaleY(0.4f,0.05f).SetOnComplete(() => this.health.TweenLocalScaleY(1,0.05f));
        this.health.text = health.ToString("0.0");
        this.health.color = Color.Lerp(Color.red, Color.white, health/100);
    }

    public void SetMoney(int money) {
        StopAllCoroutines();
        this.money.TweenLocalScaleY(1.5f,0.05f).SetOnComplete(() => this.money.TweenLocalScaleY(1,0.05f));
        StartCoroutine(MoneyGoal(money));
    }

    private IEnumerator MoneyGoal(int to) {
        int w = int.Parse(this.money.text);
        int i = 0;
        while(w != to) {
            w = int.Parse(this.money.text);
            if(w < to)
                this.money.text =  "" + (w+1);
            else if(w > to)
                this.money.text = "" + (w-1);
            i++;
            if(i%10 == 0) yield return null;
                
        }
    }
}

