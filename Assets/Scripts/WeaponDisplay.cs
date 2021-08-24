using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class WeaponDisplay : MonoBehaviour {
    [SerializeField]
    private Image weapon1, weapon2, weapon3;

    [SerializeField] private Sprite fallback;

    private WeaponManager wm;
    private void Start() {
        wm = GameObject.FindObjectOfType<WeaponManager>();
    }

    public void Update() {
        weapon1.sprite = GetImage(wm.selected - 1);
        weapon2.sprite = GetImage(wm.selected);
        weapon3.sprite = GetImage(wm.selected + 1);
    }

    private Sprite GetImage(int index) {
        if(index >= wm.weapons.Count || index < 0) return fallback;
        return wm.weapons[index].icon;
    }
}