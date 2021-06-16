using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour
{
    public static RoleControl role;
    public string currentRole;
    // Start is called before the first frame update
    void Awake()
    {
        currentRole = PlayerPrefs.GetString("currentRole");
        if(role == null)
        {
            DontDestroyOnLoad(this.gameObject);
            role = this;
        }
        else if(role != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        /*
        currentRole = "cow";
        Debug.Log("niuzi");
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
