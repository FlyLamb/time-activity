using UnityEngine.UI;
using UnityEngine;
using ElRaccoone.Tweens;

public class WeaponDisplay : MonoBehaviour {
    [SerializeField] private Image weapon1, weapon2, weapon3;
    [SerializeField] private Sprite fallback;
    private WeaponManager m_wm;

    private void Start() {
        m_wm = GameObject.FindObjectOfType<WeaponManager>();
    }

    public void Update() {
        weapon1.sprite = GetImage(m_wm.SelectedWeapon - 1);
        weapon2.sprite = GetImage(m_wm.SelectedWeapon);
        weapon3.sprite = GetImage(m_wm.SelectedWeapon + 1);
    }

    public void ShowAnimation() {
        gameObject.TweenCancelAll();
        gameObject.TweenAnchoredPositionX(-75, 0.1f).SetOnComplete(() => gameObject.TweenAnchoredPositionX(75, 0.2f).SetDelay(2f));
    }

    private Sprite GetImage(int index) {
        index = index % m_wm.weapons.Count;
        if (index < 0) index = m_wm.weapons.Count + index;

        if (m_wm.weapons[index].icon != null)
            return m_wm.weapons[index].icon;
        else return fallback;
    }
}