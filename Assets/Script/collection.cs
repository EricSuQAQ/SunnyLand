using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{
    // Start is called before the first frame update
    public int cherry;
    public Text cherryCount;
    void Start()
    {
        cherry = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "collection")
        {
            Destroy(other.gameObject);
            cherry += 1;
            cherryCount.text = "cherry：" + cherry;
        }
    }
}
