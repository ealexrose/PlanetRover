using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Image blackout;
    public Animator blackoutTransition;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
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
        yield return new WaitForSeconds(1f);
        //Load Scene
        SceneManager.LoadScene(_targetLevel);
    }
}
