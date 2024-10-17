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
    [SerializeField] AudioClip levelUpSound;
    PowerUpScreen powerUpScreen;
    PlayerData playerData;
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

        maxPowerUpsLabel = UIDoc.rootVisualElement.Q("MaxPowerUpsLabel") as Label;

    }

    public void UpdateMaxPowerLabel()
    {
        if (playerData.current_has_max_stats)
        {
            maxPowerUpsLabel.style.display = DisplayStyle.Flex;
        }
        else
        {
            maxPowerUpsLabel.style.display = DisplayStyle.None;
        }
    }

    public void UpdatePointsGoal()
    {
        AudioSource.PlayClipAtPoint(levelUpSound, transform.position, playerSaveData.sfxVolume / 100);
        playerSaveData.pointsGoal = (int)(0.25 * Math.Pow(playerData.current_level, 2) + 20);
        pointsBar.highValue = playerSaveData.pointsGoal;
        playerData.current_level++;
        pointsBar.title = "Level: " + playerData.current_level.ToString();
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
        playerData.current_points += new_points;
        if (playerData.current_points >= playerSaveData.pointsGoal)
        {
            playerData.current_points -= playerSaveData.pointsGoal;
            UpdatePointsGoal();
        }
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
        playerData.current_health += health;
        if (playerData.current_health > 100)
        {
            playerData.current_health = 100;
        }
        else
        {
            healthBar.value = playerData.current_health;
            healthBar.title = "Health: " + healthBar.value.ToString();
        }
    }
}
