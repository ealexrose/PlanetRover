using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject sceneTransitioner;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransitioner = GameObject.Find("SceneTransitionHolder");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("LevelSelect");
        sceneTransitioner.GetComponent<SceneTransitions>().Blackout("LevelSelect");
    }
    public void Options()
    {
        Debug.Log("Load The Options Menu");
    }
    public void Credits()
    {
        Debug.Log("go to credits screen");
    }

}
