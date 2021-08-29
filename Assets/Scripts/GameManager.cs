using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public static List<Weapon> unlocked = new List<Weapon>();
    public static int money = 0;

    public static List<Weapon> loadout = new List<Weapon>();

    public static int waveNum = 0;

    public Weapon chaosBlade;

    public void StartGame() {
        loadout = new List<Weapon>();
        money = 0;
        unlocked = new List<Weapon>();
        unlocked.Add(chaosBlade);
        loadout.Add(chaosBlade);
        SceneManager.LoadScene(1);
    }

    private void Awake() {
        instance = this;
    }

    public void Death() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        waveNum = 0;
    }

    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartWave() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        waveNum = WaveManager.Instance.waveNum;
    }

    public void NextArena() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

