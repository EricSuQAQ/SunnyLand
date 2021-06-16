using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cherryAudio()
    {
        GameObject music = GameObject.Find("游戏主控");
        music.SendMessage("collectAudio");
    }

    public void cherryGot()
    {
        FindObjectOfType<ScoreCauculator>().cherryAccumulate();
        Destroy(this.gameObject);
    }
}
