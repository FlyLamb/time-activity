using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    public static List<Weapon> unlocked = new List<Weapon>();
    public static int money = 0;

    public static List<Weapon> loadout;
}

