using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    // Start is called before the first frame update

    // Start is called before the first frame update
    //private Rigidbody2D rb;
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
        this.transform.Translate(speed * Time.deltaTime,0,0,Space.Self);
        if(coll.IsTouchingLayers(ground) == true)
        {
            Destroy(this.gameObject);
        }
        Vector3 sp = Camera.main.WorldToScreenPoint(this.transform.position);
        if(sp.x > Screen.width || sp.x < 0)
        {
            //Debug.Log("超出屏幕右边界，自动销毁");
            Destroy(this.gameObject);
        }        
    }

    
    public void setSpeed(bool k)
    {
        speed = -speed;
    }

}
