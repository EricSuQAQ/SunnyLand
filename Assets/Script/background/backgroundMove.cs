using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMove : MonoBehaviour
{
    Transform bg1,bg2,bg3;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        bg1 = GameObject.Find("/background/back1").transform;
        bg2 = GameObject.Find("/background/back2").transform;
        bg3 = GameObject.Find("/background/back3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = speed * Time.deltaTime;
        bg1.Translate(-dx,0,0);
        bg2.Translate(-dx,0,0);
        bg3.Translate(-dx,0,0);
        if(bg1.position.x <= -24f)
        {
            bg1.Translate(48,0,0);
        }
        
        if(bg2.position.x <= -24f)
        {
            bg2.Translate(48,0,0);
        }        

        if(bg3.position.x <= -24f)
        {
            bg3.Translate(48,0,0);
        }        
    }
}
