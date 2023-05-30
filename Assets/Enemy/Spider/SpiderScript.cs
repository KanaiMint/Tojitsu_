using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiderScript : MonoBehaviour
{
    public Vector2 Taregtpos;
    public Vector2 Homepos;
    public bool isHook;
    public bool isShot;
    public float Himoflame;
    public float distance;
    public GameObject spiderhimoPrefab;
    private GameObject spiderhimo;
    public GameObject spiderBulletPrefab;
    public bool isReturn;
    private GameObject Player;
    private LineRenderer lineRenderer;

    public GameObject HimoCollision;
    public bool cut = false;
    private bool cutInit = false;

    public GroundCheck ground;
    public Rigidbody2D rb;
    private Vector3 vel = Vector3.zero;
    public float gravity = 0.8f;
    public bool isGround = false;//
    public EnemyStatus Es;
    // Start is called before the first frame update
    void Start()
    {
        Homepos = transform.position;
        Taregtpos = new Vector2(transform.position.x, transform.position.y - 3);
        Player = GameObject.Find("Player");
        lineRenderer = GetComponent<LineRenderer>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

        cut = HimoCollision.GetComponent<HimoCollisionScript>().cut;
        isGround = ground.IsGround();
        //ひもが切られたら
        if (cut == true)
        {
            if (cutInit == false)
            {
                HimoCollision.SetActive(false);
                if (spiderhimo != null)
                {
                    Destroy(spiderhimo);
                }
                Es.invincible = false;
                lineRenderer.enabled = false;
                rb.isKinematic = false;
                cutInit = true;
            }

            

            if (!isGround)
            {
                vel.y += -gravity;
            }


            if (isGround)
            {
                vel.y = 0.0f;

            }

            rb.velocity = vel ;
        }
        else
        {
            DrawLine();
            if (isHook == false)
            {
                Himoflame += Time.deltaTime;

            }
            if (Himoflame > 2 && isHook == false)
            {
                isHook = true;
                if (spiderhimo != null)
                {
                    Destroy(spiderhimo);
                }

                spiderhimo = Instantiate(spiderhimoPrefab, transform);
            }

            if (isHook)
            {

                distance = Vector2.Distance(transform.position, Taregtpos);
                if (Vector2.Distance(transform.position, Taregtpos) < 0.1f)
                {
                    Himoflame += Time.deltaTime;

                    if (Himoflame <= 2 && isShot == false)
                    {
                        Vector2 direction = Player.transform.position - transform.position;
                        direction.Normalize();
                        GameObject Bullet = Instantiate(spiderBulletPrefab, this.transform.position, Quaternion.identity);
                        Bullet.GetComponent<BulletController>().Direction = direction;
                        Bullet.GetComponent<BulletController>().BulletSpeed = 10;



                        isShot = true;
                    }
                    if (Himoflame > 3)
                    {
                        Vector2 direction = Player.transform.position - transform.position;
                        direction.Normalize();
                        GameObject Bullet = Instantiate(spiderBulletPrefab, this.transform.position, Quaternion.identity);
                        Bullet.GetComponent<BulletController>().Direction = direction;
                        Bullet.GetComponent<BulletController>().BulletSpeed = 10;
                        isReturn = true;

                    }

                }
                else if (isReturn == false)
                {
                    Himoflame = 0;
                    Vector3 newPosition = Vector2.MoveTowards(transform.position, Taregtpos, 5.0f * Time.deltaTime);

                    transform.position = newPosition;

                    float HimoPos = 0;
                    HimoPos += 50;
                    spiderhimo.GetComponent<himo>().transform.localScale = new Vector3(1, 1 + HimoPos * Time.deltaTime, 1);
                }
                if (isReturn == true)
                {
                    Vector3 newPosition = Vector2.MoveTowards(transform.position, Homepos, 5.0f * Time.deltaTime);
                    transform.position = newPosition;

                    float HimoPos = 0;
                    HimoPos += 50;
                    spiderhimo.GetComponent<himo>().transform.localScale = new Vector3(1, 1 + HimoPos * Time.deltaTime, 1);

                    if (Vector2.Distance(transform.position, Homepos) < 0.1f)
                    {
                        Himoflame = 0;
                        isHook = false;
                        isReturn = false;
                        isShot = false;
                    }
                }

                HimoCollision.transform.localScale = new Vector3(1, Homepos.y - transform.position.y, 1);
                HimoCollision.transform.localPosition = new Vector3(0, (Homepos.y - transform.position.y) / 2, 0);
            }

        }

    }

    private void DrawLine()
    {
        lineRenderer.positionCount = 2; // 線の頂点数を2に設定
        lineRenderer.SetPosition(0, Homepos); // 線の始点を座標Aに設定
        lineRenderer.SetPosition(1, transform.position); // 線の終点を座標Bに設定
    }

    
}
