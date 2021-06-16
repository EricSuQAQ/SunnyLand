using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] musicGroup;

    public Text timeText;
    private float timer;
    private int second,minute;

    public bool fuck = true;
    //[SerializeField] private AudioSource gameOver;

    //Math.Round(0.55555,2)
    void Start()
    {
        //audio = GetComponent<AudioSource>();
        if(true == false)
        {
            timer = 0;
            second = 0;
            minute = 0;
        }
        else
        {
            timer = PlayerPrefs.GetFloat("currentTimer");
            second = PlayerPrefs.GetInt("currentSec");
            minute = PlayerPrefs.GetInt("currentMin");
        }

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        refleshTime();
    }

    void refleshTime()
    {
        if(timer > 1)
        {
            second += 1;
            timer -= 1;
        }
        if(second >= 60)
        {
            minute += 1;
            second -= 60;
        }
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

    public void CoverCurrentTime()
    {
        PlayerPrefs.SetFloat("currentTimer",timer);
        PlayerPrefs.SetInt("currentSec",second);
        PlayerPrefs.SetInt("currentMin",minute);
    }


    void deadAudio()
    {
        CoverCurrentTime();
        musicGroup[0].Stop();
        //musicGroup[1].GetComponent<AudioSource>().clip.value = 0.3f;
        musicGroup[1].volume = 0.01f;
        musicGroup[1].PlayOneShot(musicGroup[1].GetComponent<AudioSource>().clip);
    }

    void collectAudio()
    {
        //musicGroup[2].GetComponent<AudioSource>().clip.value = 0.5f;
        musicGroup[2].volume = 0.5f;
        musicGroup[2].PlayOneShot(musicGroup[2].GetComponent<AudioSource>().clip);
    }

    void passAudio()
    {
        musicGroup[0].Stop();
        //musicGroup[3].GetComponent<AudioSource>().clip.value = 0.3f;
        musicGroup[3].volume = 0.3f;
        musicGroup[3].PlayOneShot(musicGroup[3].GetComponent<AudioSource>().clip);
    }

    void punishTime()
    {
        second += 3;
    }
}
