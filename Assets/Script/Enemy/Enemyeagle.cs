using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyeagle : Enemy
{
    // Start is called before the first frame update
    //private Animator anim; //动画控制
    //private Rigidbody2D rb;
    //[SerializeField] private Collider2D coll; 
    public LayerMask ground;
    public Transform topPoint,bottomPoint;
    public float speed;
    private bool directionUp;
    private float topy,bottomy;
    private float timer;

    private int randomBall;

    [SerializeField] private GameObject fireBall; 

    void Awake()
    {
        directionUp = true;
        timer = 0;
        randomBall = 2;
        //speed = 3f;
    }

    protected override void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        transform.DetachChildren();  
        topy = topPoint.position.y;
        bottomy = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > randomBall)
        {
            eagleFire();
            randomBall = Random.Range(2, 5); //鹰在2-5秒后吐火球
            timer = 0;
        }
        moveMent();
    }

    private void eagleFire()
    {
        GameObject tempbullet = Instantiate(fireBall);
        tempbullet.transform.position = this.transform.position - this.transform.up / 2;
    }

    private void moveMent()
    {
        if(this.transform.position.y + speed > topy)
        {
            if(directionUp == true)
            {
                eagleFire();
            }
            directionUp = false;
        }
        else if(this.transform.position.y - speed < bottomy)
        {
            directionUp = true;
        }
        else if(coll.IsTouchingLayers(ground))
        {
            directionUp = !directionUp;
        }
        
        if(directionUp == true)
        {
            rb.velocity = new Vector2(rb.velocity.x,speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,-speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "enemy")
        {
            directionUp = !directionUp;
        }
    }
}
