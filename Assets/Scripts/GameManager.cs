using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public static List<Weapon> unlocked = new List<Weapon>();
    public static int money = 0;

    public static List<Weapon> loadout = new List<Weapon>();

    public void StartGame() {
        loadout = new List<Weapon>();
        money = 0;
        unlocked = new List<Weapon>();
    }

    private void Awake() {
        if(instance != null) Destroy(gameObject);
        else instance = this;
    }
}

