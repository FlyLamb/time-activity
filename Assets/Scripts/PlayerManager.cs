using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager instance;
    public static PlayerManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return instance;
        }
    }

    public BajtixPlayerController controller;
    public new PlayerCamera camera;

    private StatsDisplay display;
    private UIManager uI;

    private void Start() {
        display = GameObject.FindObjectOfType<StatsDisplay>();
        uI = UIManager.Instance;
    }

    public float maxHealth;
    private float health = 100;

    public void Hit(float dmg) {
        health -= dmg;
        if(health <= 0)
            Die();
        display.SetHealth(health);

    }

    public void Die() {
        Time.timeScale = 0.1f;
        uI.ShowDeath();
    }

}
