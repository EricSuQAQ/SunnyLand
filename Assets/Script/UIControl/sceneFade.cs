using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneFade : MonoBehaviour
{
    // Start is called before the first frame update
    public Image black;
    [SerializeField] private float alpha;
    void Start()
    {
        alpha = 1f;
        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void goFadeOut()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        while(alpha > 0.5)
        {
            alpha -= Time.deltaTime/5;
            black.color = new Color(0,0,0,alpha);
            
        }   
        while(alpha > 0)
        {
            alpha -= Time.deltaTime;
            black.color = new Color(0,0,0,alpha);
            yield return null;     
        }   
        this.gameObject.SetActive(false);
         
    }

    IEnumerator FadeOut()
    {
        float every = Time.deltaTime;
        Time.timeScale = 0f;
        alpha = 0;
        while(alpha < 0.5)
        {
            alpha += every/3;
            black.color = new Color(0,0,0,alpha);
            
        }   
        while(alpha < 1)
        {
            alpha += every/5 ;
            black.color = new Color(0,0,0,alpha);
            yield return null;     
        }   
        Time.timeScale = 1f;
         
    }
}
