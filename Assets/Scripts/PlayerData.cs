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

    /// <summary>
    /// Resets the player's stats in the PlayerSaveData Scriptable Object
    /// </summary>
    public void ResetStats()
    {
        SaveData.health = maxHealth;
        current_health = maxHealth;
        SaveData.points = 0;
        current_points = 0;
    }

    /// <summary>
    /// Loads the player's stats from the PlayerSaveData Scriptable Object
    /// </summary>
    public void LoadStats()
    {
        current_health = SaveData.health;
        current_points = SaveData.points;
    }

    /// <summary>
    /// Saves the current player stats from the PlayerData script to the PlayerSaveData Scriptable Object
    /// </summary>
    public void SaveStats()
    {
        SaveData.health = current_health;
        SaveData.points = current_points;
    }
}
