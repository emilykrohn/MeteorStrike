using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html (Check if esc key is pressed)
// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/ (Pause game when menu open)
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI buttons functional)
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (How to use the LoadScene function)
// https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent (FindGameObjectWithTag)
// https://stackoverflow.com/questions/52406605/how-can-i-access-an-objects-components-from-a-different-scene (DontDestroyOnLoad and FindObjectWithTag)

public class PauseMenu : MonoBehaviour
{
    UIDocument UIDoc;
    Button resumeButton;
    Button settingsButton;
    Button saveButton;
    Button mainMenuButton;

    PlayerData playerData;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Get UIDocument Component from the current game object
        UIDoc = GetComponent<UIDocument>();

        // Finds the game object that is the PlayerData type in the scene
        playerData = FindAnyObjectByType<PlayerData>();

        // Find player game object with tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Is true when the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Makes the UIDoc visiable and the user can interact with it
            UIDoc.enabled = true;

            // Stop time from moving in the scene until the player resumes the game
            Time.timeScale = 0;

            // Gets all the buttons from the UIDoc and casts them to buttons
            resumeButton = UIDoc.rootVisualElement.Q("ResumeButton") as Button;
            settingsButton = UIDoc.rootVisualElement.Q("SettingsButton") as Button;
            saveButton = UIDoc.rootVisualElement.Q("SaveButton") as Button;
            mainMenuButton = UIDoc.rootVisualElement.Q("MainMenuButton") as Button;

            // When the Button is clicked, the function is called
            resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
            settingsButton.RegisterCallback<ClickEvent>(SettingsMenu);
            saveButton.RegisterCallback<ClickEvent>(SaveGame);
            mainMenuButton.RegisterCallback<ClickEvent>(LoadMainMenu);
        }
    }
    
    private void OnDisable()
    {
        // When UI Disabled, unregister callbacks
        resumeButton.UnregisterCallback<ClickEvent>(ResumeGame);
        settingsButton.UnregisterCallback<ClickEvent>(SettingsMenu);
        saveButton.UnregisterCallback<ClickEvent>(SaveGame);
        mainMenuButton.UnregisterCallback<ClickEvent>(LoadMainMenu);
    }

    /// <summary>
    /// Disables the Pause Screen UI and lets time pass in the scene
    /// </summary>
    /// <param name="evt"></param>
    void ResumeGame(ClickEvent evt)
    {
        UIDoc.enabled = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Saves the current player stats to the PlayerSaveData Scriptable Object
    /// </summary>
    /// <param name="evt"></param>
    void SaveGame(ClickEvent evt)
    {
        playerData.SaveStats();
    }

    /// <summary>
    /// Lets time pass in the Game scene and then loads the main menu scene
    /// </summary>
    /// <param name="evt"></param>
    void LoadMainMenu(ClickEvent evt)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Prevents the player from being destroyed so Settings Scene can use the Player Data Script, then Loads Settings Menu Scene
    /// </summary>
    /// <param name="evt"></param>
    void SettingsMenu(ClickEvent evt)
    {
        DontDestroyOnLoad(player);
        SceneManager.LoadScene("SettingsMenu");
    }
}
