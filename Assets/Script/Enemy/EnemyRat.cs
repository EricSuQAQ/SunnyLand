using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat : Enemy
{
    // Start is called before the first frame update
    public Transform leftPoint,rightPoint;
    public float speed;
    private bool faceLeft,speedChange;
    private float leftx,rightx;

    void Awake() 
    { 
        faceLeft = true; 
        speedChange = false;
    }

    protected override void Start()
    {
        base.Start();
        base.SetHP(4);
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        int randomRat = Random.Range(0, 1000); 
        if(this.transform.position.x/*-speed*/ < leftx  || (randomRat < 2 && faceLeft == true) )
        {
            this.transform.localScale = new Vector3(-1,1,1);
            faceLeft = false;
        }
        else if(this.transform.position.x /*+ speed*/ > rightx || (randomRat < 2 && faceLeft == false))
        {
            this.transform.localScale = new Vector3(1,1,1);
            faceLeft = true;
        }
        
        randomRat = Random.Range(0, 1500);
        if(randomRat < 2 && speedChange == false)
        {
            speedUp();
            speedChange = true;
        }

        if(faceLeft == true)
        {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }
    }

    private void speedUp()
    {
        speed *= 1.4f;
        Invoke("speedDown",0.5f);
    }

    private void speedDown()
    {
        speed /= 1.4f;
        speedChange = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {      
            faceLeft = !faceLeft;
            if(faceLeft == true)
            {
                this.transform.localScale = new Vector3(1,1,1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1,1,1);
            }
                
        }
    }
}
