using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public PauseMenu pauseMenu;
    public GameObject rover;
    public GameObject collectable;
    public float collectableStartDistance;
    public float collectableMinimumDistanceApart;
    [HideInInspector]
    public float score;
    public int digits;
    [HideInInspector]
    public string display;
    [HideInInspector]
    public Text displayText;
    public bool active = true;
    public bool endless;
    [System.Serializable]
    public enum MissionType
    {
        timed,
        collect,
        attack
    }
    public MissionType missionType;
    public float MissionTarget;
    public int missionNum;
    public string nextLevel;

    public float difficultyScaleFactor;
    public GameObject sceneTransitioner;
    public SaveSystem saveSystem;
    bool difficultyChanged;
    // Start is called before the first frame update
    void Start()
    {
        difficultyChanged = false;
        sceneTransitioner = GameObject.Find("SceneTransitionHolder");
        saveSystem = GetComponent<SaveSystem>();
        ScoreSet(MissionTarget);
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            CalculateScore();
            score = Mathf.Max(score, 0);
            this.gameObject.GetComponent<Text>().text = GetScoreText();
            if (!endless)
            {
                if (MissionComplete())
                {
                    active = false;

                    pauseMenu.WinScreen(missionNum);
                    Debug.Log("You did it!");
                }
            }
            if (endless && (missionType == MissionType.collect) && MissionComplete())
            {
                if (!difficultyChanged)
                {
                    saveSystem.SetEndlessDifficulty(saveSystem.GetDifficulty() + difficultyScaleFactor);
                    difficultyChanged = true;
                }
                sceneTransitioner.GetComponent<SceneTransitions>().Blackout(SceneManager.GetActiveScene().name);
            }
        }
        //Adjust Score

        //Display Score

        //if not endless, check to see if mission objective is complete
    }


    public void ScoreSet(float _missionTarget)
    {
        switch (missionType)
        {
            case MissionType.timed:
                if (!endless)
                {
                    score = _missionTarget;
                }
                else
                {
                    score = 0;
                }
                break;
            case MissionType.collect:
                score = 0;
                for (int i = 0; i < MissionTarget; i++)
                {
                    if (!rover.GetComponent<SpawnManager>().SpawnItem(collectable, rover.GetComponent<RoverController>().radius, collectableMinimumDistanceApart, collectableStartDistance))
                    {
                        i--;
                        collectableMinimumDistanceApart *= .9f;
                    }
                }
                if (endless)
                {
                    rover.GetComponent<SpawnManager>().enemyTimer /= saveSystem.GetDifficulty() + 1;
                }
                break;
            case MissionType.attack:
                score = 0;
                break;
            default:
                score = 0;
                break;

        }

    }
    public void CalculateScore()
    {
        switch (missionType)
        {
            case MissionType.timed:
                if (!endless)
                {
                    score -= Time.deltaTime;
                }
                else
                {
                    score += Time.deltaTime;
                }
                break;

            case MissionType.collect:
                break;

            case MissionType.attack:
                break;


        }

    }

    public string GetScoreText()
    {
        switch (missionType)
        {
            case MissionType.timed:
                return TimedScoreDisplay();

            case MissionType.collect:
                return CollectScoreDisplay();

            case MissionType.attack:
                return CollectScoreDisplay();

            default:
                return "Oops!";

        }
    }
    public string TimedScoreDisplay()
    {
        display = score.ToString("000.00");
        display = display + "s";
        return display;
    }
    public string CollectScoreDisplay()
    {
        display = score.ToString("00");
        displayText.alignment = TextAnchor.MiddleCenter;
        //if (!endless)
        //{
            display += "/" + MissionTarget.ToString("00");
        //}

        return display;

    }

    public bool MissionComplete()
    {
        switch (missionType)
        {
            case MissionType.timed:
                if (score <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case MissionType.collect:
                if (score >= MissionTarget)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case MissionType.attack:
                if (score >= MissionTarget)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;

        }
    }
}
