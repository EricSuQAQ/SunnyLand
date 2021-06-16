using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickMenu : MonoBehaviour
{
    public GameObject cursor; 
    public GameObject currentRole;
    public GameObject pickCheckpoint;
    // Start is called before the first frame update

    void Awake()
    {
        PlayerPrefs.SetString("currentRole","fox");
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject fuck = GetRoleByMouse();
            //Debug.Log(fuck.name);
            
            float newX = fuck.transform.position.x;
            float newY = fuck.transform.position.y;
            cursor.transform.position = Camera.main.WorldToScreenPoint(new Vector3(newX, newY+1.5f, 0));
        }
    }

    public GameObject GetRoleByMouse()
    {
        GameObject father = GameObject.Find("Player");
        for (int i = 0; i < father.transform.childCount; i++)
        {
            GameObject child = father.transform.GetChild(i).gameObject;
            if(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition),child.transform.position) < 1.5)
            {
                currentRole = child;
                showTheChara(child.name);
            }  
        }
        return currentRole;
    }

    public void goToPick()
    {
        //Debug.Log("fuck you! " + currentRole.name);
        PlayerPrefs.SetString("currentRole",currentRole.name);
        Debug.Log(currentRole.name);
        this.gameObject.SetActive(false);
        pickCheckpoint.SetActive(true);
    }

    public void showTheChara(string name)
    {
        GameObject father = GameObject.Find("选角Canvas/介绍");
        for (int i = 0; i < father.transform.childCount; i++)
        {
            GameObject child = father.transform.GetChild(i).gameObject;
            if(child.name == name)
            {
                child.SetActive(true);
            }  
            else
            {
                child.SetActive(false);
            }
        }
        //Debug.Log(name);
    }
}
