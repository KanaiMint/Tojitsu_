using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour
{
    private Vector3 Startpos;
    public float MoveSpeed;
    public Vector3 MoveDirection;
    private GameObject Player;
    public GameObject bullet1;
    public GameObject bumeranpurefab;
    public GameObject Himo;
    public Himocollision himocollision;
    public bool isAttack=false;
    public Vector2 TaregtLeftpos;
    public Vector2 TaregtRightpos;
    public float KAttackSecond;
    public float AttackFlame;
    public int HP;
    private int MaxHP=250;
    public float Bulletflame = 0;

   public bool isleft = false;
    public bool isright = false;

    public int Attackkind;
    private LineRenderer linerenderer;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Player =  GameObject.Find("Player");
        Startpos=transform.position;
        himocollision=Himo.GetComponent<Himocollision>();
        linerenderer = this.GetComponent<LineRenderer>();
        HP = MaxHP;
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //HPÇSliderÇ…îΩâfÅB
        slider.value = (float)HP / (float)MaxHP; 

        if (HP<=0) { Destroy(this.gameObject); }

        if (himocollision.Cut == true)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -2), 6f * Time.deltaTime);
            transform.position = newPosition;
            isAttack = false;
            linerenderer.enabled = false;
        }
        else
        {
            linerenderer.enabled = true;

            if (himocollision.HP < 7)
            {
                linerenderer.startColor= Color.yellow;
                linerenderer.endColor= Color.yellow;
                if (himocollision.HP < 4)
                {
                    linerenderer.startColor = Color.red;
                    linerenderer.endColor = Color.red;
                }
            }
            else
            {
                linerenderer.startColor = Color.green;
                linerenderer.endColor = Color.green;
            }

            if (isAttack == false)
            {

                Attackkind = Random.Range(1, 4);
                Vector3 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 3), 6f * Time.deltaTime);
                transform.position = newPosition;
                MoveDirection = Player.transform.position - transform.position;
                MoveDirection.Normalize();
                transform.position += new Vector3(MoveDirection.x, 0, 0) * MoveSpeed * Time.deltaTime;
                // transform.position.Set(transform.position.x,Startpos.y,0);
                AttackFlame += Time.deltaTime;
                if (AttackFlame > KAttackSecond)
                {
                    isAttack = true;
                    //Attackkind = Random.Range(1, 2);
                }
            }
            else if (isAttack == true)
            {
                AttackFlame = 0;
                if (Attackkind == 1)
                {
                    Attack1();
                }
                else if (Attackkind == 2)
                {
                    Attack2();

                }
                else if (Attackkind == 3)
                {
                    GameObject Bumeran = Instantiate(bumeranpurefab, transform);
                    if (Random.Range(1, 3) == 1)
                    {
                        Bumeran.GetComponent<BumeranScript>().isleft = true;

                    }
                    else
                    {
                        Bumeran.GetComponent<BumeranScript>().isright = true;
                    }
                    isAttack = false;
                }
                else if (Attackkind == 4)
                {

                }
                else if (Attackkind == 5)
                {

                }
            }
        }


    }

    private void Attack1()
    {
       
        if (Vector2.Distance(transform.position, TaregtLeftpos) < 0.1f && isleft == true)
        {
            isright=true;
        }
        else if(isright==false)
        {
            isleft = true;
            Vector3 newPosition = Vector2.MoveTowards(transform.position, TaregtLeftpos, 6f * Time.deltaTime);
            transform.position = newPosition;
        }
        if (isright==true)
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, TaregtRightpos, 3f * Time.deltaTime);
            transform.position = newPosition;
            if (Vector2.Distance(transform.position, TaregtRightpos) < 0.1f )
            {
                isright = false;
                isleft = false;
                isAttack = false;
            }
            else
            {
                Bulletflame += Time.deltaTime;
                if (Bulletflame > 0.1f)
                {
                    GameObject bullet = Instantiate(bullet1, transform);
                    Bulletflame = 0;
                }
            }

          
        }
    }

    private void Attack2()
    {
        if (Vector2.Distance(transform.position, new Vector2(transform.position.x, -2)) < 0.1f)
        {
            isright = false;
            isleft = false;
            isAttack = false;
        }
        else
        {
            Vector3 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -2), 6f * Time.deltaTime);
            transform.position = newPosition;
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boomerang")
        {
            if (himocollision.Cut == true)
            {
                HP -= 5;
                Destroy(collision.gameObject);

            }
            if(himocollision.Cut == false)
            {
                HP -= 1; Destroy(collision.gameObject);
            }
        }
    }
}
