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
    ProgressBar pointsBar;
    ProgressBar healthBar;

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

        // Find Points and Lives Labels in UI Document as a Label or ProgressBar
        pointsBar = UIDoc.rootVisualElement.Q("PointsBar") as ProgressBar;
        healthBar = UIDoc.rootVisualElement.Q("HealthBar") as ProgressBar;
        
        // Update the healthBar value when GameUI is enabled
        healthBar.value = playerSaveData.health;
        healthBar.title = "Health: " + healthBar.value.ToString();
        pointsBar.value = playerSaveData.points;
        pointsBar.title = "Points: " + playerSaveData.points.ToString();
    }

    /// <summary>
    /// This increases the player points by 1 and updates the GameUI pointsLabel. Function called in Stars Script
    /// </summary>
    public void IncreasePoints(int new_points)
    {
        // Increase current points by the new_points amount and update points label to this new number
        playerData.current_points += new_points;
        pointsBar.value = playerData.current_points;
        pointsBar.title = "Points: " + playerData.current_points.ToString();
    }

    /// <summary>
    /// This decreases the player lives by 1 and updates the GameUI livesLabel. If the player lives reaches below 0, the GameOver Scene is Loaded. Function called in Meteor Script
    /// </summary>
    public void DecreaseHealth(int damage)
    {
        // Decrease current lives by damage amount
        playerData.current_health -= damage;

        // Check if lives is less than or equal to zero, if so then load game over scene
        if (playerData.current_health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // otherwise, update healthBar to new health amount
            healthBar.value = playerData.current_health;
            healthBar.title = "Health: " + healthBar.value.ToString();
        }
    }
}
