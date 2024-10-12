using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/class-ScriptableObject.html
[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/PlayerSaveData", order = 1)]
public class PlayerSaveData : ScriptableObject
{
    public int lives = 3;
    public bool isLoadGame = false;
}
