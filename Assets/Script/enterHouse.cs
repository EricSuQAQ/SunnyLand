using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enterHouse : MonoBehaviour
{
    public GameObject dialog;
    public Text enterText;
    public Image black;
    public bool permission; 
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        permission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<playerControl>().cherry == 10)
        {
            permission = true;
            enterText.text = "按E键进入下一关";
            enterText.fontSize = 20;
            
        }
        if(Input.GetKeyDown(KeyCode.E) && dialog.activeInHierarchy == true && permission == true) //按下E，同时dialog被激活
        {
            dialog.SetActive(false);
            GameObject music = GameObject.Find("游戏主控");
            music.SendMessage("passAudio");
            Invoke("nextCheckPoint",2.5f);  
            black.gameObject.SetActive(true);
            black.SendMessage("goFadeOut");
        }
    }

    private void nextCheckPoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            dialog.SetActive(false);
        }
    }
}
