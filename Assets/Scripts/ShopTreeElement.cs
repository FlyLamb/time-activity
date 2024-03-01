using UnityEngine;
using UnityEngine.UI;

public class ShopTreeElement : MonoBehaviour {
    [SerializeField] private Image m_image;
    [SerializeField] private TMPro.TextMeshProUGUI m_text;
    private Weapon m_weapon;
    private int m_price;

    public void SetWeapon(int price, Weapon w) {
        m_image.sprite = w.icon;
        m_weapon = w;
        if (GameManager.unlocked.Contains(m_weapon)) {
            m_text.text = "";
            GetComponent<Image>().color = Color.white;
        } else {
            m_text.text = price + " PT";
            GetComponent<Image>().color = Color.white * 0.5f;
        }
        this.m_price = price;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(Purchase);
    }

    public void SetCategory(Sprite icon) {
        Destroy(GetComponent<Image>());
        Destroy(GetComponent<Button>());
        m_image.sprite = icon;
        m_text.text = "";
    }

    public void Purchase() {
        if (!WeaponManager.Instance.weapons.Contains(m_weapon)) {
            if (GameManager.unlocked.Contains(m_weapon)) {
                if (WeaponManager.Instance.weapons.Count < 5) {
                    WeaponManager.Instance.weapons.Add(m_weapon);
                    UIManager.Instance.loadoutDisplay.Refresh();
                } else {
                    UIManager.Instance.Announce("No space!");
                }

            } else if (PlayerManager.Instance.CanAfford(m_price)) {
                GameManager.unlocked.Add(m_weapon);
                PlayerManager.Instance.AddMoney(-m_price, Vector3.down * 1000);
                UIManager.Instance.shopDisplay.Refresh();
                UIManager.Instance.Announce("Purchased!");
                GameManager.money = PlayerManager.Instance.GetMoney();
            } else {
                UIManager.Instance.Announce("Cant Afford!");
            }
        }
    }

}