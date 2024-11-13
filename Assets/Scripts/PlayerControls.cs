using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (LoadScene())
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI Buttons functional)

public class PlayerControls : MonoBehaviour
{
    [SerializeField] PlayerSaveData playerSaveData;
    private Button backButton;
    AudioSource buttonAudioSource;

    private void OnEnable()
    {
        buttonAudioSource = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        var UIDoc = GetComponent<UIDocument>();
        backButton = UIDoc.rootVisualElement.Q("BackButton") as Button;
        backButton.RegisterCallback<ClickEvent>(LoadMainMenu);
    }

    private void LoadMainMenu(ClickEvent evt)
    {
        buttonAudioSource.volume = playerSaveData.sfxVolume / 100;
        buttonAudioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

}
