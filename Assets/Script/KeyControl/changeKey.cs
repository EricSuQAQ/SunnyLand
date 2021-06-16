using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeKey : MonoBehaviour
{
    public GameObject keyButton,keyChange;
    public Text keyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openKeyChange()
    {
        keyChange.SetActive(true);
        //this.gameObject.SetActive(false);

        GameObject father = GameObject.Find("Canvas/修改panel");
        for (int i = 0; i < father.transform.childCount; i++)
        {
            GameObject child = father.transform.GetChild(i).gameObject;
            if(child.name != "警告" && child.name != this.name)
            {
                child.SetActive(false);
            }  
        }
    }
}
