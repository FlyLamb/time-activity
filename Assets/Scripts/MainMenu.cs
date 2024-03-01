using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject m_gameTitlePanel, m_settingsPanel;
    private bool m_settingsActive = false;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame() {
        Fader.Instance.FadeIn(() => {
            GameManager.instance.StartGame();
        });
    }

    public void QuitGame() {
        if (Application.platform != RuntimePlatform.WebGLPlayer) {
            Application.Quit();
        }
    }

    public void Settings() {
        if (!m_settingsActive) {
            m_gameTitlePanel.SetActive(false);
            m_settingsPanel.SetActive(true);
        } else {
            m_gameTitlePanel.SetActive(true);
            m_settingsPanel.SetActive(false);
        }

        m_settingsActive = !m_settingsActive;
    }
}

