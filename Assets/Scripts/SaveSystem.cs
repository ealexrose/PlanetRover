using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public int LevelsPassed = 0;

    public void SaveGame(int levelsPassed)
    {
        PlayerPrefs.SetInt("LevelsPassed", levelsPassed);
        PlayerPrefs.Save();
        Debug.Log("Levels Passed: " + LevelsPassed);
    }

    public int LoadGame()
    {
        LevelsPassed = PlayerPrefs.GetInt("LevelsPassed");
        return LevelsPassed;
    }
}
