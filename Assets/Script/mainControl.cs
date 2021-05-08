using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] musicGroup;
    //[SerializeField] private AudioSource gameOver;
    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void deadAudio()
    {
        musicGroup[0].Stop();
        musicGroup[1].PlayOneShot(musicGroup[1].GetComponent<AudioSource>().clip);
    }

    void collectAudio()
    {
        musicGroup[2].PlayOneShot(musicGroup[2].GetComponent<AudioSource>().clip);
    }

    void passAudio()
    {
        musicGroup[0].Stop();
        musicGroup[3].PlayOneShot(musicGroup[3].GetComponent<AudioSource>().clip);
    }
}
