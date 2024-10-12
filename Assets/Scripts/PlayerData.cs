using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerSaveData SaveData;
    [SerializeField] int maxLives = 3;

    public int lives;

    void Start()
    {
        if (SaveData.isLoadGame)
        {
            LoadStats();
        }
        else
        {
            ResetStats();
        }
    }

    public void ResetStats()
    {
        lives = maxLives;
    }

    public void LoadStats()
    {
        lives = SaveData.lives;
    }

    public void SaveStats()
    {
        SaveData.lives = lives;
    }
}
