using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private CanvasGroup death;

    [SerializeField] private GameObject waveAnnouncer;
    [SerializeField] private TextMeshProUGUI waveText;

    [SerializeField] public LoadoutDisplay loadoutDisplay;
    [SerializeField] public ShopTree shopDisplay;

    private static UIManager instance;

    private bool isPaused = false;

    private Tween<float> announcerTween;

    public GameObject menu;

    [SerializeField]
    private CanvasGroup shopMenu;

    public static UIManager Instance {
        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UIManager>();
            return instance;
        }
    }

    public void ShowDeath(string message = "<size=162>b</size>URWA") {
        death.TweenCanvasGroupAlpha(1, 1.5f).SetUseUnscaledTime(true).SetOnComplete(() => GameManager.instance.Death());
    }

    public void AnnounceNewWave(int waveNum, int enemyCount) {


        Announce($"<b>Wave {waveNum}</b>");
    }

    public void Announce(string text) {
        if (announcerTween != null)
            announcerTween.Cancel();
        waveText.text = text;
        announcerTween = waveAnnouncer.TweenAnchoredPositionY(-50, 0.1f).SetOnComplete(() => waveAnnouncer.TweenAnchoredPositionY(40, 0.1f).SetDelay(2));
    }

    public void ShowShopMenu() {
        shopMenu.TweenCancelAll();
        Cursor.lockState = CursorLockMode.None;
        shopMenu.gameObject.SetActive(true);
        shopMenu.TweenCanvasGroupAlpha(1, 0.2f).SetUseUnscaledTime(true);
    }

    public void RestartWave() {
        GameManager.instance.RestartWave();
    }

    public void ReplaytWave() {
        HideShopMenu();
        WaveManager.Instance.ReplayWave();
    }


    public void MainMenu() {
        GameManager.instance.MainMenu();
    }

    public void HideShopMenu() {
        GameManager.loadout = WeaponManager.Instance.weapons;
        Cursor.lockState = CursorLockMode.Locked;
        shopMenu.TweenCanvasGroupAlpha(0, 0.2f).SetUseUnscaledTime(true).SetOnComplete(() => {
            shopMenu.gameObject.SetActive(false);
        });
    }

    public void ShopMenuNewWave() {
        HideShopMenu();
        // gameObject.TweenDelayedInvoke(3,()=>WaveManager.Instance.SpawnWave());
        WaveManager.Instance.SpawnWave();
    }

    private void Update() {
        Cursor.visible = Cursor.lockState != CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape) && death.alpha < 0.5f) {
            Pause();

        }
    }

    public void Pause() {
        if (shopDisplay.gameObject.activeSelf)
            HideShopMenu();

        print("Pause");
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0.001f : 1f;

        menu.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}