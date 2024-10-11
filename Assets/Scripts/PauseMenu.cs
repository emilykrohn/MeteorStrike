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
    Button MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        UIDoc = GetComponent<UIDocument>();
    }

    void ResumeGame(ClickEvent evt)
    {
        UIDoc.enabled = false;
        Time.timeScale = 1;
    }

    void LoadMainMenu(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIDoc.enabled = true;
            Time.timeScale = 0;
            resumeButton = UIDoc.rootVisualElement.Q("ResumeButton") as Button;
            settingsButton = UIDoc.rootVisualElement.Q("SettingsButton") as Button;
            MainMenuButton = UIDoc.rootVisualElement.Q("MainMenuButton") as Button;
            resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
            MainMenuButton.RegisterCallback<ClickEvent>(LoadMainMenu);
        }
    }
}
