using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (LoadScene())
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI Buttons functional)
// https://docs.unity3d.com/ScriptReference/Application.Quit.html (Close Application)
// https://discussions.unity.com/t/volume-of-audio-clip-is-too-loud/135791 (Audio Clip)
// https://stackoverflow.com/questions/52406605/how-can-i-access-an-objects-components-from-a-different-scene (DontDestroyOnLoad and FindObjectWithTag)
// https://stackoverflow.com/questions/71119026/unity-enable-disable-component-script-from-component-on-same-gameobject (enable components)

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerSaveData playerSaveData;
    PlayerInput playerInput;
    
    PlayerMovement playerMovement;
    Spawner spawner;
    Shoot shoot;

    // Audio Sources
    AudioSource buttonAudioSource;
    AudioSource music;

    // UI Elements
    private Button newGameButton;
    private Button loadGameButton;
    private Button settingsButton;
    private Button quitButton;
    public bool isLoadGame = true;


    private void OnEnable()
    {
        playerSaveData.isSettingMenuOpened = false;
        playerSaveData.previousScene = "MainMenu";

        buttonAudioSource = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        playerInput = FindObjectOfType<PlayerInput>();
        spawner = FindObjectOfType<Spawner>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        shoot = FindObjectOfType<Shoot>();

        DontDestroyOnLoad(buttonAudioSource);
        DontDestroyOnLoad(music);

        music.volume = playerSaveData.musicVolume / 100;
        music.Play();
        // Find UI Document on game object
        var UIDoc = GetComponent<UIDocument>();

        // Find buttons in UI Document as Buttons
        newGameButton = UIDoc.rootVisualElement.Q("NewGameButton") as Button;
        loadGameButton = UIDoc.rootVisualElement.Q("LoadGameButton") as Button;
        settingsButton = UIDoc.rootVisualElement.Q("SettingsButton") as Button;
        quitButton = UIDoc.rootVisualElement.Q("QuitButton") as Button;

        // Call start / quit game functions when buttons are clicked
        newGameButton.RegisterCallback<ClickEvent>(NewGame);
        loadGameButton.RegisterCallback<ClickEvent>(LoadGame);
        settingsButton.RegisterCallback<ClickEvent>(SettingsMenu);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    /// <summary>
    /// Loads the Game scene and the PlayerSaveData isLoadGame is set to true so the player stats will be reset in the PlayerData function
    /// </summary>
    /// <param name="evt"></param>
    private void LoadGame(ClickEvent evt)
    {
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();
        // When start button clicked, load Game scene
        SceneManager.LoadScene("Game");
        playerSaveData.previousScene = "Game";
        playerSaveData.isLoadGame = true;

        playerInput.enabled = true;
        spawner.enabled = true;
        playerMovement.enabled = true;
        shoot.enabled = true;
    }

    /// <summary>
    /// Closes the Game Application
    /// </summary>
    /// <param name="evt"></param>
    private void QuitGame(ClickEvent evt)
    {
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();
        // When quit button clicked, close application
        Application.Quit();
    }

    /// <summary>
    /// Loads the Game scene and the PlayerSaveData isLoadGame is set to false so the player stats will be reset in the PlayerData function
    /// </summary>
    /// <param name="evt"></param>
    private void NewGame(ClickEvent evt)
    {
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();
        SceneManager.LoadScene("Game");
        // Keep track of where back button in settings goes to
        playerSaveData.previousScene = "Game";
        playerSaveData.isLoadGame = false;
        
        playerInput.enabled = true;
        spawner.enabled = true;
        playerMovement.enabled = true;
        shoot.enabled = true;
    }
    
    /// <summary>
    /// Prevents the player from being destroyed so Settings Scene can use the Player Data Script, then Loads Settings Menu Scene
    /// </summary>
    /// <param name="evt"></param>
    void SettingsMenu(ClickEvent evt)
    {
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();
        SceneManager.LoadScene("SettingsMenu");
    }
}
