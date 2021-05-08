using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat : Enemy
{
    // Start is called before the first frame update
    public Transform leftPoint,rightPoint;
    public float speed;
    private bool faceLeft;
    private float leftx,rightx;

    void Awake() 
    { 
        faceLeft = true; 
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
        

        if(faceLeft == true)
        {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }
    }
}
