using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public EnemyController EnemyController_Script;
    public Vector3 Vel;
    public bool IsDead = false;
    public float lifeTime = 0;
    public const int KlifeTime = 3;
    public bool muki = false;
    public float BulletSpeed=5;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (muki == false)
        {
            spriteRenderer.flipX = false;
            Vel = new Vector3(-BulletSpeed, 0, 0);
            
        }
        else
        {
            spriteRenderer.flipX = true;
            Vel = new Vector3(BulletSpeed, 0, 0);
        }
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position += Vel*Time.deltaTime;
        lifeTime+= Time.deltaTime;
        if(lifeTime>2) { Destroy(this.gameObject); }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag( "Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
