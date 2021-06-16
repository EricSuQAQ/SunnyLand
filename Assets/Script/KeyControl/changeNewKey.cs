using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeNewKey : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ParentButtonText;
    public string keyString;
    public string keyName;

    void Start()
    {
        //PlayerPrefs.SetString("ShootKey","Space");
        //Debug.Log(PlayerPrefs.GetString("ShootKey"));
        keyString = PlayerPrefs.GetString(keyName);
        ParentButtonText.text = keyString;
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyContr.KC.getCurrentKey() != "None")
        {
            keyString = KeyContr.KC.getCurrentKey();
            ParentButtonText.text = keyString; 
        }
        
    }

    public void confirmNewKey()
    {
        KeyContr.KC.GiveNewKey(keyName); 
        ParentButtonText.text = keyString;

        string[] keyList = new string[] {"JumpKey","CrouchKey","LeftKey","RightKey","ShootKey","WudiKey","DashKey"};
        int num = 0;
        for(int i = 0; i < 7; i ++)
        {
            Debug.Log(keyList[i] + " : " +PlayerPrefs.GetString(keyList[i]));
            if(PlayerPrefs.GetString(keyList[i]) == keyString)
            {
                num += 1;
            }
        }

        if(num == 1)
        {
            this.gameObject.SetActive(false);
            GameObject father = GameObject.Find("Canvas/修改panel");
            for (int i = 0; i < father.transform.childCount; i++)
            {
                GameObject child = father.transform.GetChild(i).gameObject;
                if(child.name != "警告" && child.name != this.name)
                {
                    child.SetActive(true);
                }  
            }
        }
        else
        {
            Debug.Log("警告：数据发生重复");
            GameObject slogan = GameObject.Find("Canvas/修改panel/警告");
            slogan.GetComponent<Text>().color = new Color(1,0,0);
            Invoke("backToBlack",0.1f);
        }
    }

    public void backToBlack()
    {
        GameObject slogan = GameObject.Find("Canvas/修改panel/警告");
        slogan.GetComponent<Text>().color = new Color(0,0,0);        
    }
}
