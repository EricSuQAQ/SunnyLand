using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skillControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Image CDMask,cover;
    private float timer;
    private Text showText;
    [SerializeField] private float CDtime;
    [SerializeField] private char skill;
    
    void Start()
    {
        CDMask = this.transform.Find("CDMask").GetComponent<Image>();
        cover = this.transform.Find("Cover").GetComponent<Image>();
        showText = this.transform.Find("Button").GetComponent<Text>();
        timer = CDtime;
        if(skill == 'z')
        {
            showText.text = KeyContr.KC.WudiKey.ToString();
        }
        else if(skill == 'c')
        {
            showText.text = KeyContr.KC.DashKey.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer <= CDtime)
        {
            cover.enabled = true;
            CDMask.fillAmount = 1 - timer/CDtime;
            //zoom = false;
        }
        else
        {
            cover.enabled = false;
            CDMask.fillAmount = 0;
        }
    }

    public void RefleshCD()
    {
        timer = 0;
    }

    

}
