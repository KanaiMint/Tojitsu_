using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BumeranScript : MonoBehaviour
{
    public bool isleft;
    public bool isright;
    public bool isThrow;
    public float Movespeed=1.0f;
    public float LifeTime = 0;
    public Vector2 LeftPos=new Vector2(-9,-2.5f);
    public Vector2 RightPos = new Vector2(9, -2.5f);
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Movespeed * Time.deltaTime*100);
        LifeTime += Time.deltaTime;
        if (LifeTime > 6.0f)
        {
            Destroy(this.gameObject);

        }
        if (isleft)
        {
            if(Vector2.Distance(transform.position, LeftPos) <0.1f&&isThrow==false) 
            {
                isThrow = true;
            }
            else if(isThrow==false) 
            {
                transform.position=Vector2.MoveTowards(transform.position, LeftPos, Movespeed * Time.deltaTime);
            }
            if(isThrow == true)
            {
                transform.position=Vector2.MoveTowards(transform.position, RightPos, Movespeed * Time.deltaTime);
            }
        }

        if (isright)
        {
            if (Vector2.Distance(transform.position, RightPos) < 0.1f && isThrow == false)
            {
                isThrow = true;
            }
            else if (isThrow == false)
            {
                transform.position= Vector2.MoveTowards(transform.position, RightPos, Movespeed * Time.deltaTime);
            }
            if (isThrow == true)
            {
                transform.position= Vector2.MoveTowards(transform.position, LeftPos, Movespeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            Destroy(this.gameObject);
        }
    }
}
