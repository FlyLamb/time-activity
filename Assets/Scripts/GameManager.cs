using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public static List<Weapon> unlocked = new List<Weapon>();
    public static int money = 0;

    public static List<Weapon> loadout = new List<Weapon>();

    public static int waveNum = 0;
    [SerializeField] private Weapon m_chaosBlade;


    [Header("Debug")][SerializeField] private bool m_debugEnabled = false;
    [SerializeField] private List<Weapon> m_debugLoadout;
    [SerializeField] private int m_dbgMoney = Int16.MaxValue;



    [ContextMenu("Reset to default")]

    public void StartGame() {
        loadout = new List<Weapon>();
        money = 0;
        unlocked = new List<Weapon>();

        if (m_chaosBlade == null) throw new Exception("YOU FORGOT TO ADD THE DEFAULT SWORD MORON");
        unlocked.Add(m_chaosBlade);
        loadout.Add(m_chaosBlade);

        SceneManager.LoadScene(1);
    }

    private void Awake() {
        instance = this;

        if (!Application.isEditor) {
            m_debugEnabled = false;
        }
    }

    private void Start() {
        if (!m_debugEnabled) return;
        money = m_dbgMoney;
        loadout.Clear();
        loadout.AddRange(m_debugLoadout);
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

