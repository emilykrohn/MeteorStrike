using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI buttons functional)
// https://stackoverflow.com/questions/52406605/how-can-i-access-an-objects-components-from-a-different-scene (DontDestroyOnLoad and FindObjectWithTag)

// https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEngine.UIElements.ChangeEvent-1.html (ChangeEvent<T>)
// https://docs.unity3d.com/ScriptReference/UIElements.ChangeEvent_1.html (newValue)
// https://docs.unity3d.com/Manual/UIE-Change-Events.html (ChangeEvent<T>, Example 2)

// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (How to use the LoadScene function)

public class Settings : MonoBehaviour
{
    [SerializeField] PlayerSaveData playerSaveData;

    // UI Elements
    UIDocument UIDoc;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;
    Button backButton;

    // Audio Sources
    AudioSource buttonAudioSource;
    AudioSource music;
    
    GameObject player;
    PlayerData saveData;

    void OnEnable()
    {
        // Find Audio Sources in the game scene
        buttonAudioSource = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        // Get Player Data Script to update the music and sfx volume
        player = GameObject.FindGameObjectWithTag("Player");
        saveData = player.GetComponent<PlayerData>();

        UIDoc = GetComponent<UIDocument>();

        // Find the sliders in the UIDoc
        musicVolumeSlider = UIDoc.rootVisualElement.Q("MusicVolume") as Slider;
        sfxVolumeSlider = UIDoc.rootVisualElement.Q("SFXVolume") as Slider;
        backButton = UIDoc.rootVisualElement.Q("BackButton") as Button;

        // Set the initial value that has been saved in the playerSaveData scriptable object
        musicVolumeSlider.value = playerSaveData.musicVolume;
        sfxVolumeSlider.value = playerSaveData.sfxVolume;

        // When the slider value changes, it calls the UpdateMusicVolume function
        musicVolumeSlider.RegisterValueChangedCallback((evt) => 
        {
            saveData.UpdateMusicVolume((int)evt.newValue);
        });

        // When the slider value changes, it call the UpdateSFXVolume function
        sfxVolumeSlider.RegisterValueChangedCallback((evt) =>
        {
            saveData.UpdateSFXVolume((int)evt.newValue);
        });

        backButton.RegisterCallback<ClickEvent>(LoadPreviousScene);
    }

    // This is for the back button
    void LoadPreviousScene(ClickEvent evt)
    {
        // Play button sound at current saved volume level
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();

        // Play music at current saved volume level
        music.volume = playerSaveData.musicVolume / 100;
        music.Play();

        // Destroy player game object
        Destroy(player);

        // Save that the player has just closed the settings menu, this way the pause menu can open again if that was the previous menu
        playerSaveData.isSettingMenuOpened = true;

        // Load previous menu
        SceneManager.LoadScene(playerSaveData.previousScene);
    }
}