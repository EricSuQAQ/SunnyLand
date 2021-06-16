using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private Collider2D coll,headColl; 
    private Animator anim;
    [SerializeField] private AudioSource jumpAudio,yoo,bulletAudio;
    public GameObject whiteBullet,blackHoleBullet;

    public float speed = 5.5f;
    public float horizontalMove;
    public float jumpForce = 10;
    public float attackInterval,backSteepInterval,dashInterval;

    public Transform head,feet;
    public LayerMask ground;

    public bool isGround,isJump,isHurt,isLive,isWudi,isDash;

    public bool jumpPreesed,shootPreesed,WudiPressed,DashPressed;

    public int jumpCount;
    public int cherry;

    public Text cherryCount;
    public bool faceRight;
    public bool invincible;
    private int hp = 5;

    private GameObject dashShadow;
    private bool dashReady;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //coll = GetComponent<CircleCollider2D>();
        jumpCount = 0;
        isGround = true;
        cherry = 0;
        attackInterval = 1;
        backSteepInterval = 3;
        dashInterval = 10;
        faceRight = true;
        invincible = false;
        isLive = true;
        WudiPressed = false;
        DashPressed = false;

        dashShadow = null;
        dashReady = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval += Time.deltaTime;
        backSteepInterval += Time.deltaTime;
        dashInterval += Time.deltaTime;
        //if(Input.GetKeyDown(KeyCode.UpArrzow) && jumpCount > 0)
        if(Input.GetKeyDown(KeyContr.KC.JumpKey) && jumpCount > 0)
        {
            jumpPreesed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.ShootKey) && attackInterval >= 0.5)
        {
            shootPreesed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.WudiKey) && backSteepInterval >= 3)
        {
            WudiPressed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.DashKey) && dashInterval >= 10)
        {
            DashPressed = true;
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
/*
        if(coll.IsTouchingLayers(ground))
        {
            Debug.Log("接触到地板");
        }
*/

        if(dashShadow == null && dashReady == true)
        {
            dashReady = false;
            dashInterval = 0;   
            GameObject CDMask = GameObject.Find("Canvas/猎手本能");
            CDMask.SendMessage("RefleshCD");         
        }
    }

    public int getHp()
    {
        return hp;
    }

    public void SetHp(int k)
    {
        hp -= k;
    }

    private void backSteep()
    {
        if(WudiPressed == true)
        {
            WudiPressed = false;
            isWudi = true;
            headColl.enabled = false;
            rb.velocity = new Vector2 (-40f * this.transform.localScale.x, 0f);
            sr.color = new Color(1,0,0);
            GameObject CDMask = GameObject.Find("Canvas/无敌中亚");
            CDMask.SendMessage("RefleshCD");
            Invoke("backToVicible",0.3f);
        }
        //StartCoroutine("houche");
    }

    private void dash()
    {
        if(DashPressed == true)
        {
            DashPressed = false;
            if(dashReady == false) //执行第一段dash
            {
                firstDash();
            }
            else
            {
                secondDash();
            } 
            //sr.color = new Color(0,1,1);
        }
    }

    private void firstDash()
    {
        dashShadow = Instantiate(blackHoleBullet);
        if(faceRight == false)
        {
            dashShadow.SendMessage("setSpeed",-15f);
            dashShadow.transform.position = this.transform.position - 1 * this.transform.right /*- this.transform.up / 2*/;
        }
        else
        {
            dashShadow.SendMessage("setSpeed",15f);
            dashShadow.transform.position = this.transform.position + 1 * this.transform.right /*- this.transform.up / 2*/;
        }
        dashReady = true;
    }

    private void secondDash()
    {
        dashReady = false;
        dashInterval = 0;
        GameObject CDMask = GameObject.Find("Canvas/猎手本能");
        CDMask.SendMessage("RefleshCD");
        if(dashShadow != null)
        {
            this.transform.position = dashShadow.transform.position;  
            Destroy(dashShadow);       
        }
    }

    private void backToVicible()
    {
        isWudi = false;
        backSteepInterval = 0;
        sr.color = new Color(1,1,1);
        if(Physics2D.OverlapCircle(head.position,0.2f,ground) == true)
        {
            anim.SetBool("crouching",true);
            anim.SetBool("idle",false);
        }
        else
        {
            headColl.enabled = true;
        }

    }
    

    private void FixedUpdate() 
    {
        isGround = Physics2D.OverlapCircle(feet.position,0.2f,ground); //检测脚底距离地面的距离
        //isGround = coll.IsTouchingLayers(ground); //这个也可以但是容易碰头
        if(isHurt == false && isLive == true)
        {
            moveMent();
            jump();
            shoot();
            backSteep();
            dash();
        }
        else
        {
            jumpPreesed = false;
            WudiPressed = false;
        }

        if(isLive == true)
        {
            switchAnim();
        }
        else
        {
            anim.SetBool("hurting",true);
        }

        
    }

    public void switchAnim()
    {
        if(isHurt == true)
        {
            rb.gravityScale = 2;
            /*
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                invincible = false;
                isHurt = false;
                anim.SetBool("hurting",false);
            }
            */
        }
        else if(isGround == true && isWudi == false)
        {
            anim.SetBool("falling",false);
            anim.SetBool("jumping",false);
            anim.SetBool("idle",true);
            rb.gravityScale = 2;
        }
        else if(isGround == false && rb.velocity.y > 0)
        {
            anim.SetBool("falling",false);
            anim.SetBool("jumping",true);
            rb.gravityScale = 2;
        }
        else if(isGround == false && rb.velocity.y < 0 )
        {
            anim.SetBool("jumping",false);
            anim.SetBool("falling",true);
            rb.gravityScale = 4;
        }
    }

    private void shoot()
    {
        if(shootPreesed == true)
        {
            GameObject tempbullet = Instantiate(whiteBullet);
            if(faceRight == false)
            {
                tempbullet.SendMessage("setSpeed",-10f);
                tempbullet.transform.position = this.transform.position - 1 * this.transform.right - this.transform.up / 2;
            }
            else
            {
                tempbullet.transform.position = this.transform.position + 1 * this.transform.right - this.transform.up / 2;
            }

            bulletAudio.PlayOneShot(bulletAudio.GetComponent<AudioSource>().clip); 
            shootPreesed = false;
            attackInterval = 0;
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
            //Debug.Log("一段跳");
            jumpAudio.PlayOneShot(jumpAudio.GetComponent<AudioSource>().clip);   
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount -= 1;
            jumpPreesed = false;
        }
        else if(jumpPreesed == true && jumpCount > 0 && isJump == true) //执行二段跳
        {
            //Debug.Log("二段跳");
            jumpAudio.PlayOneShot(jumpAudio.GetComponent<AudioSource>().clip);   
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount -= 1;
            jumpPreesed = false;
        }
    }

    public void crouch()
    {
        if(Physics2D.OverlapCircle(head.position,0.2f,ground) == false) //头距离地图元素的距离大于0.2f
        {
            //if (Input.GetKey(KeyCode.DownArrow) && coll.IsTouchingLayers(ground) == true)
            if (Input.GetKey(KeyContr.KC.CrouchKey) && coll.IsTouchingLayers(ground) == true)
            //if (Input.GetKey(KeyCode.DownArrow) && coll.IsTouchingLayers(ground) == true)
            {
                headColl.enabled = false;
                anim.SetBool("crouching",true);
                anim.SetBool("idle",false);
            }
            //else if(!Input.GetKey(KeyCode.DownArrow))Crouch
            else if(!Input.GetKey(KeyContr.KC.CrouchKey))
            {
                headColl.enabled = true;
                anim.SetBool("crouching",false);
                anim.SetBool("idle",true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "collection")
        {
            //other.GetComponent<Collider2D>().enabled = false;
            //Destroy(other.gameObject);
            other.GetComponent<Animator>().Play("cherryGot");
            /*
            GameObject music = GameObject.Find("游戏主控");
            music.SendMessage("collectAudio");
            */
        }

        if(other.tag == "deadline" && isLive == true)
        {
            //Debug.Log("你死了！");
            GameObject music = GameObject.Find("游戏主控");
            music.SendMessage("deadAudio");
            //music.SendMessage("CoverCurrentTime");
            Invoke("reStart",2);           
        }

        if(other.tag == "enemyBullet")
        {
            //直接跳跃秒杀敌方单位
            if(anim.GetBool("falling") == true && feet.position.y > other.gameObject.transform.position.y)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 5);
                if(jumpCount < 2)
                {
                    jumpCount = 1;
                }  
                anim.SetBool("jumping",true);
            }
            //非无敌状态下扣血受伤
            else if(isWudi == false)
            {    
                isHurt = true;
                if(invincible == false) //非受伤无敌状态添加受伤状态
                {
                    Debug.Log("受伤状态添加");
                    Invoke("cancelHurt",0.5f); //一秒后恢复正常状态
                    hp -= 1;
                    yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                    if(hp <= 0)
                    {
                        GameObject music = GameObject.Find("游戏主控");
                        music.SendMessage("deadAudio");
                        //music.SendMessage("CoverCurrentTime");
                        Invoke("reStart",2);
                        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                        this.GetComponent<CircleCollider2D>().isTrigger = true;
                        this.GetComponent<CapsuleCollider2D>().isTrigger = true;
                        isLive = false;
                        //this.GetComponent<Rigidbody2D>().enabled = false;
                    }
                }
                invincible = true;
                anim.SetBool("hurting",true);
                anim.SetBool("jumping",false);
                float k = this.transform.position.x - other.gameObject.transform.position.x;
                //Destroy(other.gameObject);
                if(k < 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity =  new Vector2(-5, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.GetComponent<Rigidbody2D>().velocity =  new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 10);
                }
                else if(k > 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity =  new Vector2(5, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.GetComponent<Rigidbody2D>().velocity =  new Vector2(this.GetComponent<Rigidbody2D>().velocity.x,10);
                }
            } 
            Destroy(other.gameObject); 
        }         
    }

    public void cancelHurt()
    {
        invincible = false;
        isHurt = false;
        anim.SetBool("hurting",false);
        Debug.Log("受伤状态结束");
    }

    public void cherryCountAdd()
    {
        cherry += 1;
        cherryCount.text = "cherry：" + cherry;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "enemy")
        {
            if(anim.GetBool("falling") == true && feet.position.y > other.gameObject.transform.position.y)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 5);
                if(jumpCount < 2)
                {
                    jumpCount = 1;
                }  
                anim.SetBool("jumping",true);
                Enemy thisEnemy = other.gameObject.GetComponent<Enemy>(); 
                //Destroy(other.gameObject);
                thisEnemy.JumpOn();
            }
            else if(isWudi == false)
            {    
                isHurt = true;
                if(invincible == false)
                {
                    Debug.Log("受伤状态添加");
                    Invoke("cancelHurt",0.5f);
                    hp -= 1;
                    yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                    Debug.Log("当前HP："+ hp);
                    if(hp <= 0)
                    {
                        GameObject music = GameObject.Find("游戏主控");
                        music.SendMessage("deadAudio");
                        Invoke("reStart",2);
                        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                        this.GetComponent<CircleCollider2D>().isTrigger = true;
                        this.GetComponent<CapsuleCollider2D>().isTrigger = true;
                        isLive = false;
                        //this.GetComponent<Rigidbody2D>().enabled = false;
                    }
                }
                invincible = true;
                anim.SetBool("hurting",true);
                anim.SetBool("jumping",false);
                float k = this.transform.position.x - other.gameObject.transform.position.x;
                if(k < 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity =  new Vector2(-5, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.GetComponent<Rigidbody2D>().velocity =  new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 10);
                }
                else if(k > 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity =  new Vector2(5, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.GetComponent<Rigidbody2D>().velocity =  new Vector2(this.GetComponent<Rigidbody2D>().velocity.x,10);
                }
            }
        }
        
    }

    public void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
