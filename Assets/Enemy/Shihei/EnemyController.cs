using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerScript Player;
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
        //Player = GetComponent<PlayerScript>();
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

        isGround = ground.IsGround();

        //���t���[��x�������Z�b�g
        Vel.x = 0;

        //�n�ʂɐG��Ă��Ȃ��ԏd�͉��Z
        if (!isGround)
        {
            Vel.y += -gravity;
        }


        if (isGround)
        {
            Vel.y = 0.0f;
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
            //if (Vector2.Distance(Player.transform.position, new Vector2( transform.position.x, transform.position.y)) < 0.5f) //�߂��ɂ�����
            //{
                GameObject Bullet = Instantiate(BulletPrefab, this.transform.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
          
            controller.muki = muki;
           
            

           
            ShotFlame = kShotFlame;
        }

        rigitbody.velocity = Vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "")    //�J�b�^�[
        {
            Destroy(this.gameObject);
        }
    }
}
