using TMPro;
using UnityEngine;
using ElRaccoone.Tweens;
using System.Collections;
using UnityEngine.Serialization;

public class Billboard : MonoBehaviour {

    [FormerlySerializedAs("text")][SerializeField] private TextMeshProUGUI m_textMeshPro;
    [Multiline] public string fullText = @"";
    public float scrollSpeed = 0.3f;
    private int m_currentLine = 0;

    private Coroutine m_scrollCoroutine;

    public void SetText(string text, bool scroll = false) {
        fullText = text;
        if (m_scrollCoroutine != null)
            StopCoroutine(m_scrollCoroutine);
        if (scroll) {
            m_scrollCoroutine = StartCoroutine(Scroll());
        } else {
            SetText(text);
        }
    }

    private void SetText(string w) {
        if (m_textMeshPro.text == w) return;
        m_textMeshPro.TweenLocalScaleY(0.2f, 0.1f).SetOnComplete(() => m_textMeshPro.TweenLocalScaleY(1f, 0.1f));
        m_textMeshPro.text = w;
    }

    public IEnumerator Scroll() {
        m_currentLine = 0;
        var lines = fullText.Split('\n');
        while (true) {
            var current = lines[m_currentLine];
            SetText(current);
            yield return new WaitForSeconds(current.Length * scrollSpeed);
            m_currentLine++;
            if (m_currentLine >= fullText.Split('\n').Length) m_currentLine = 0;
        }
    }
}