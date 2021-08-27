using TMPro;
using UnityEngine;
using ElRaccoone.Tweens;
using System.Collections;
public class Billboard : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI text;
[Multiline]
    public string fullText = @"";
    private int textLine = 0;

    private Coroutine scrollCoroutine;

    public void SetText(string text, bool scroll = false) {
        fullText = text;
        if(scrollCoroutine != null)
            StopCoroutine(scrollCoroutine);
        if(scroll) {
            scrollCoroutine = StartCoroutine(Scroll());
        }
        else
            STxt(text);
    }

    private void STxt(string w) {
        if(this.text.text == w) return;
        this.text.TweenLocalScaleY(0.2f, 0.1f).SetOnComplete( ()=> text.TweenLocalScaleY(1f, 0.1f));
        this.text.text = w;
    }

    public IEnumerator Scroll() {
        textLine = 0;
        while(true) {
            string td = fullText.Split('\n')[textLine];
            STxt(td);
            print(td);
            yield return new WaitForSeconds(td.Length * 0.3f);
            textLine++;
            if(textLine >= fullText.Split('\n').Length) textLine = 0;
        }
    }
}