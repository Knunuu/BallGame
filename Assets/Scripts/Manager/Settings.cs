using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsUI();
        }
    }

    private void ToggleSettingsUI()
    {
        if (settingsUI != null)
        {
            settingsUI.SetActive(!settingsUI.activeSelf);
            Time.timeScale = settingsUI.activeSelf ? 0f : 1f; // Pause or resume the game
        }
    }
}
