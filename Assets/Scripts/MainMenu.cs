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
    [SerializeField] PlayerSaveData playerSaveData;
    private Button newGameButton;
    private Button loadGameButton;
    private Button quitButton;
    public bool isLoadGame = true;

    private void OnEnable()
    {
        // Find UI Document on game object
        var UIDoc = GetComponent<UIDocument>();

        // Find buttons in UI Document as Buttons
        newGameButton = UIDoc.rootVisualElement.Q("NewGameButton") as Button;
        loadGameButton = UIDoc.rootVisualElement.Q("LoadGameButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        // Call start / quit game functions when buttons are clicked
        newGameButton.RegisterCallback<ClickEvent>(NewGame);
        loadGameButton.RegisterCallback<ClickEvent>(LoadGame);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    private void OnDisable()
    {
        // When UI Disabled, unregister callbacks
        loadGameButton.UnregisterCallback<ClickEvent>(LoadGame);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void LoadGame(ClickEvent evt)
    {
        // When start button clicked, load Game scene
        SceneManager.LoadScene("Game");
        playerSaveData.isLoadGame = true;
    }

    private void QuitGame(ClickEvent evt)
    {
        // When quit button clicked, close application
        Application.Quit();
    }

    private void NewGame(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
        playerSaveData.isLoadGame = false;
    }
}
