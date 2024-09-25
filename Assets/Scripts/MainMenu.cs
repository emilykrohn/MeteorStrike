using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
public class MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button quitButton;

    private void OnEnable()
    {
        var UIDoc = GetComponent<UIDocument>();

        startButton = UIDoc.rootVisualElement.Q("StartButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        startButton.RegisterCallback<ClickEvent>(StartGame);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}
