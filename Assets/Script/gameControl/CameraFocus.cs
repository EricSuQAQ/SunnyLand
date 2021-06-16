using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = child.transform.position;
    }

    public void setNewFocus()
    {
        GameObject father = GameObject.Find("player");
        child = father.transform.Find("role").gameObject;
        this.transform.position = child.transform.position;
    } 
}
