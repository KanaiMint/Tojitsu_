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
    public GameObject BulletPrefab;
    public int HP;
    public float ShotFlame=0;
    const int kShotFlame = 0;
    public bool muki=false;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GetComponent<PlayerScript>();
        ShotFlame = 0;
        Vel = new Vector2(Movespeed_Second * Time.deltaTime, 0.0f);
    }

    private void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vel;

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
            //if (Vector2.Distance(Player.transform.position, new Vector2( transform.position.x, transform.position.y)) < 0.5f) //近くにいたら
            //{
                GameObject Bullet = Instantiate(BulletPrefab, this.transform.position, Quaternion.identity);
                BulletController controller = Bullet.GetComponent<BulletController>();
          
            controller.muki = muki;
           
            

           
            ShotFlame = kShotFlame;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "")    //カッター
        {
            Destroy(this.gameObject);
        }
    }
}
