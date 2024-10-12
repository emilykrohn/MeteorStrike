using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html (Check if esc key is pressed)
// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/ (Pause game when menu open)
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html

public class PauseMenu : MonoBehaviour
{
    UIDocument UIDoc;
    Button resumeButton;
    Button settingsButton;
    Button saveButton;
    Button mainMenuButton;

    PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        UIDoc = GetComponent<UIDocument>();
        playerData = FindAnyObjectByType<PlayerData>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIDoc.enabled = true;
            Time.timeScale = 0;
            resumeButton = UIDoc.rootVisualElement.Q("ResumeButton") as Button;
            settingsButton = UIDoc.rootVisualElement.Q("SettingsButton") as Button;
            saveButton = UIDoc.rootVisualElement.Q("SaveButton") as Button;
            mainMenuButton = UIDoc.rootVisualElement.Q("MainMenuButton") as Button;

            resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
            saveButton.RegisterCallback<ClickEvent>(SaveGame);
            mainMenuButton.RegisterCallback<ClickEvent>(LoadMainMenu);
        }
    }

    void ResumeGame(ClickEvent evt)
    {
        UIDoc.enabled = false;
        Time.timeScale = 1;
    }

    void SaveGame(ClickEvent evt)
    {
        playerData.SaveStats();
    }

    void LoadMainMenu(ClickEvent evt)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
