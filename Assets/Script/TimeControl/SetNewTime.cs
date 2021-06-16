using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("currentTimer",0);
        PlayerPrefs.SetInt("currentSec",0);
        PlayerPrefs.SetInt("currentMin",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
