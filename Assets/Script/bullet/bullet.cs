using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private AudioSource bulletAudio;
    public float speed = 10f;
    public LayerMask ground;
    public float upSpeed = 0f;
    public int bulletPower = 1;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        bulletAudio = GetComponent<AudioSource>();
        //bulletAudio.PlayOneShot(bulletAudio.GetComponent<AudioSource>().clip);   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime,upSpeed* Time.deltaTime,0,Space.Self);
        if(coll.IsTouchingLayers(ground) == true)
        {
            Destroy(this.gameObject);
        }
        Vector3 sp = Camera.main.WorldToScreenPoint(this.transform.position);
        if(sp.x > Screen.width || sp.x < 0)
        {
            //Debug.Log("超出屏幕右边界，自动销毁");
            Destroy(this.gameObject);
        }
    }

    public void setSpeed(float k)
    {
        speed = k;
    }

    public void setPower(int k)
    {
        bulletPower = k;
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(this.gameObject.tag != "blackHole" && other.gameObject.tag == "enemy")
        {
            Enemy thisEnemy = other.gameObject.GetComponent<Enemy>();
            thisEnemy.SetHP(-bulletPower);
            if(thisEnemy.getHP() <= 0)
            {
                thisEnemy.JumpOn();
            }
            Destroy(this.gameObject);
        }
        /*
        else if(other.gameObject.tag == "boss")
        {
            Destroy(this.gameObject);
            Boss thisEnemy = other.gameObject.GetComponent<Boss>();
            thisEnemy.hurt();
        }*/
    }
}
