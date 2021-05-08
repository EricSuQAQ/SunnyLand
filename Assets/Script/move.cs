using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb; //如果想在unity界面里看到这个private项，就加上前面中括号的
    private Animator anim; //动画控制
    public LayerMask ground; //获取某个图层（物理碰撞层）
    [SerializeField] private Collider2D coll,headColl; //获取碰撞体

    public float speed = 5.5f;
    public float horizontalMove;

    public float jumpForce = 10;
    public int jumpCount; 
    public bool jumpPreesed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
        jumpCount = 1;
        bool test = GetComponent<Enemy>().isHurt;
        Debug.Log(test);
    }

    void Update()
    {
        if(GetComponent<Enemy>().isHurt == false)
        {
            moveMent(); //为了保障不丢帧，使用fixedupdate对移动进行更新。
        }

        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground) == true)
        {
            jumpPreesed = true;
            jump();
        }
        crouch();
        switchAnim();        
    }

    private void FixedUpdate() 
    {

    }

    public void crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow) && coll.IsTouchingLayers(ground) == true)
        {
            headColl.enabled = false;
            anim.SetBool("crouching",true);
        }
        else
        {
            headColl.enabled = true;
            anim.SetBool("crouching",false);
        }
    }
    public void moveMent()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (horizontalMove*speed, rb.velocity.y);
        anim.SetFloat("running",Mathf.Abs(horizontalMove));

        if(horizontalMove != 0)
        {
            this.transform.localScale = new Vector3(horizontalMove,1,1);
        }

    }

    public void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("jumping",true);
    }

    public void switchAnim()
    {
        if(rb.velocity.y < 0)
        {
            anim.SetBool("falling",true);
            anim.SetBool("jumping",false);
        }
        else if(coll.IsTouchingLayers(ground) == true) //判断其是否碰撞到ground上
        {
            //Debug.Log("已经掉落");
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
        if(GetComponent<Enemy>().isHurt == true)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                Debug.Log("即将修改");
                GetComponent<Enemy>().isHurt = false;
                anim.SetBool("hurting",false);
            }
        }
    }
}