using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    public GameObject pauseMenu;
    public GameObject resume;
    public GameObject restart;
    public GameObject continueButton;
    public ScoreManager scoreManager;
    public bool alive;
    //Just testing for now
    [HideInInspector]
    public SaveSystem saveSystem;
    public int LevelsPassed;
    public Button PauseButton;
    public GameObject sceneTransitioner;
    // Start is called before the first frame update
    void Start()
    {

        sceneTransitioner = GameObject.Find("SceneTransitionHolder");
        PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        //Should Pause the Game when clicked on
        PauseButton.onClick.AddListener(PauseGame);
        //Gotta have the SaveSystem script together with PauseMenu!
        saveSystem = GetComponent<SaveSystem>();
        //Loads the amount of levels passed
        LevelsPassed = saveSystem.LoadGame();
        Time.timeScale = 1f;
        alive = true;
        Paused = false;
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !Paused)
        {
            Debug.Log("got here");
            PauseGame();
        }
        else if (Input.GetButtonDown("Cancel") && Paused && alive)
        {
            UnPauseGame();
        }
    }

    public void Restart()
    {
        sceneTransitioner.GetComponent<SceneTransitions>().Blackout(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        sceneTransitioner.GetComponent<SceneTransitions>().Blackout("MainMenu");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void EndScreen()
    {
        alive = false;
        resume.SetActive(false);
        restart.SetActive(true);
        PauseGame();
        Time.timeScale = 1f;
    }

    public void WinScreen(int _missionNum)
    {
        alive = false;
        resume.SetActive(false);
        restart.SetActive(false);
        continueButton.SetActive(true);
        PauseGame();
        Time.timeScale = .1f;

        //Save Progress
        LevelsPassed = _missionNum;
        saveSystem.SaveGame(LevelsPassed);
    }

    public void Continue()
    {
        sceneTransitioner.GetComponent<SceneTransitions>().Blackout(scoreManager.nextLevel);
    }

}
