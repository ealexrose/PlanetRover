using System.Collections;
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
    public GameObject sceneTransitioner;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransitioner = GameObject.Find("SceneTransitionHolder");
        //Gotta have the SaveSystem script together with PauseMenu!
        saveSystem = GetComponent<SaveSystem>();
        //Loads the amount of levels passed
        LevelsPassed = saveSystem.LoadGame();
        saveSystem.SetEndlessDifficulty(0);
        Debug.Log(LevelsPassed);

        endless.isOn = false;
        endlessDisable.SetActive(false);
        foreach (Transform child in transform)
        {
            
            if (child.GetComponent<LevelSelector>())
            {
                missionList.Add(child.gameObject);
                child.GetComponent<LevelSelector>().missionGuide.missionID = missionList.Count;
                if (child.GetComponent<LevelSelector>().missionGuide.missionID > LevelsPassed)
                {
                    child.GetComponent<LevelSelector>().missionGuide.isBeaten = false;
                }
                else
                {
                    child.GetComponent<LevelSelector>().missionGuide.isBeaten = true;
                }
                if (child.GetComponent<LevelSelector>().missionGuide.missionID - 1 > LevelsPassed)
                {
                    child.GetComponent<Button>().interactable = false;

                }
                else
                {
                    child.GetComponent<Button>().interactable = true;
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
            if (cachedMission.endlessMission != "")
            {
                sceneTransitioner.GetComponent<SceneTransitions>().Blackout(cachedMission.endlessMission);
            }

        }
        else
        {
            if (cachedMission.mission != "")
            {
                sceneTransitioner.GetComponent<SceneTransitions>().Blackout(cachedMission.mission);
            }
        }
        

    }
}
