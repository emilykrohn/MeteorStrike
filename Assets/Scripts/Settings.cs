using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI buttons functional)
// https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Slider.html (Slider)
// https://stackoverflow.com/questions/52406605/how-can-i-access-an-objects-components-from-a-different-scene (DontDestroyOnLoad and FindObjectWithTag)

// https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEngine.UIElements.ChangeEvent-1.html (ChangeEvent<T>)
// https://docs.unity3d.com/ScriptReference/UIElements.ChangeEvent_1.html (newValue)
// https://docs.unity3d.com/Manual/UIE-Change-Events.html (ChangeEvent<T>, Example 2)

public class Settings : MonoBehaviour
{
    UIDocument UIDoc;
    [SerializeField]
    PlayerSaveData playerSaveData;

    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;

    PlayerData saveData;

    void OnEnable()
    {
        // Get Player Data Script to update the music and sfx volume
        saveData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        UIDoc = GetComponent<UIDocument>();

        // Find the sliders in the UIDoc
        musicVolumeSlider = UIDoc.rootVisualElement.Q("MusicVolume") as Slider;
        sfxVolumeSlider = UIDoc.rootVisualElement.Q("SFXVolume") as Slider;

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
    }
}