using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour {
    public static WeaponManager Instance {
        get {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType<WeaponManager>();
            return m_instance;
        }
    }

    public List<Weapon> weapons;
    public int SelectedWeapon => m_selected;
    public Vector3 CameraAim => PlayerManager.Instance.camera.transform.forward;
    public Vector3 CameraPosition => PlayerManager.Instance.camera.transform.position;
    public BajtixPlayerController PlayerController => PlayerManager.Instance.controller;

    private static WeaponManager m_instance;
    [SerializeField][FormerlySerializedAs("source")] private AudioSource m_source;
    private int m_selected = 0;
    private Weapon m_selectedWeapon;
    private float switchWheelProgress = 0;
    private float delay = 0;


    private void Start() {
        if (GameManager.loadout != null) {
            weapons = GameManager.loadout;
        }
        if (weapons.Count > 0)
            Select(0);
    }

    public void Select(int index) {
        if (delay > 0) return;

        int previous_selected = m_selected;

        if (index < 0) {
            m_selected = weapons.Count - 1;
        } else if (index >= weapons.Count) {
            m_selected = 0;
        } else {
            m_selected = index;
        }

        if (m_selected == previous_selected && m_selectedWeapon != null && m_selectedWeapon.weaponName == weapons[m_selected].weaponName) return; // god is dead :)

        if (m_selectedWeapon != null) {
            m_selectedWeapon.Hide();
        }
        m_selectedWeapon = Instantiate(weapons[m_selected].gameObject, transform).GetComponent<Weapon>();
        m_selectedWeapon.Show();
        WeaponDisplay.Instance.ShowAnimation();
        delay = 0.2f;
        if (!GameManager.unlocked.Contains(weapons[m_selected])) {
            GameManager.unlocked.Add(weapons[m_selected]);
        }
    }

    public void PlayAudio(AudioClip clip) {
        if (clip == null) return;
        m_source.PlayOneShot(clip);
    }

    private void Update() {
        if (delay >= 0)
            delay -= Time.deltaTime;
        if (m_selectedWeapon != null) m_selectedWeapon.WeaponUpdate();

        if (Cursor.lockState == CursorLockMode.None) return;
        float sw = Input.GetAxis("Mouse ScrollWheel");


        switchWheelProgress -= sw;
        if (switchWheelProgress > 0.02f) {
            Select(m_selected + 1);
            switchWheelProgress = 0;
            if (weapons.Count != 1)
                WeaponDisplay.Instance.ShowAnimationTo(-150, 0.1f);

        } else if (switchWheelProgress < -0.02f) {
            Select(m_selected - 1);
            switchWheelProgress = 0;
            if (weapons.Count != 1)
                WeaponDisplay.Instance.ShowAnimationTo(150, 0.1f);
        }

        if (m_selectedWeapon != null) {
            if (Input.GetButton("Fire1")) m_selectedWeapon.Fire1();
            else
            if (Input.GetButton("Fire2")) m_selectedWeapon.Fire2();
        }
    }
}