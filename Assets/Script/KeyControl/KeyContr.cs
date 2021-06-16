using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyContr : MonoBehaviour
{
    public static KeyContr KC;

    public KeyCode JumpKey {get;set;}
    public KeyCode CrouchKey {get;set;}
    public KeyCode WudiKey {get;set;}
    public KeyCode DashKey {get;set;}
    public KeyCode LeftKey {get;set;}
    public KeyCode RightKey {get;set;}
    public KeyCode ShootKey {get;set;}

    public Event keyEvent;
    public KeyCode newKey;

    // Start is called before the first frame update
    void Awake()
    {
        if(KC == null)
        {
            DontDestroyOnLoad(this.gameObject);
            KC = this;
        }
        else if(KC != this)
        {
            Destroy(this.gameObject);
        }
        defaultKey();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void defaultKey()
    {
        JumpKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("JumpKey","UpArrow"));
        CrouchKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("CrouchKey","DownArrow"));
        LeftKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("LeftKey","LeftArrow"));
        RightKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("RightKey","RightArrow"));
        ShootKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("ShootKey","Space"));
        WudiKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("WudiKey","Z"));
        DashKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("DashKey","C"));

        string[] keyList = new string[] {"JumpKey","CrouchKey","LeftKey","RightKey","ShootKey","WudiKey","DashKey"};
        for(int i = 0; i < 7; i ++)
        {
            if(PlayerPrefs.GetString(keyList[i]) == "")
            {
                string keyName = keyList[i];
                if(keyName == "ShootKey")
                {
                    PlayerPrefs.SetString("ShootKey",ShootKey.ToString());
                }
                if(keyName == "CrouchKey")
                {
                    PlayerPrefs.SetString("CrouchKey",CrouchKey.ToString());
                }
                if(keyName == "JumpKey")
                {
                    PlayerPrefs.SetString("JumpKey",JumpKey.ToString());
                }
                if(keyName == "WudiKey")
                {
                    PlayerPrefs.SetString("WudiKey",WudiKey.ToString());
                }
                if(keyName == "DashKey")
                {
                    PlayerPrefs.SetString("DashKey",DashKey.ToString());
                }
                if(keyName == "LeftKey")
                {
                    PlayerPrefs.SetString("LeftKey",LeftKey.ToString());
                }
                if(keyName == "RightKey")
                {
                    PlayerPrefs.SetString("RightKey",RightKey.ToString());
                }   
            }
        }
        //ShootKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),"L");
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        if(keyEvent.isKey == true && keyEvent.keyCode.ToString() != "None")
        {
            newKey = keyEvent.keyCode;
            //Debug.Log(newKey.ToString());
        }
    }


    public string getCurrentKey()
    {
        return newKey.ToString();
    }

    public void GiveNewKey(string keyName)
    {
        if(keyName == "ShootKey")
        {
            ShootKey = newKey;
            PlayerPrefs.SetString("ShootKey",ShootKey.ToString());
        }
        if(keyName == "CrouchKey")
        {
            CrouchKey = newKey;
            PlayerPrefs.SetString("CrouchKey",CrouchKey.ToString());
        }
        if(keyName == "JumpKey")
        {
            JumpKey = newKey;
            PlayerPrefs.SetString("JumpKey",JumpKey.ToString());
        }
        if(keyName == "WudiKey")
        {
            WudiKey = newKey;
            PlayerPrefs.SetString("WudiKey",WudiKey.ToString());
        }
        if(keyName == "DashKey")
        {
            DashKey = newKey;
            PlayerPrefs.SetString("DashKey",DashKey.ToString());
        }
        if(keyName == "LeftKey")
        {
            LeftKey = newKey;
        }
        if(keyName == "RightKey")
        {
            RightKey = newKey;
        }   
    }
}
