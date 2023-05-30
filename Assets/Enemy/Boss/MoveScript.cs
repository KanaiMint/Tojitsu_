using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private Vector3 Startpos;
    public float MoveSpeed;
    public Vector3 MoveDirection;
    private GameObject Player;
    public GameObject bullet1;
    public bool isAttack=false;
    public Vector2 TaregtLeftpos;
    public Vector2 TaregtRightpos;
    public float KAttackSecond;
    public float AttackFlame;

    public float Bulletflame = 0;

   public bool isleft = false;
    public bool isright = false;

    public int Attackkind;
    // Start is called before the first frame update
    void Start()
    {
        Player =  GameObject.Find("Player");
        Startpos=transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == false)
        {
           
            Attackkind = Random.Range(1, 3);
            Vector3 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 3), 6f * Time.deltaTime);
            transform.position = newPosition;
            MoveDirection = Player.transform.position - transform.position;
            MoveDirection.Normalize();
            transform.position += new Vector3( MoveDirection.x,0,0 )* MoveSpeed * Time.deltaTime;
           // transform.position.Set(transform.position.x,Startpos.y,0);
            AttackFlame += Time.deltaTime;
            if (AttackFlame > KAttackSecond)
            {
                isAttack = true;
                //Attackkind = Random.Range(1, 2);
            }
        }else if (isAttack == true) 
        {
            AttackFlame = 0;
            if (Attackkind == 1) 
            {
                Attack1();
            }
            else if(Attackkind == 2)
            {
                Attack2();

            }
            else if(Attackkind == 3)
            {

            }
            else if (Attackkind == 4)
            {

            }
            else if(Attackkind == 5) {

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
}
