using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fox : Player
{
    // Start is called before the first frame update
    public float attackInterval,backSteepInterval,dashInterval;

    private GameObject dashShadow;
    private bool dashReady;
    public bool isWudi,isDash;

    [SerializeField] private AudioSource bulletAudio,yoo;
    public GameObject whiteBullet,blackHoleBullet;

    public bool shootPreesed,WudiPressed,DashPressed;
    
    protected override void Awake() 
    {
        base.Awake();
        attackInterval = 0.5f;
        backSteepInterval = 3f;
        dashInterval = 8f;

        dashShadow = null;
        dashReady = false;

        GameObject Skill = GameObject.Find("Canvas/Skill/fox");
        Skill.SetActive(true);

        yoo = GameObject.Find("audioGroup/yoo").GetComponent<AudioSource>();
        bulletAudio = GameObject.Find("audioGroup/普通攻击").GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        attackInterval += Time.deltaTime;
        backSteepInterval += Time.deltaTime;
        dashInterval += Time.deltaTime;
        if(Input.GetKeyDown(KeyContr.KC.ShootKey) && attackInterval >= 0.5)
        {
            shootPreesed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.WudiKey) && backSteepInterval >= 3)
        {
            WudiPressed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.DashKey) && dashInterval >= 8)
        {
            DashPressed = true;
        }

        if(dashShadow == null && dashReady == true)
        {
            dashReady = false;
            dashInterval = 0;   
            GameObject CDMask = GameObject.Find("Canvas/Skill/fox/猎手本能");
            CDMask.SendMessage("RefleshCD");         
        }
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();
        if(isHurt == false && isLive == true)
        {
            shoot();
            backSteep();
            dash();
        }
        else
        {
            jumpPreesed = false;
            WudiPressed = false;
        }
    }

    private void backSteep() //小狐狸1技能：中亚沙漏
    {
        if(WudiPressed == true)
        {
            WudiPressed = false;
            isWudi = true;
            headColl.enabled = false;
            rb.velocity = new Vector2 (-40f * this.transform.localScale.x, 0f);
            sr.color = new Color(1,0,0);
            GameObject CDMask = GameObject.Find("Canvas/Skill/fox/无敌中亚");
            CDMask.SendMessage("RefleshCD");
            Invoke("backToVicible",0.3f);
        }
    }

    private void backToVicible() //中亚时间结束
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

    private void dash() //小狐狸2技能：冲刺
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

    private void firstDash() //一段冲刺：射出黑洞子弹
    {
        dashShadow = Instantiate(blackHoleBullet);
        if(faceRight == false)
        {
            dashShadow.SendMessage("setSpeed",-15f);
            dashShadow.transform.position = this.transform.position - 1 * this.transform.right;
        }
        else
        {
            dashShadow.SendMessage("setSpeed",15f);
            dashShadow.transform.position = this.transform.position + 1 * this.transform.right;
        }
        dashReady = true;
    }

    private void secondDash() //二段冲刺：位移到黑洞子弹位置
    {
        dashReady = false;
        dashInterval = 0;
        GameObject CDMask = GameObject.Find("Canvas/Skill/fox/猎手本能");
        CDMask.SendMessage("RefleshCD");
        if(dashShadow != null)
        {
            this.transform.position = dashShadow.transform.position;  
            Destroy(dashShadow);       
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
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.gameObject.tag == "enemyBullet") 
        {
            if(feet.position.y > other.gameObject.transform.position.y)
            {
                treadEnemy(other.gameObject);
            }
            else if(isWudi == false && isHurt == false)
            {
                yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                hurt(1,other.gameObject);
                Destroy(other.gameObject);
            } 
        }    
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "enemy")
        {
            if(feet.position.y > other.gameObject.transform.position.y)
            {
                treadEnemy(other.gameObject);
            }
            else if(isWudi == false && isHurt == false)
            {
                yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                hurt(1,other.gameObject);
            } 
        }
    }

}
