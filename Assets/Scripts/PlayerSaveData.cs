using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/class-ScriptableObject.html (How to use a scriptable object)
[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/PlayerSaveData", order = 1)]

// PlayerSaveData Scriptable Object is used to save and load player data
public class PlayerSaveData : ScriptableObject
{
    public int health = 100;
    public int points = 0;
    public int pointsGoal = 20;
    public int speed = 6;
    public float fireRate = 1f;
    public bool isLoadGame = false;
    public float musicVolume = 100;
    public float sfxVolume = 100;
    public int level = 1;
    public bool hasMaxPowerUps = false;
    public string previousScene = "MainMenu";
    public bool isSettingMenuOpened = false;
}
