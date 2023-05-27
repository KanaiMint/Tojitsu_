using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public Rigidbody2D ThisRigitBody;
    public float MoveSpeed;

    private float Rotate = 0.0f;

    private Vector2 MoveVelocity = Vector2.zero;
    private Vector2 preVelocity = Vector2.zero;
    private Vector3 prePos = Vector3.zero;
    private Vector2 prepreVelocity = Vector2.zero;
    private Vector3 preprePos = Vector3.zero;

    public GameObject DebugCircle;
    public GameObject DebugCircle2;

    public float DistanceX;
    public float DistanceY;

    private float ColRad = 0.2f;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        MoveVelocity.x = MoveSpeed;
        Rotate = ThisRigitBody.transform.eulerAngles.z;
        ThisRigitBody.transform.eulerAngles = Vector3.zero;

        MoveVelocity.x = Mathf.Cos(Rotate / 180.0f * Mathf.PI) * MoveSpeed;
        MoveVelocity.y = Mathf.Sin(Rotate / 180.0f * Mathf.PI) * MoveSpeed;

        DebugCircle = GameObject.Find("DebugCircle");
        DebugCircle2 = GameObject.Find("DebugCircle2");

        prePos = this.transform.position;
        preprePos = prePos;
        preVelocity = MoveVelocity;
        prepreVelocity = preVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        ThisRigitBody.velocity = MoveVelocity;
        preprePos = prePos;
        prepreVelocity = preVelocity;
        prePos = this.transform.position;
        preVelocity = MoveVelocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {



            Vector3 ThisPos = this.transform.position;
            Vector3 BlockPos = collision.ClosestPoint(this.transform.position);

            DebugCircle.transform.position = ThisPos;
            DebugCircle2.transform.position = BlockPos;

            if (MoveVelocity.y > 0 && ThisPos.y > BlockPos.y)
            {
                BlockPos.y = ThisPos.y;
            }
            if (MoveVelocity.y < 0 && ThisPos.y < BlockPos.y)
            {
                BlockPos.y = ThisPos.y;
            }

            if (MoveVelocity.x > 0 && ThisPos.x > BlockPos.x)
            {
                BlockPos.x = ThisPos.x;
            }
            if (MoveVelocity.x < 0 && ThisPos.x < BlockPos.x)
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

            Vector3 ThisPos = this.transform.position;
            Vector3 BlockPos = collision.ClosestPoint(this.transform.position);

            DebugCircle.transform.position = ThisPos;
            DebugCircle2.transform.position = BlockPos;

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
}