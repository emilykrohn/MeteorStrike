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
    [Header("PlayerSaveData Scriptable Objects")]
    [SerializeField] PlayerSaveData SaveData;
    [SerializeField] PlayerSaveData ResetData;
    [Header("Max Stats")]
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
        // Stats
        SaveData.health = ResetData.health;
        current_health = ResetData.health;

        current_speed = ResetData.speed;
        SaveData.speed = ResetData.speed;

        current_fire_rate = ResetData.fireRate;
        SaveData.fireRate = ResetData.fireRate;

        // Points
        SaveData.points = ResetData.points;
        current_points = ResetData.points;

        SaveData.pointsGoal = ResetData.pointsGoal;

        // Levels
        current_fire_rate_level = ResetData.fireRateLevel;
        SaveData.fireRateLevel = ResetData.fireRateLevel;

        current_speed_level = ResetData.speedLevel;
        SaveData.speedLevel = ResetData.speedLevel;
        
        SaveData.level = ResetData.level;
        current_level = ResetData.level;

        // Max Stats/Power ups
        SaveData.hasMaxStats = ResetData.hasMaxStats;
        current_has_max_stats = ResetData.hasMaxStats;
        
        SaveData.hasMaxPowerUps = ResetData.hasMaxPowerUps;
    }

    /// <summary>
    /// Loads the player's stats from the PlayerSaveData Scriptable Object
    /// </summary>
    public void LoadStats(SaveData saveObject)
    {
        // Stats
        SaveData.health = saveObject.health;
        current_health = saveObject.health;
        Debug.Log(SaveData.health.ToString() + "loadstats");
        Debug.Log(current_health.ToString() + " LoadStats: " + saveObject.health.ToString());

        current_speed = saveObject.speed;
        SaveData.speed = saveObject.speed;

        current_fire_rate = saveObject.fireRate;
        SaveData.fireRate = saveObject.fireRate;

        // Points
        SaveData.points = saveObject.points;
        current_points = saveObject.points;

        SaveData.pointsGoal = saveObject.pointsGoal;

        // Levels
        current_fire_rate_level = saveObject.fireRateLevel;
        SaveData.fireRateLevel = saveObject.fireRateLevel;

        current_speed_level = saveObject.speedLevel;
        SaveData.speedLevel = saveObject.speedLevel;
        
        SaveData.level = saveObject.level;
        current_level = saveObject.level;

        // Max Stats/Power ups
        SaveData.hasMaxStats = saveObject.hasMaxStats;
        current_has_max_stats = saveObject.hasMaxStats;
        
        SaveData.hasMaxPowerUps = saveObject.hasMaxPowerUps;
    }

    /// <summary>
    /// Saves the current player stats from the PlayerData script to the PlayerSaveData Scriptable Object
    /// </summary>
    public void SaveStats()
    {
        // Stats
        SaveData.health = current_health;
        SaveData.speed = current_speed;
        SaveData.fireRate = current_fire_rate;

        // Points
        SaveData.points = current_points;

        // Level
        SaveData.level = current_level;
        SaveData.fireRateLevel = current_fire_rate_level;
        SaveData.speedLevel = current_speed_level;
        
        // Max Stats/Power ups
        SaveData.hasMaxStats = current_has_max_stats;
        SaveData.hasMaxPowerUps = current_has_max_power_ups;
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
