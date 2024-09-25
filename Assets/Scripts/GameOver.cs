using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
// https://docs.unity3d.com/ScriptReference/Application.Quit.html
public class GameOver : MonoBehaviour
{
    private Button restartButton;
    private Button quitButton;

    private void OnEnable()
    {
        // Get where all of the UI elements are located
        var UIDoc = GetComponent<UIDocument>();

        // Find the Restart and Quit buttons in the UI Document as Buttons
        restartButton = UIDoc.rootVisualElement.Q("RestartButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        // When the buttons are pressed, call the RestartGame / QuitGame functions
        restartButton.RegisterCallback<ClickEvent>(RestartGame);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    private void OnDisable()
    {
        // Unregister Callbacks for both buttons when UI is disabled
        restartButton.UnregisterCallback<ClickEvent>(RestartGame);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void RestartGame(ClickEvent evt)
    {
        // Loads Game Scene
        SceneManager.LoadScene("Game");
    }

    private void QuitGame(ClickEvent evt)
    {
        // Closes Application
        Application.Quit();
    }
}
