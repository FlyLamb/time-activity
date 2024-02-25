using UnityEngine.UI;
using UnityEngine;
using ElRaccoone.Tweens;
using UnityEngine.Serialization;

public class WeaponDisplay : MonoBehaviour {

    public static WeaponDisplay Instance => GameObject.FindObjectOfType<WeaponDisplay>();

    [SerializeField][FormerlySerializedAs("weapon1")] private Image m_weapon1;
    [SerializeField][FormerlySerializedAs("weapon2")] private Image m_weapon2;
    [SerializeField][FormerlySerializedAs("weapon3")] private Image m_weapon3;

    [SerializeField][FormerlySerializedAs("fallback")] private Sprite m_fallback;

    private float m_defaultOffset = 0;
    private WeaponManager Manager => WeaponManager.Instance;


    private void Start() {
        m_defaultOffset = m_weapon1.rectTransform.anchoredPosition.y - m_weapon2.rectTransform.anchoredPosition.y;
    }

    public void UpdateIcons() {
        m_weapon1.sprite = GetImage(Manager.SelectedWeapon - 1);
        m_weapon2.sprite = GetImage(Manager.SelectedWeapon);
        m_weapon3.sprite = GetImage(Manager.SelectedWeapon + 1);
    }

    public void ShowAnimation() {
        gameObject.TweenCancelAll();
        gameObject.TweenAnchoredPositionX(-75, 0.1f).SetOnComplete(() => gameObject.TweenAnchoredPositionX(75, 0.2f).SetDelay(2f));
    }

    public void ShowAnimationTo(float yOffset, float dur) {
        AnimateElement(m_weapon1, yOffset, m_defaultOffset, dur, GetImage(Manager.SelectedWeapon - 1));
        AnimateElement(m_weapon2, yOffset, 0, dur, GetImage(Manager.SelectedWeapon));
        AnimateElement(m_weapon3, yOffset, -m_defaultOffset, dur, GetImage(Manager.SelectedWeapon + 1));
    }

    private void AnimateElement(Image element, float yOffset, float defaultPosition, float duration, Sprite change) {
        element.TweenCancelAll();
        element.TweenAnchoredPositionY(defaultPosition + yOffset, duration).SetOnComplete(() => {
            element.rectTransform.anchoredPosition = new Vector2(
                element.rectTransform.anchoredPosition.x,
                defaultPosition
            );
            element.sprite = change;
        });
    }

    private Sprite GetImage(int index) {
        index = index % Manager.weapons.Count;
        if (index < 0) index = Manager.weapons.Count + index;

        if (Manager.weapons[index].icon != null)
            return Manager.weapons[index].icon;
        else return m_fallback;
    }
}