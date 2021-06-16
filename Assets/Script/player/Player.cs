using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物理元素")]
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    [SerializeField] protected Collider2D bodyColl,headColl; 
    protected Animator anim;
    [SerializeField] protected AudioSource jumpAudio;

    [Header("自身属性")]
    protected float speed = 5.5f;
    protected float horizontalMove;
    protected float jumpForce = 10;
    protected bool faceRight;
    protected int hp;

    [Header("图层判定")]
    protected Transform head,feet;
    [SerializeField]protected LayerMask ground;

    [Header("状态判定")]
    protected bool isGround,isJump,isHurt,isLive;

    [Header("按键判定")]
    protected bool jumpPreesed;
    protected int jumpCount;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //bodyColl = GetComponent<CapsuleCollider2D>();
        //headColl = GetComponent<CircleCollider2D>();
        jumpAudio = GetComponent<AudioSource>();

        head = GameObject.Find("head").transform;
        feet = GameObject.Find("feet").transform;

        jumpCount = 0;
        isGround = true;
        hp = 5;

        faceRight = (this.transform.localScale.x == 1);
        isLive = true;
        
    }

    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Input.GetKeyDown(KeyContr.KC.JumpKey) && jumpCount > 0) //判定是否起跳
        {
            jumpPreesed = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject pauseMenue = canvas.GetComponent<menu>().pauseMenue;
            if(pauseMenue.activeInHierarchy == false)
            {
                canvas.SendMessage("pauseDown");  
            }
            else
            {
                canvas.SendMessage("resumuDown");
            }  
        }
    }

    protected virtual void FixedUpdate() 
    {
        isGround = Physics2D.OverlapCircle(feet.position,0.2f,ground); //检测脚底距离地面的距离

        if(isLive == true) //不死亡条件下：动画更迭
        {
            switchAnim();
        }
        else //死亡条件下：只播放受伤动画
        {
            anim.SetBool("hurting",true);
        } 

        if(isHurt == false && isLive == true) //不受伤、不死亡条件下:可进行控制
        {
            moveMent();
            jump();
        }
    }

    public int getHp() //获取当前血量
    {
        return hp;
    }

    public void SetHp(int k) //给当前单位扣血
    {
        hp -= k;
    }

    public void switchAnim()
    {
        if(isHurt == true) //受伤情况下：重力判定为2
        {
            rb.gravityScale = 2;
        }
        else if(isGround == true) //站立动画
        {
            anim.SetBool("idle",true);
            anim.SetBool("falling",false);
            anim.SetBool("jumping",false);
            rb.gravityScale = 2;
        }
        else if(isGround == false && rb.velocity.y > 0) //跳跃动画
        {
            anim.SetBool("jumping",true);
            anim.SetBool("falling",false);
            rb.gravityScale = 2;
        }
        else if(isGround == false && rb.velocity.y < 0 ) //落地动画，同时重力改变为4
        {
            anim.SetBool("falling",true);
            anim.SetBool("jumping",false);
            rb.gravityScale = 4;
        }
    }

    private void moveMent()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("running",Mathf.Abs(horizontalMove));
        rb.velocity = new Vector2 (horizontalMove*speed, rb.velocity.y);
        if(horizontalMove != 0)
        {
            this.transform.localScale = new Vector3(horizontalMove,1,1);
            if(horizontalMove == 1)
            {
                faceRight = true;
            }
            else if(horizontalMove == -1)
            {
                faceRight = false;
            }
        }
        crouch();
    }

    private void jump()
    {
        if(isGround == true) //位于地面可以执行跳跃
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPreesed == true && isGround == true) //执行一段跳
        {
            isJump = true;
            jumpAudio.PlayOneShot(jumpAudio.GetComponent<AudioSource>().clip);   
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount -= 1;
            jumpPreesed = false;
        }
        else if(jumpPreesed == true && jumpCount > 0 && isJump == true) //执行二段跳
        {
            jumpAudio.PlayOneShot(jumpAudio.GetComponent<AudioSource>().clip);   
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount -= 1;
            jumpPreesed = false;
        }
    }

    public void crouch()
    {
        if (Input.GetKey(KeyContr.KC.CrouchKey) && bodyColl.IsTouchingLayers(ground) == true) //站在地面上按下下蹲键
        {
            headColl.enabled = false;
            anim.SetBool("crouching",true);
            anim.SetBool("idle",false);
        }
        else if(!Input.GetKey(KeyContr.KC.CrouchKey) && Physics2D.OverlapCircle(head.position,0.2f,ground) == false) //松开下蹲键，同时头距离地板大于0.2f
        {
            headColl.enabled = true;
            anim.SetBool("crouching",false);
            anim.SetBool("idle",true);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "collection")
        {
            other.GetComponent<Animator>().Play("cherryGot");
        }

        if(other.tag == "deadline" && isLive == true)
        {
            GameObject music = GameObject.Find("游戏主控");
            music.SendMessage("deadAudio");
            Invoke("reStart",2);           
        }   
    }

    protected virtual void hurt(int loseHp, GameObject otherGameObejct) //受伤状态添加
    {
        isHurt = true;
        Invoke("cancelHurt",0.5f);
        hp -= loseHp;

        if(hp <= 0)
        {
            isLive = false;
            GameObject music = GameObject.Find("游戏主控");
            music.SendMessage("deadAudio");
            Invoke("reStart",2);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            bodyColl.isTrigger = true;
            headColl.isTrigger = true;      
        }
        anim.SetBool("hurting",true);
        anim.SetBool("jumping",false);
        float k = this.transform.position.x - otherGameObejct.transform.position.x;
        if(k < 0)
        {
            this.GetComponent<Rigidbody2D>().velocity =  new Vector2(-5, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(k > 0)
        {
            this.GetComponent<Rigidbody2D>().velocity =  new Vector2(5, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void cancelHurt()
    {
        anim.SetBool("hurting",false);
        isHurt = false;
    }

    protected virtual void treadEnemy(GameObject otherGameObejct) //踩踏并秒杀怪物
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 5);
        if(jumpCount < 2)
        {
            jumpCount = 1;
        }  
        anim.SetBool("jumping",true);
        if(otherGameObejct.tag == "enemy")
        {
            Enemy thisEnemy = otherGameObejct.GetComponent<Enemy>(); 
            thisEnemy.JumpOn();
        }
        if(otherGameObejct.tag == "enemyBullet")
        {
            //enemyBullet thisEnemy = otherGameObejct.GetComponent<enemyBullet>(); 
            Destroy(otherGameObejct);
        }
    }

    public void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
