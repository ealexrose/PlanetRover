using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score;
    public int digits;
    public string display;
    public Text displayText;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            score += Time.deltaTime;
            display = score.ToString("000.00");
            displayText.text = display + "s";
        }
    }

    public void AddZeroes()
    {
        if (display.Length <= digits)
        {
            for (int i = 0; i <= digits - display.Length; i++)
            {
                display.Insert(0, "0");
            }
        }
    }
}
