using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutDisplayElement : MonoBehaviour {
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;


    private Weapon my;
    public void Load(Weapon w) {
        icon.sprite = w.icon;
        textMeshProUGUI.text = $"<size=36>{w.weaponName}</size><br>{w.weaponDescription}";
        if(WeaponManager.Instance.weapons.Count > 1)
            transform.GetComponentInChildren<Button>().onClick.AddListener(BtnRemove);
        my = w;
    }

    public void BtnRemove() {
        WeaponManager.Instance.weapons.Remove(my);
        UIManager.Instance.loadoutDisplay.Refresh();
    } 
}
