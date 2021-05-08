using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    //private Animator anim; //动画控制
    //private Rigidbody2D rb;
    //[SerializeField] private Collider2D coll; 
    public LayerMask ground;
    public Transform leftPoint,rightPoint;
    public float speed,jumpForce;
    private bool faceLeft;
    private float leftx,rightx;


    void Awake() 
    { 
        faceLeft = true; 
    }
    protected override void Start()  //override：重载函数
    {
        base.Start(); //获取父级的start函数并执行
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //movement();
        switchAnim();
    }

    private void movement()
    {
        if(this.transform.position.x-speed < leftx)
        {
            this.transform.localScale = new Vector3(-0.71f,0.67f,1);
            faceLeft = false;
        }
        else if(this.transform.position.x + speed > rightx)
        {
            this.transform.localScale = new Vector3(0.71f,0.67f,1);
            faceLeft = true;
        }
        
        if(faceLeft == true)
        {
            if(coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jump",true);
                rb.velocity = new Vector2(-speed,jumpForce);
            }
        }
        else
        {
            if(coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jump",true);
                rb.velocity = new Vector2(speed,jumpForce);
            }
        }
    }
    private void switchAnim()
    {
        if(anim.GetBool("jump") == true)
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jump",false);
                anim.SetBool("fall",true);
            }
        }
        if(coll.IsTouchingLayers(ground) == true && anim.GetBool("fall") == true)
        {
            anim.SetBool("fall",false);
        }
    }
}
