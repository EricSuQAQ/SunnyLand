using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpCaculator : MonoBehaviour
{
    public Image[] HPGroup;
    private int hpCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hpCount = GameObject.Find("player").GetComponent<playerControl>().getHp();
        //HPGroup[hpCount].color.a = 0;
        if(hpCount < 3)
        {
            HPGroup[hpCount].color =new Color(0,0,0,0);
        }
        
    }
}
