
using UnityEngine;
using UnityEngine.UI;

public static class GameSettings {
    public static float volume = 1;
    public static bool allowSlam = true;
    public static bool accessibleMode = false;
}

public class SettingsManager : MonoBehaviour {
    [SerializeField] private Slider m_setVolume;
    [SerializeField] private Toggle m_setAllowSlam;
    [SerializeField] private Toggle m_setAccessibleMode;

    private void OnEnable() {
        m_setVolume.value = GameSettings.volume;
        m_setAllowSlam.isOn = GameSettings.allowSlam;
        m_setAccessibleMode.isOn = GameSettings.accessibleMode;


        m_setVolume.onValueChanged.AddListener(f => GameSettings.volume = f);
        m_setAllowSlam.onValueChanged.AddListener(f => GameSettings.allowSlam = f);
        m_setAccessibleMode.onValueChanged.AddListener(f => GameSettings.accessibleMode = f);
    }

    private void Update() {
        AudioListener.volume = GameSettings.volume;
    }

    private void OnDisable() {
        m_setVolume.onValueChanged.RemoveAllListeners();
        m_setAllowSlam.onValueChanged.RemoveAllListeners();
        m_setAccessibleMode.onValueChanged.RemoveAllListeners();
    }


}
