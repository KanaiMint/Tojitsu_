using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public float Movespeed_Second;
    public Vector3 Vel;
    public bool Death;
    public float gravity;
    public GameObject BulletPrefab;
    public int HP;
    public float ShotFlame=0;
    const int kShotFlame = 0;
    public bool muki=false;
    public GroundCheck ground;
    public Rigidbody2D rigitbody;
    private SpriteRenderer spriteRenderer;
    public float DistancePlayer_Shihei;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ShotFlame = 0;
        //Vel = new Vector2(Movespeed_Second * Time.deltaTime, 0.0f);
        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
    }

    public bool isGround = false;
    public bool GetIsGround()
    {
        return isGround;

    }


  
    // Update is called once per frame
    void Update()
    {
       
        isGround = ground.IsGround();



        ////地面に触れていない間重力加算
        if (!isGround)
        {
            Vel.y += -gravity;
        }


        if (isGround)
        {
            Vel.y = 0.0f;

            transform.position += Vel* Time.deltaTime;
        
        }

        ShotFlame += Time.deltaTime;
        Mathf.Clamp(ShotFlame, 0, 50);
        if (Player != null)
        {
            if (Player.transform.position.x < this.transform.position.x)
            {
                spriteRenderer.flipX = false;
                muki = false;
            }
            else
            {
                spriteRenderer.flipX = true;
                muki = true;

            }
        }
        if (ShotFlame > 2)
        {
            
            if (Vector2.Distance(Player.transform.position, new Vector2(transform.position.x, transform.position.y)) < DistancePlayer_Shihei) //近くにいたら
            {
                GameObject Bullet = Instantiate(BulletPrefab, this.transform.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                Vector2 direction = Player.transform.position - transform.position;
                direction.Normalize();
                controller.Direction = direction;
                controller.muki = muki;
                Vel.x = UnityEngine.Random.Range(-2.0f, 2.0f);

            }
            else
            {
                //毎フレームx方向リセット
                //Vel.x = 0;
            }

            ShotFlame = kShotFlame;
        }
        else
        {
            
        }
        if (Vector2.Distance(Player.transform.position, new Vector2(transform.position.x, transform.position.y)) > DistancePlayer_Shihei)
        {
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();
            Vel.x = (direction.x*Movespeed_Second) * Time.deltaTime;
            Debug.Log("awdadawdasdwa");

        }
        else
        {
            
        }

        rigitbody.velocity = Vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "")    //カッター
        {
            Destroy(this.gameObject);
        }
    }
}
