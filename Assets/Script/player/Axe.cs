using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemyBullet") 
        {
            Destroy(other.gameObject);
            Debug.Log("子弹皮雕");
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "enemy")
        {
            Enemy thisEnemy = other.gameObject.GetComponent<Enemy>();
            thisEnemy.SetHP(-1);
            if(thisEnemy.getHP() <= 0)
            {
                thisEnemy.JumpOn();
            }
            Debug.Log("yoo你妈" + other.gameObject.name) ;
        }
    }
}
