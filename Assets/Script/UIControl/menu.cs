using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menu : MonoBehaviour
{
    public GameObject pauseMenue,KeyMenue,pickMenu,checkCanvas;
    public GameObject startButton,quitButton,ruleButton,rulePanel,keyButton,title;
    public AudioSource bgm,deadVoice,passVoice,yoo,attack,collection;
    public Slider slider,voiceSlider;
    // Start is called before the first frame update
    void Start()
    {
        bgm.volume = 0.05f;
        slider.value = 0.05f;
        voiceSlider.value = 0.5f;
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToPreface()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void reStartCurrentStage()
    {
        string mode = PlayerPrefs.GetString("mode");
        if(mode != "all")
        {
            PlayerPrefs.SetFloat("currentTimer",0);
            PlayerPrefs.SetInt("currentSec",0);
            PlayerPrefs.SetInt("currentMin",0);
        }
        else
        {
            GameObject mainControl = GameObject.Find("游戏主控");
            mainControl.SendMessage("CoverCurrentTime");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
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
        deadVoice = GameObject.Find("audioGroup/GmaeOver").GetComponent<AudioSource>();
        passVoice = GameObject.Find("audioGroup/通过音效").GetComponent<AudioSource>();
        //bgm.Play();
        bgm.volume = slider.value;
        deadVoice.volume = slider.value;
        passVoice.volume = slider.value;
    }

    public void VoiceChange()
    {
        yoo = GameObject.Find("audioGroup/yoo").GetComponent<AudioSource>();
        attack = GameObject.Find("audioGroup/普通攻击").GetComponent<AudioSource>();
        collection = GameObject.Find("audioGroup/collect").GetComponent<AudioSource>();

        yoo.volume = voiceSlider.value;
        attack.volume = voiceSlider.value;
        collection.volume = voiceSlider.value;
    }

    public void openRule()
    {
        ruleButton.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        rulePanel.SetActive(true);
        keyButton.SetActive(false);
        KeyMenue.SetActive(false);
        title.SetActive(false);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(/*SceneManager.GetActiveScene().buildIndex + */1);
        ruleButton.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        keyButton.SetActive(false);
        title.SetActive(false);
        pickMenu.SetActive(true);
    }
    public void openKey()
    {
        ruleButton.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        rulePanel.SetActive(false);
        keyButton.SetActive(false);
        KeyMenue.SetActive(true);
        title.SetActive(false);

        GameObject father = GameObject.Find("Canvas/修改panel");
        for (int i = 0; i < father.transform.childCount; i++)
        {
            GameObject child = father.transform.GetChild(i).gameObject;
            if(child.name != "警告" && child.name != "退出")
            {
                GameObject grandChild = GameObject.Find("Canvas/修改panel/" + child.name + "/Text");
                Debug.Log(child.name);
                grandChild.GetComponent<Text>().text = PlayerPrefs.GetString(child.name);
            }  
        }
    }

    public void quitRule()
    {
        ruleButton.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        keyButton.SetActive(true);
        rulePanel.SetActive(false);
        KeyMenue.SetActive(false);
        title.SetActive(true);
    }

    public void quitKeyChange()
    {
        ruleButton.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        keyButton.SetActive(true);
        rulePanel.SetActive(false);
        KeyMenue.SetActive(false);
        title.SetActive(true);
    }

    public void quitPickChange()
    {
        ruleButton.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
        keyButton.SetActive(true);
        pickMenu.SetActive(false);
        title.SetActive(true);
    }

    public void quitCheckChange()
    {
        pickMenu.SetActive(true);
        checkCanvas.SetActive(false);
    }
    

}
