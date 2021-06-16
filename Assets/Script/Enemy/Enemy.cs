using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator anim; //动画控制
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected AudioSource deathAudio;
    public bool isHurt;
    public int hp;

    protected void Awake() 
    {
        isHurt = false;   
    }
    protected virtual void Start()  //virtual：可以被重新定义
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        deathAudio = GetComponent<AudioSource>();
        hp = 1;
    }

    public void SetHP(int i)
    {
        hp += i;
    }

    public int getHP()
    {
        return hp;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void enemyDeath()
    {
        Destroy(gameObject);       
    }
    
    public void JumpOn()
    {
        anim.SetTrigger("deathTrigger");
        deathAudio.PlayOneShot(deathAudio.GetComponent<AudioSource>().clip);
        coll.isTrigger = true;
    }



}
