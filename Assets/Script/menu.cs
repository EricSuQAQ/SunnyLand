using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menu : MonoBehaviour
{
    public GameObject pauseMenue;
    public GameObject startButton,quitButton,ruleButton,rulePanel,title;
    public AudioSource bgm;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(/*SceneManager.GetActiveScene().buildIndex + */1);
    }

    public void backToPreface()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void pauseDown()
    {
        pauseMenue.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumuDown()
    {
        pauseMenue.SetActive(false);
        Time.timeScale = 1f;
    }

    public void volumeChange()
    {
        bgm = GameObject.Find("audioGroup/bgm").GetComponent<AudioSource>();
        //bgm.Play();
        bgm.volume = slider.value;

    }

    public void openRule()
    {
        ruleButton.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        rulePanel.SetActive(true);
        title.SetActive(false);
    }

    public void quitRule()
    {
        ruleButton.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        rulePanel.SetActive(false);
        title.SetActive(true);
    }
}
