using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    


    public void StartGame() {
        GameManager.instance.StartGame();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Settings() {

    }
}

