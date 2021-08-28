using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        instance = this;
    }

    public void Death() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

