using UnityEngine;
using UnityEngine.UI;

public class ShopTreeElement : MonoBehaviour {
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMPro.TextMeshProUGUI textMeshProUGUI;
    public void Set(int price, Sprite icon) {
        image.sprite = icon;
        textMeshProUGUI.text = price.ToString();
    }
}