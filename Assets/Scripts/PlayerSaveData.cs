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
    public bool isLoadGame = false;
    public int musicVolume = 100;
    public int sfxVolume = 100;
    public string previousScene = "MainMenu";
    public bool isSettingMenuOpened = false;
}
