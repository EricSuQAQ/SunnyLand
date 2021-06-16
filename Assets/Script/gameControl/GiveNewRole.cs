using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNewRole : MonoBehaviour
{
    public GameObject fox,cow;
    public GameObject role;
    // Start is called before the first frame update
    void Awake()
    {
        RoleControl.role.currentRole = PlayerPrefs.GetString("currentRole");
        if(RoleControl.role.currentRole == "fox")
        {
            role = Instantiate(fox);
        }
        if(RoleControl.role.currentRole == "cow")
        {
            role = Instantiate(cow);
        }
        role.transform.parent = this.transform;
        role.name = "role";//RoleControl.role.currentRole;
        role.transform.position = this.transform.position;

        GameObject focusPoint = GameObject.Find("镜头焦点");
        focusPoint.SendMessage("setNewFocus");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
