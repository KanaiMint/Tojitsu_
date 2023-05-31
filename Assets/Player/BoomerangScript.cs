using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public Rigidbody2D ThisRigitBody;
    public float MoveSpeed;

    private float Rotate = 0.0f;

    private Vector2 MoveVelocity = Vector2.zero;
    private Vector2 ThisVelocity = Vector2.zero;
    private Vector2 preVelocity = Vector2.zero;
    private Vector3 prePos = Vector3.zero;
    private Vector2 prepreVelocity = Vector2.zero;
    private Vector3 preprePos = Vector3.zero;

    public GameObject Gra;
    private float GraRotate = 0.0f;

    public float DeathTime = 10.0f;
    private float LifeTime = 0.0f;
    private float DeathSlowPer = 1.0f;
    public float DeathSlowPerAcc = 0.01f;

    //public GameObject DebugCircle;
   // public GameObject DebugCircle2;

    public float DistanceX;
    public float DistanceY;

    //private float ColRad = 0.2f;
    public Collider2D col;

    private bool TurnStart = false;
    private bool Reflectioned = false;
    public float TurnStartTime = 5.0f;

    public float TurnStayTime = 2.0f;
    private float TurnPer = 1.0f;
    public float TurnPerMinus = 0.02f;
    private float TurnPerMinusStartTimeMinus = 0.0f;
    private LineRenderer lineRenderer;
    private Vector3 Homepos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        MoveVelocity.x = MoveSpeed;
        Rotate = ThisRigitBody.transform.eulerAngles.z;
        ThisRigitBody.transform.eulerAngles = Vector3.zero;

        MoveVelocity.x = Mathf.Cos(Rotate / 180.0f * Mathf.PI) * MoveSpeed;
        MoveVelocity.y = Mathf.Sin(Rotate / 180.0f * Mathf.PI) * MoveSpeed;

        //DebugCircle = GameObject.Find("DebugCircle");
        //DebugCircle2 = GameObject.Find("DebugCircle2");

        prePos = this.transform.position;
        preprePos = prePos;
        preVelocity = MoveVelocity;
        prepreVelocity = preVelocity;
        lineRenderer = GetComponent<LineRenderer>();
        TurnPerMinusStartTimeMinus = (1.0f / TurnPerMinus) / 60.0f;

        Homepos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        col.enabled = true;
        DrawLine();
        if (LifeTime >= DeathTime)
        {
            DeathSlowPer -= DeathSlowPerAcc;
        }
        else
        {
            LifeTime += Time.deltaTime;
        }

        if (DeathSlowPer <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        if (Reflectioned == false)
        {
            if (LifeTime > TurnStartTime - TurnPerMinusStartTimeMinus)
            {
                TurnStart = true;
                Gra.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f, 1.0f);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            Gra.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f, 1.0f);
        }

        if (TurnStart)
        {
            if (TurnPer > -1.0f)
            {
                if (Reflectioned == false || TurnPer > 0.0f)
                {
                    TurnPer -= TurnPerMinus;
                }
            }

            if (TurnStayTime >= 0.0f && TurnPer <= 0)
            {
                TurnPer = 0;
                TurnStayTime -= Time.deltaTime;
                //Gra.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }


            if (TurnPer < -1.0f)
            {
                TurnPer = -1.0f;
                lineRenderer.enabled = false;
            }

            if (Reflectioned == true && TurnPer <= 0.0f && TurnPer != -1.0f)
            {
                Destroy(this.gameObject);
            }

        }

        int RotSpeed = 20;

        if (MoveVelocity.x < 0)
        {
            GraRotate += RotSpeed * DeathSlowPer;
        }
        else
        {
            GraRotate -= RotSpeed * DeathSlowPer;
        }


        if (GraRotate >= 360.0f)
        {
            GraRotate -= 360.0f;
        }
        else if (GraRotate <= -360.0f)
        {
            GraRotate += 360.0f;
        }

        Gra.transform.eulerAngles = new Vector3(0.0f, 0.0f, GraRotate);
        ThisVelocity = MoveVelocity * DeathSlowPer * TurnPer;
        ThisRigitBody.velocity = ThisVelocity;
        preprePos = prePos;
        prepreVelocity = preVelocity;
        prePos = this.transform.position;
        preVelocity = MoveVelocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {

            Reflectioned = true;

            Vector3 ThisPos = this.transform.position;
            Vector3 BlockPos = collision.ClosestPoint(this.transform.position);

            // DebugCircle.transform.position = ThisPos;
            //DebugCircle2.transform.position = BlockPos;

            if (ThisVelocity.y > 0 && ThisPos.y > BlockPos.y)
            {
                BlockPos.y = ThisPos.y;
            }
            if (ThisVelocity.y < 0 && ThisPos.y < BlockPos.y)
            {
                BlockPos.y = ThisPos.y;
            }

            if (ThisVelocity.x > 0 && ThisPos.x > BlockPos.x)
            {
                BlockPos.x = ThisPos.x;
            }
            if (ThisVelocity.x < 0 && ThisPos.x < BlockPos.x)
            {
                BlockPos.x = ThisPos.x;
            }



            DistanceX = Mathf.Abs(BlockPos.x) - Mathf.Abs(ThisPos.x);
            DistanceY = Mathf.Abs(BlockPos.y) - Mathf.Abs(ThisPos.y);

            if (Mathf.Abs(DistanceY) > Mathf.Abs(DistanceX))
            {
                MoveVelocity.y *= -1;
            }
            else
            {
                MoveVelocity.x *= -1;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Reflectioned = true;

            Vector3 ThisPos = this.transform.position;
            Vector3 BlockPos = collision.ClosestPoint(this.transform.position);

            // DebugCircle.transform.position = ThisPos;
            //DebugCircle2.transform.position = BlockPos;

            col.enabled = false;

            this.transform.position = preprePos;
            MoveVelocity = prepreVelocity;
            MoveVelocity.x *= -1;
            MoveVelocity.y *= -1;
            ThisRigitBody.velocity = MoveVelocity;

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        col.enabled = true;
    }

    private void DrawLine()
    {
        lineRenderer.positionCount = 2; // 線の頂点数を2に設定
        lineRenderer.SetPosition(0, Homepos); // 線の始点を座標Aに設定
        lineRenderer.SetPosition(1, transform.position); // 線の終点を座標Bに設定
    }
}