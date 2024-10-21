using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/class-ScriptableObject.html (How to use a scriptable object)
// https://docs.unity3d.com/2022.3/Documentation/ScriptReference/HeaderAttribute.html (Header Attribute)

/// <summary>
/// PlayerData Script is used to change the PlayerSaveData Scriptable Object data and store the current stats of the player
/// </summary>
public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerSaveData SaveData;
    [Header("Max Stats")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthPowerUpAmount = 10;
    public bool current_has_max_stats;
    public bool current_has_max_power_ups;

    // Current lives the player has during the game
    [Header("Current Stats")]
    public int current_health;
    public int current_points;
    public int current_speed;
    public float current_fire_rate;
    [Header("Current Level")]
    public int current_level;
    public int current_fire_rate_level;
    public int current_speed_level;
    GameUI gameUI;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    /// <summary>
    /// Resets the player's stats in the PlayerSaveData Scriptable Object
    /// </summary>
    public void ResetStats()
    {
        SaveData.health = maxHealth;
        current_health = maxHealth;

        SaveData.points = 0;
        current_points = 0;

        SaveData.level = 1;
        current_level = 1;

        SaveData.pointsGoal = 20;

        current_speed = 6;
        SaveData.speed = 6;

        current_fire_rate = 1f;
        SaveData.fireRate = 1f;

        SaveData.hasMaxPowerUps = false;

        current_fire_rate_level = 1;
        SaveData.fireRateLevel = 1;

        current_speed_level = 1;
        SaveData.speedLevel = 1;

        SaveData.hasMaxStats = false;
        current_has_max_stats = false;
    }

    /// <summary>
    /// Loads the player's stats from the PlayerSaveData Scriptable Object
    /// </summary>
    public void LoadStats()
    {
        current_health = SaveData.health;
        current_points = SaveData.points;
        current_level = SaveData.level;
        current_speed = SaveData.speed;
        current_fire_rate = SaveData.fireRate;
        current_has_max_power_ups = SaveData.hasMaxPowerUps;
        current_fire_rate_level = SaveData.fireRateLevel;
        current_speed_level = SaveData.speedLevel;
        current_has_max_stats = SaveData.hasMaxStats;
    }

    /// <summary>
    /// Saves the current player stats from the PlayerData script to the PlayerSaveData Scriptable Object
    /// </summary>
    public void SaveStats()
    {
        SaveData.health = current_health;
        SaveData.points = current_points;
        SaveData.level = current_level;
        SaveData.speed = current_speed;
        SaveData.fireRate = current_fire_rate;
        SaveData.hasMaxPowerUps = current_has_max_power_ups;
        SaveData.fireRateLevel = current_fire_rate_level;
        SaveData.speedLevel = current_speed_level;
        SaveData.hasMaxStats = current_has_max_stats;
    }

    /// <summary>
    /// Updates the PlayerSaveData SFX volume value
    /// </summary>
    /// <param name="value">SFX Volume Percentage</param>
    public void UpdateSFXVolume(int value)
    {
        SaveData.sfxVolume = value;
    }

    /// <summary>
    /// Updates the PlayerSave Data Music volume value
    /// </summary>
    /// <param name="value">Music Volume Percentage</param>
    public void UpdateMusicVolume(int value)
    {
        SaveData.musicVolume = value;
    }

    public void HealPowerUp()
    {
        gameUI.IncreaseHealth(healthPowerUpAmount);
    }

    public void SpeedPowerUp()
    {
        current_speed += 2;
    }

    public void FireRatePowerUp()
    {
        current_fire_rate -= 0.2f;
    }
}
