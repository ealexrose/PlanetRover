using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Mission missionGuide;
    public BriefingController briefingController;
    public Button button;


    // Start is called before the first frame update
    void Start()
    {

        briefingController = this.GetComponentInParent<BriefingController>();
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        Debug.Log("got here");
        briefingController.SelectMission(missionGuide);
    }
}
