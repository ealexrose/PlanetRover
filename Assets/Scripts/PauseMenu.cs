using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    public GameObject pauseMenu;
    public GameObject resume;
    public GameObject restart;
    public bool alive;
    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        Paused = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
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

}
