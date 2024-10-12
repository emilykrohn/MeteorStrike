using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (LoadScene())
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI Buttons functional)
public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerSaveData playerSaveData;
    PlayerData playerData;
    Label pointsLabel;
    int points = 0;
    Label livesLabel;

    private void OnEnable()
    {
        playerData = FindAnyObjectByType<PlayerData>();

        // If the player choose the load game option, it loads that game stats from the PlayerSaveData
        if (playerSaveData.isLoadGame)
        {
            playerData.LoadStats();
        }
        // Else the game data is reset
        else
        {
            playerData.ResetStats();
        }

        // Find UI Document on game object
        var UIDoc = GetComponent<UIDocument>();

        // Find Points and Lives Labels in UI Document as Labels
        pointsLabel = UIDoc.rootVisualElement.Q("Points") as Label;
        livesLabel = UIDoc.rootVisualElement.Q("Lives") as Label;
        
        // Update the livesLabel text when GameUI is enabled
        livesLabel.text = "Lives: " + playerData.lives.ToString();
    }

    /// <summary>
    /// This increases the player points by 1 and updates the GameUI pointsLabel. Function called in Stars Script
    /// </summary>
    public void IncrementPoints()
    {
        // Increase current points by 1 and update points label to this new number
        points++;
        pointsLabel.text = "Points: " + points.ToString();
    }

    /// <summary>
    /// This decreases the player lives by 1 and updates the GameUI livesLabel. If the player lives reaches below 0, the GameOver Scene is Loaded. Function called in Meteor Script
    /// </summary>
    public void DecrementLives()
    {
        // Decrease current lives by 1
        playerData.lives--;
        // Check if lives is less than zero, if so then load game over scene
        if (playerData.lives < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // otherwise, update lives label to new lives count
            livesLabel.text = "Lives: " + playerData.lives.ToString();
        }
    }
}
