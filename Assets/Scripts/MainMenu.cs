using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
// https://docs.unity3d.com/ScriptReference/Application.Quit.html
public class MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button quitButton;

    private void OnEnable()
    {
        // Find UI Document on game object
        var UIDoc = GetComponent<UIDocument>();

        // Find buttons in UI Document as Buttons
        startButton = UIDoc.rootVisualElement.Q("StartButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        // Call start / quit game functions when buttons are clicked
        startButton.RegisterCallback<ClickEvent>(StartGame);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    private void OnDisable()
    {
        // When UI Disabled, unregister callbacks
        startButton.UnregisterCallback<ClickEvent>(StartGame);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        // When start button clicked, load Game scene
        SceneManager.LoadScene("Game");
    }

    private void QuitGame(ClickEvent evt)
    {
        // When quit button clicked, close application
        Application.Quit();
    }
}
