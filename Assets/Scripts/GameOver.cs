using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
public class GameOver : MonoBehaviour
{
    private Button restartButton;
    private Button quitButton;

    private void OnEnable()
    {
        var UIDoc = GetComponent<UIDocument>();

        restartButton = UIDoc.rootVisualElement.Q("RestartButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        restartButton.RegisterCallback<ClickEvent>(RestartGame);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    private void OnDisable()
    {
        restartButton.UnregisterCallback<ClickEvent>(RestartGame);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void RestartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}
