using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public Rigidbody2D ThisRigitBody;
    public float MoveSpeed;

    private float Rotate = 0.0f;

    private Vector2 MoveVelocity = Vector2.zero;

    public GameObject DebugCircle;
    public GameObject DebugCircle2;

    // Start is called before the first frame update
    void Start()
    {
        MoveVelocity.x = MoveSpeed;
        Rotate = ThisRigitBody.transform.eulerAngles.z;
        ThisRigitBody.transform.eulerAngles = Vector3.zero;

        MoveVelocity.x = Mathf.Cos(Rotate / 180.0f * Mathf.PI) * MoveSpeed;
        MoveVelocity.y = Mathf.Sin(Rotate / 180.0f * Mathf.PI) * MoveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

        ThisRigitBody.velocity = MoveVelocity;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {



            Vector3 ThisPos = this.transform.position;
            Vector3 BlockPos = collision.ClosestPoint(this.transform.position);


            float DistanceX = Mathf.Abs(BlockPos.x) - Mathf.Abs(ThisPos.x);
            float DistanceY = Mathf.Abs(BlockPos.y) - Mathf.Abs(ThisPos.y);

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
}
