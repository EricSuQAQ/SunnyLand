using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pickCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chuangGuan()
    {
        PlayerPrefs.SetString("mode","all");
        SceneManager.LoadScene(1);
    }

    public void level_1()
    {
        PlayerPrefs.SetString("mode","1");
        SceneManager.LoadScene(1);
    }

    public void level_2()
    {
        PlayerPrefs.SetString("mode","2");
        SceneManager.LoadScene(2);
    }
}
