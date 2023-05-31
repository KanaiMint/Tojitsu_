using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargusControllerScript : MonoBehaviour
{
    public Vector3 vel;
    public float MoveSpeed;
    public bool isGround;
    public GroundCheck check;
    public float isGroundFlame;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<GroundCheck>();
        vel = new Vector2(0,-2.0f);
        isGroundFlame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += vel*Time.deltaTime;
        if (check.IsGround())
        {
            isGround = true;
            isGroundFlame += Time.deltaTime;
            if (isGroundFlame > 2)
            {
                vel = new Vector3(0, 3f);
               
            }
            else if (isGroundFlame > 0.25f)
            {
                vel = new Vector3(0, -0.7f);
            }
        }
        else
        {
            isGround = false;
            isGroundFlame = 0;
            vel += new Vector3(0, -0.05f);
        }
    }

   
}
