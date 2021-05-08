using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform camera;
    public float moveRate;
    private float startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(startPoint+camera.position.x * moveRate, this.transform.position.y);
    }

    
}
