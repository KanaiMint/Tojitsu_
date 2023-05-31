using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public EnemyController EnemyController_Script;
    public Vector2 Vel;
    public bool IsDead = false;
    public float lifeTime = 0;
    public const int KlifeTime = 3;
    public bool muki = false;
    public float BulletSpeed=0.03f;
    private SpriteRenderer spriteRenderer;
    public Vector2 Direction;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (muki == false)
        {
            spriteRenderer.flipX = false;      
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        Vel=Direction*BulletSpeed * Time.deltaTime;
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = Vel ;
        transform.position += new Vector3( Vel.x,Vel.y,0);
        lifeTime+= Time.deltaTime;
        if(lifeTime>2) { Destroy(this.gameObject); }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
          Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
