using TMPro;
using UnityEngine;
using System.Collections;
public class Billboard : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI text;

    private static string testString = @"<size=70>bywa; tak się czasem zdarza.</size>
Ludzie są, albo ludzi nie ma. 
Widzisz, to nie do końca jest tak, że jest dobrze, albo jest niedobrze. 
Cały nasz świat, nasza planeta, ona składa się z ludzi. 
Ludzi, którzy nie znają celu w życiu, swego  skopós.
Tego celu nie da się poznać za swojego życia, a czysto teoretycznie
nie da się go poznać nigdy, lecz w praktyce w pewnym momencie może stać się zbyt mały aby był zauważalny. 
Sens twojego życia, jego cel, jego skopós może być bardzo długotrwały, ale nie musi. 
Mimo tego, że będzie de facto wieczny, jego znaczenie może spaść bardzo szybko, lub powoli. 
Szybko to około 1 wiek, a długo to 500 lat albo całe millenia. Posłużę się przykładem Alleksandra Wielkiego, 
który poprzez podbicie terenów Tureckich należących wówczas do Persów w 331 roku p.n.e. naniósł tam kulturę grecką. 
Jej skutki obserwujemy nawet dziś w postaci kultury Hellenistycznej, dzięki której powstało wiele znanych dzieł sztuki,
 w tym charakterystyczna architektura Turecka.";
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
            yield return new WaitForSeconds(td.Length * 0.1f);
            testLine++;
            if(testLine >= testString.Split('\n').Length) testLine = 0;
        }
    }
}