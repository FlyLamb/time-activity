using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {


    public static WeaponManager Instance {
        get {
            if(instance == null)
                instance = GameObject.FindObjectOfType<WeaponManager>();
            return instance;
        }
    }

    private static WeaponManager instance;

    public List<Weapon> weapons;

    private Weapon selectedWeapon;
    public int selected = 0;

    private float switchWheelProgress = 0;

    private float delay = 0;


    private void Start() {
        Select(0);
    }

    public void Select(int index) {
        if(index < 0 || index >= weapons.Count) return;
        if(delay > 0) return;

        if(selectedWeapon != null) {
            selectedWeapon.Hide();
        }
        selected = index;
        selectedWeapon = Instantiate(weapons[selected].gameObject, transform).GetComponent<Weapon>();
        selectedWeapon.Show();
        GameObject.FindObjectOfType<WeaponDisplay>().ShowAnimation();
        delay = 0.2f;
        if(!GameManager.unlocked.Contains(weapons[selected])) {
            GameManager.unlocked.Add(weapons[selected]);
        }
    }

    private void Update() {
        if(delay >= 0)
            delay-=Time.deltaTime;
        if(selectedWeapon != null) selectedWeapon.WeaponUpdate();

        if(Cursor.lockState == CursorLockMode.None) return;
        float sw = Input.GetAxis("Mouse ScrollWheel");
        switchWheelProgress -= sw;
        if(switchWheelProgress > 0.02f) {
            Select(selected+1);
            switchWheelProgress = 0;
        } else if(switchWheelProgress < -0.02f) {
            Select(selected-1);
            switchWheelProgress = 0;
        }
        if(selectedWeapon != null)  {
            if(Input.GetButton("Fire1")) selectedWeapon.Fire1();
            if(Input.GetButton("Fire2")) selectedWeapon.Fire2();
        }
    } 
}