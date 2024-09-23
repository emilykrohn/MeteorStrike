using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
    }
    
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);
    }

    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }
}
