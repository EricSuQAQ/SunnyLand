using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private Collider2D coll,headColl; 
    private Animator anim;
    [SerializeField] private AudioSource jumpAudio,yoo,bulletAudio;
    public GameObject whiteBullet;

    public float speed = 5.5f;
    public float horizontalMove;
    public float jumpForce = 10;
    public float attackInterval;

    public Transform head,feet;
    public LayerMask ground;

    public bool isGround,isJump,isHurt,isLive;

    public bool jumpPreesed,shootPreesed;
    public int jumpCount;
    public int cherry;

    public Text cherryCount;
    public bool faceRight;
    public bool invincible;
    private int hp = 3;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coll = GetComponent<CircleCollider2D>();
        jumpCount = 0;
        isGround = true;
        cherry = 0;
        attackInterval = 1;
        faceRight = true;
        invincible = false;
        isLive = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            jumpPreesed = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && attackInterval >= 0.5)
        {
            shootPreesed = true;
        }
    }

    public int getHp()
    {
        return hp;
    }

    private void FixedUpdate() 
    {
        isGround = Physics2D.OverlapCircle(feet.position,0.2f,ground); //检测脚底距离地面的距离
        //isGround = coll.IsTouchingLayers(ground); //这个也可以但是容易碰头
        if(isHurt == false)
        {
            moveMent();
            jump();
            shoot();
        }
        if(isLive == true)
        {
            switchAnim();
        }
        
    }

    public void switchAnim()
    {
        if(isHurt == true)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                invincible = false;
                isHurt = false;
                anim.SetBool("hurting",false);
            }
        }
        else if(isGround == true)
        {
            anim.SetBool("falling",false);
            anim.SetBool("jumping",false);
            anim.SetBool("idle",true);
        }
        else if(isGround == false && rb.velocity.y > 0)
        {
            anim.SetBool("falling",false);
            anim.SetBool("jumping",true);
        }
        else if(isGround == false && rb.velocity.y < 0 )
        {
            anim.SetBool("jumping",false);
            anim.SetBool("falling",true);
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
        if(Physics2D.OverlapCircle(head.position,0.2f,ground) == false)
        {
            if (Input.GetKey(KeyCode.DownArrow) && coll.IsTouchingLayers(ground) == true)
            {
                headColl.enabled = false;
                anim.SetBool("crouching",true);
                anim.SetBool("idle",false);
            }
            else if(!Input.GetKey(KeyCode.DownArrow))
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
            Invoke("reStart",2);
        }
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
                anim.SetBool("jumping",true);
                Enemy thisEnemy = other.gameObject.GetComponent<Enemy>(); 
                //Destroy(other.gameObject);
                thisEnemy.JumpOn();
            }
            else
            {
                isHurt = true;
                if(invincible == false)
                {
                    hp -= 1;
                    yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                    //Debug.Log("当前HP："+ hp);
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
                }
                else if(k > 0)
                {
                    this.GetComponent<Rigidbody2D>().velocity =  new Vector2(5, this.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }
    }

    public void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
