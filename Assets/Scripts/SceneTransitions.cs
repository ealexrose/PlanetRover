using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class SceneTransitions : MonoBehaviour
{
    public Image blackout;
    public Animator blackoutTransition;
    public bool displayAdOnSceneChange;
    string gameId = "3655189";
    bool testMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Advertisement.Initialize(gameId, testMode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Blackout(string _targetLevel)
    {
        StartCoroutine(BlackoutRoutine(_targetLevel));
    }
    IEnumerator BlackoutRoutine(string _targetLevel)
    {
        Time.timeScale = 1f;
        //Play Animation
        blackoutTransition.SetTrigger("StartFade");
        //Wait


        yield return new WaitForSeconds(1.2f);

        if (Advertisement.IsReady("video"))
        {
            if (displayAdOnSceneChange)
            {
                Time.timeScale = 0f;
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("video", options);
            }
        }
        yield return new WaitForSeconds(.1f);
        //Load Scene
        SceneManager.LoadScene(_targetLevel);
    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Time.timeScale = 1f;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                Time.timeScale = 1f;
                break;
            case ShowResult.Failed:
                Time.timeScale = 1f;
                Debug.LogError("The ad failed to be shown.");
                break;

        }
    }
}
