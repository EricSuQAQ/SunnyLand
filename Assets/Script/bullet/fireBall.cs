using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D coll;
    [SerializeField] private AudioSource bulletAudio;
    public float speed = 5f;
    public LayerMask ground;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        bulletAudio = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(coll.IsTouchingLayers(ground) == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "deadline")
        {
            Destroy(this.gameObject);
        }
    }
}
