﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BriefingController : MonoBehaviour
{
    public List<GameObject> missionList;
    public Mission cachedMission;
    public Text briefingDisplay;
    public Toggle endless;
    public GameObject endlessDisable;

    [HideInInspector]
    public SaveSystem saveSystem;
    public int LevelsPassed;
    // Start is called before the first frame update
    void Start()
    {


        //Gotta have the SaveSystem script together with PauseMenu!
        saveSystem = GetComponent<SaveSystem>();
        //Loads the amount of levels passed
        LevelsPassed = saveSystem.LoadGame();
        Debug.Log(LevelsPassed);

        endless.isOn = false;
        endlessDisable.SetActive(false);
        foreach (Transform child in transform)
        {
            missionList.Add(child.gameObject);
            if (child.GetComponent<LevelSelector>())
            {
                child.GetComponent<LevelSelector>().missionGuide.missionID = missionList.Count;
                if (child.GetComponent<LevelSelector>().missionGuide.missionID > LevelsPassed)
                {
                child.GetComponent<LevelSelector>().missionGuide.isBeaten =  false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMission(Mission _missionGuide)
    {
        cachedMission = _missionGuide;

        
        endless.interactable = _missionGuide.isBeaten;
        if (!_missionGuide.isBeaten)
        {
            endlessDisable.SetActive(true);
            endless.isOn = false;
        }
        else
        {
            endlessDisable.SetActive(false);
        }
        briefingDisplay.text = _missionGuide.missionBriefing;
        Debug.Log(_missionGuide.mission);
    }

    public void Launch()
    {
        if (endless.isOn)
        {
            SceneManager.LoadScene(cachedMission.endlessMission);
        }
        else
        {
            SceneManager.LoadScene(cachedMission.mission);
        }
        

    }
}
