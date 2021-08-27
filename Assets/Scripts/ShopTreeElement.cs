using UnityEngine;
using UnityEngine.UI;

public class ShopTreeElement : MonoBehaviour {
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMPro.TextMeshProUGUI textMeshProUGUI;

    private Weapon my;
    private int price;

    public void Set(int price, Weapon w) {
        image.sprite = w.icon;
        my = w;
        if(GameManager.unlocked.Contains(my))
            textMeshProUGUI.text = "";
        else
            textMeshProUGUI.text = price + " PT";
        this.price = price;
        

        GetComponentInChildren<Button>().onClick.AddListener(Purchase);
    }

    public void Purchase() {
        if(!WeaponManager.Instance.weapons.Contains(my)) { 
            if(GameManager.unlocked.Contains(my)) {
                if(WeaponManager.Instance.weapons.Count < 5) {
                    WeaponManager.Instance.weapons.Add(my);
                    UIManager.Instance.loadoutDisplay.Refresh();
                }
                else {
                    UIManager.Instance.Announce("No space!");
                }
                
            }
            else if(PlayerManager.Instance.CanAfford(price)) {
                GameManager.unlocked.Add(my);
                PlayerManager.Instance.AddMoney(-price, Vector3.down * 1000);
                UIManager.Instance.shopDisplay.Refresh();
                UIManager.Instance.Announce("Purchased!");
                GameManager.money = PlayerManager.Instance.GetMoney();
            } else {
                UIManager.Instance.Announce("Cant Afford!");
            }
        }
    }

    public void SetIcon(Sprite icon) {
        Destroy(GetComponent<Image>());
        Destroy(GetComponent<Button>());
        image.sprite = icon;
        textMeshProUGUI.text = "";
    }
}