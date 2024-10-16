using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/class-ScriptableObject.html (How to use a scriptable object)

/// <summary>
/// PlayerData Script is used to change the PlayerSaveData Scriptable Object data and store the current stats of the player
/// </summary>
public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerSaveData SaveData;
    [SerializeField] int maxHealth = 100;

    // Current lives the player has durring the game
    public int current_health;
    public int current_points;
    public int current_level;

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
    }

    /// <summary>
    /// Loads the player's stats from the PlayerSaveData Scriptable Object
    /// </summary>
    public void LoadStats()
    {
        current_health = SaveData.health;
        current_points = SaveData.points;
        current_level = SaveData.level;
    }

    /// <summary>
    /// Saves the current player stats from the PlayerData script to the PlayerSaveData Scriptable Object
    /// </summary>
    public void SaveStats()
    {
        SaveData.health = current_health;
        SaveData.points = current_points;
        SaveData.level = current_level;
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
}
