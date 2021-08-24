using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public List<Weapon> weapons;

    private Weapon selectedWeapon;

[SerializeField]
    private int selected = 0;

    private float switchWheelProgress = 0;


    private void Start() {
        Select(0);
    }

    public void Select(int index) {
        if(index < 0 || index >= weapons.Count) return;
        if(selectedWeapon != null) {
            selectedWeapon.Hide();
        }
        selected = index;
        selectedWeapon = Instantiate(weapons[selected].gameObject, transform).GetComponent<Weapon>();
        selectedWeapon.Show();
    }

    private void Update() {
        if(selectedWeapon != null) selectedWeapon.WeaponUpdate();


        float sw = Input.GetAxis("Mouse ScrollWheel");
        switchWheelProgress += sw;
        if(switchWheelProgress > 0.1f) {
            Select(selected+1);
            switchWheelProgress = 0;
        } else if(switchWheelProgress < -0.1f) {
            Select(selected-1);
            switchWheelProgress = 0;
        }

        if(Input.GetButton("Fire1")) selectedWeapon.Fire1();
        if(Input.GetButton("Fire2")) selectedWeapon.Fire2();
    } 
}