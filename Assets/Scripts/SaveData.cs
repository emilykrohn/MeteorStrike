using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    [Header("Stats")]
    public int health = 100;
    public int speed = 6;
    public float fireRate = 1f;

    [Header("Points")]
    public int points = 0;
    public int pointsGoal = 20;

    [Header("Volume")]
    public float musicVolume = 100;
    public float sfxVolume = 100;

    [Header("Levels")]
    public int level = 1;
    public int speedLevel = 1;
    public int fireRateLevel = 1;

    [Header("Max Stats")]
    public int maxSpeedLevel = 4;
    public int maxFireRateLevel = 5;
    public string previousScene = "MainMenu";
    
    [Header("Booleans")]
    public bool hasMaxPowerUps = false;
    public bool isLoadGame = false;
    public bool isSettingMenuOpened = false;
    public bool hasMaxStats = false;
}
