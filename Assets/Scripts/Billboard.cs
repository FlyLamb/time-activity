using TMPro;
using UnityEngine;
using System.Collections;
public class Billboard : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI text;
[Multiline]
    public string testString = @"<b>Najnowsze zmiany</b>";
    private int testLine = 0;

    private void Start() {
        StartCoroutine(Test());
    }

    public void SetText(string text) {
        this.text.text = text;
    }

    public IEnumerator Test() {
        while(true) {
            string td = testString.Split('\n')[testLine];
            SetText(td);
            yield return new WaitForSeconds(td.Length * 0.3f);
            testLine++;
            if(testLine >= testString.Split('\n').Length) testLine = 0;
        }
    }
}