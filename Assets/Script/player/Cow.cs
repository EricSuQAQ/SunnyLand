using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cow : Player
{
    // Start is called before the first frame update
    public float attackInterval,summonInterval,WudiInterval;
    public bool shootPressed,summonPressed,WudiPressed;

    [SerializeField] private Collider2D axeColl;
    public GameObject summonStone;
    public AudioSource yoo;
    private bool isWudi;

    protected override void Awake() 
    {
        base.Awake();
        attackInterval = 0.3f;
        summonInterval = 10f;
        WudiInterval = 8f;
        shootPressed = false;
        jumpForce = 10;
        GameObject Skill = GameObject.Find("Canvas/Skill/cow");
        Skill.SetActive(true);

        yoo = GameObject.Find("audioGroup/yoo").GetComponent<AudioSource>();
        axeColl = GameObject.Find("player/axe").GetComponent<Collider2D>();
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
        summonInterval += Time.deltaTime;
        WudiInterval += Time.deltaTime;
        if(Input.GetKeyDown(KeyContr.KC.ShootKey) && attackInterval >= 0.5 && headColl.enabled == true)
        {
            shootPressed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.WudiKey) && WudiInterval >= 8f)
        {
            WudiPressed = true;
        }
        if(Input.GetKeyDown(KeyContr.KC.DashKey) && summonInterval >= 10f)
        {
            summonPressed = true;
        }
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();
        if(isHurt == false && isLive == true)
        {
            attack();
            summon();
            olaf();
        }
    }

    public void olaf()
    {
        if(WudiPressed == true)
        {
            WudiPressed = false;
            isWudi = true;
            sr.color = new Color(1,0,0);
            Invoke("backToVicible",3f);
        }
    }

    private void backToVicible() //中亚时间结束
    {
        isWudi = false;
        sr.color = new Color(1,1,1);
        WudiInterval = 0f;
        GameObject CDMask = GameObject.Find("Canvas/Skill/cow/奥拉夫");
        CDMask.SendMessage("RefleshCD");       
    }

    public void summon()
    {
        if(summonPressed == true)
        {
            GameObject CDMask = GameObject.Find("Canvas/Skill/cow/土块");
            CDMask.SendMessage("RefleshCD");
            GameObject tempSummon = Instantiate(summonStone);
            tempSummon.transform.position = feet.transform.position - 0.2f * this.transform.up;
            summonPressed = false;
            summonInterval = 0;
            Destroy(tempSummon,5f);
        }
    }

    public void attack()
    {
        if(shootPressed == true) //下蹲时不可攻击
        {
            if(this.transform.localScale.x < 0)
            {
                axeColl.gameObject.transform.position = this.transform.position + new Vector3(-1.2f,0,0);
            }  
            if(this.transform.localScale.x > 0)
            {
                axeColl.gameObject.transform.position = this.transform.position;
            }           
            shootPressed = false;
            axeColl.enabled = true;
            anim.SetBool("attack",true);
            Invoke("attackDown",0.5f);
        }   
    }

    public void attackDown()
    {   
        axeColl.enabled = false;
        attackInterval = 0;
    }

    public void attackAnimationDown()
    {
        anim.SetBool("attack",false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.gameObject.tag == "enemyBullet") 
        {
            if(isWudi == true || (anim.GetBool("falling") == true && feet.position.y > other.gameObject.transform.position.y))
            {
                treadEnemy(other.gameObject);
            }
            else if(isHurt == false)
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
            if(isWudi == true ||(anim.GetBool("falling") == true && feet.position.y > other.gameObject.transform.position.y))
            {
                treadEnemy(other.gameObject);
            }
            else if(isHurt == false)
            {
                yoo.PlayOneShot(yoo.GetComponent<AudioSource>().clip);
                hurt(1,other.gameObject);
            } 
        }
    }
}
