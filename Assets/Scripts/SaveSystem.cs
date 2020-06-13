using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public int LevelsPassed = 0;

    public void SaveGame(int _levelsPassed)
    {
        PlayerPrefs.SetInt("LevelsPassed", _levelsPassed);
        PlayerPrefs.Save();
        Debug.Log("Levels Passed: " + LevelsPassed);
    }
    public void SetEndlessDifficulty(float _difficulty)
    {
        PlayerPrefs.SetFloat("difficulty", _difficulty);
        PlayerPrefs.Save();

    }
    public float GetDifficulty()
    {
        float difficulty = PlayerPrefs.GetFloat("difficulty");
        return difficulty;
    }

    public int LoadGame()
    {
        LevelsPassed = PlayerPrefs.GetInt("LevelsPassed");
        return LevelsPassed;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SaveGame(0);
            Debug.Log("progress reset");
        }
    }
}
