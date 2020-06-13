using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
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

    public void Return()
    {
        sceneTransitioner.GetComponent<SceneTransitions>().Blackout("MainMenu");
    }
}
