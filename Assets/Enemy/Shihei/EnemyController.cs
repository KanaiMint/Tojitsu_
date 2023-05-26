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

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ShotFlame = 0;
        //Vel = new Vector2(Movespeed_Second * Time.deltaTime, 0.0f);
    }

    public bool isGround = false;
    public bool GetIsGround()
    {
        return isGround;

    }


    private void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position += Vel;

        //毎フレームx方向リセット
        Vel.x = 0;

        //地面に触れていない間重力加算
        if (!isGround)
        {
            Vel.y += -gravity;
        }

        ShotFlame += Time.deltaTime;
        Mathf.Clamp(ShotFlame, 0, 50);
        if (Player != null)
        {
            if (Player.transform.position.x < this.transform.position.x)
            {

                muki = false;
            }
            else
            {
                muki = true;

            }
        }
        if (ShotFlame > 2)
        {
                Debug.Log("awdadawdasdwa");
            if (Vector2.Distance(Player.transform.position, new Vector2(transform.position.x, transform.position.y)) < 5.0f) //近くにいたら
            {
                GameObject Bullet = Instantiate(BulletPrefab, this.transform.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
                controller.muki = muki;

            }
            else
            {
                Vector2 direction = Player.transform.position - transform.position;
                direction.Normalize();
                Vel.x += direction.x*Time.deltaTime;

            }



            ShotFlame = kShotFlame;
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
