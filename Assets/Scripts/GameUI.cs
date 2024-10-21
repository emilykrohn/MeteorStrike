using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html (LoadScene())
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI Buttons functional)
// https://www.geeksforgeeks.org/c-sharp-math-pow-method/# (Power function)
public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerSaveData playerSaveData;
    PlayerData playerData;

    [SerializeField] AudioClip levelUpSound;

    // UI
    PowerUpScreen powerUpScreen;
    ProgressBar pointsBar;
    ProgressBar healthBar;
    
    public Label maxPowerUpsLabel;

    private void OnEnable()
    {
        playerData = FindAnyObjectByType<PlayerData>();
        powerUpScreen = FindAnyObjectByType<PowerUpScreen>();

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
        
        // Update the healthBar and pointsBar value when GameUI is enabled
        healthBar.value = playerSaveData.health;
        healthBar.title = "Health: " + healthBar.value.ToString();
        pointsBar.value = playerSaveData.points;
        pointsBar.title = "Level: " + playerSaveData.level.ToString();
        pointsBar.highValue = playerSaveData.pointsGoal;
    }

    public void UpdatePointsGoal()
    {
        // Play level up sound
        AudioSource.PlayClipAtPoint(levelUpSound, transform.position, playerSaveData.sfxVolume / 100);

        // Update player points goal
        playerSaveData.pointsGoal = (int)(0.25 * Math.Pow(playerData.current_level, 2) + 20);

        // Update the new high value for the points progress bar
        pointsBar.highValue = playerSaveData.pointsGoal;

        // Increase the current player level
        playerData.current_level++;

        // Update the points progress bar title to the new player level
        pointsBar.title = "Level: " + playerData.current_level.ToString();

        // If the player doesn't have max stats, show power up screen
        if (!playerData.current_has_max_stats)
        {
            powerUpScreen.EnablePowerUpScreen();
        }
    }

    /// <summary>
    /// This increases the player points by 1 and updates the GameUI pointsLabel. Function called in Stars Script
    /// </summary>
    public void IncreasePoints(int new_points)
    {
        // Increase current points by new_points
        playerData.current_points += new_points;

        // If the new current_points amount is over the points goal, then update the points goal
        if (playerData.current_points >= playerSaveData.pointsGoal)
        {
            // If the player went over the goal amount, let them keep the extra points they earned
            playerData.current_points -= playerSaveData.pointsGoal;
            UpdatePointsGoal();
        }

        // Update the points progress bar value by the new current points amount
        pointsBar.value = playerData.current_points;
    }

    /// <summary>
    /// This decreases the player lives by 1 and updates the GameUI livesLabel. If the player lives reaches below 0, the GameOver Scene is Loaded. Function called in Meteor Script
    /// </summary>
    public void DecreaseHealth(int damage)
    {
        // Decrease current lives by damage amount
        playerData.current_health -= damage;
        
        playerData.current_has_max_stats = false;

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

    public void IncreaseHealth(int health)
    {
        // Increase the player's current health by health arguement amount
        playerData.current_health += health;

        // If the player's health is more than 100 after adding the arguement health amount, set the current health to 100
        if (playerData.current_health > 100)
        {
            playerData.current_health = 100;
        }
        else
        {
            // otherwise, update healthBar to new health amount
            healthBar.value = playerData.current_health;
            healthBar.title = "Health: " + healthBar.value.ToString();
        }
    }
}
