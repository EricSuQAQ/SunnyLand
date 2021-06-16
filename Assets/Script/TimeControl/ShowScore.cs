using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text timeText;
    private float timer;
    private int second,minute;
    public Text gameMode;
    // Start is called before the first frame update
    void Start()
    {
        timer = PlayerPrefs.GetFloat("currentTimer");
        second = PlayerPrefs.GetInt("currentSec");
        minute = PlayerPrefs.GetInt("currentMin");
        transTime();
        transMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void transMode()
    {
        string mode = PlayerPrefs.GetString("mode");
        if(mode == "all")
        {
            gameMode.text = "游戏模式：闯关模式-全关卡";
        }
        else
        {
            gameMode.text = "游戏模式：单关挑战-第" + mode + "关";
        }
    }

    void transTime()
    {
        string newMilSec = ((int)(1000 * timer)).ToString();
        while(newMilSec.Length < 3)
        {
            newMilSec = "0" + newMilSec;
        }

        string secStr = second.ToString();
        if(second < 10)
        {
            secStr = "0" + secStr;
        }
        string minStr = minute.ToString();
        if(minute < 10)
        {
            minStr = "0" + minStr;
        }

        timeText.text = minStr + ":" + secStr + ":" + newMilSec;
    }
}
