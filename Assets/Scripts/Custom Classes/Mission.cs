using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public string mission;
    public string endlessMission;
    public int missionID;
    public bool isBeaten;
    [TextArea]
    public string missionBriefing;
}
