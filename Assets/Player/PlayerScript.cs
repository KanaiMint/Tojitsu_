using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

   
    public Vector2 pVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;
    public float jumpHeight;
    public GroundCheck ground;
    public Rigidbody2D playerRigidBody;

    public GameObject BoomerangPrefab;

    public float ShotRot = 0;

    private bool isPreSpace;

    private float jumpPos = 0.0f;
    private bool isJump = false;

    public bool isGround = false;

    private Vector3 MousePos = Vector3.zero;
    private Vector3 CameraMousePos = Vector3.zero;
    private Vector3 ThisPos = Vector3.zero;

   

    public bool GetIsGround()
    {
        return isGround;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ThisPos = this.transform.position;
        MousePos = Input.mousePosition;

        CameraMousePos = Camera.main.ScreenToWorldPoint(MousePos);

        float Radian = Mathf.Atan2(CameraMousePos.y - ThisPos.y, CameraMousePos.x - ThisPos.x);

        ShotRot = Radian * 180.0f / Mathf.PI;

        //�ڒn����𓾂�
        isGround = ground.IsGround();

        //���t���[��x�������Z�b�g
        pVelocity.x = 0;

        //�n�ʂɐG��Ă��Ȃ��ԏd�͉��Z
        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            pVelocity.x = moveSpeed;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            pVelocity.x = -moveSpeed;
        }

        if (Input.GetMouseButton(0) )
        {



            GameObject boomerang = Instantiate(BoomerangPrefab,this.transform.position, Quaternion.Euler(0.0f,0.0f, ShotRot));
            
        }

        

        if (isGround)
        {

            pVelocity.y = 0;
            Debug.Log("a");

            //�W�����v�����A�������ł͔������Ȃ�
            if (Input.GetKey(KeyCode.Space) && !isPreSpace)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
                //�W�����v����y���W�ۑ�
                jumpPos = transform.position.y;
                //�W�����v�t���O��true��
                isJump = true;
            }
            else
            {
                //������Ă��Ȃ��Ԃ̓W�����v�t���O��false��
                isJump = false;
            }
        }
        else if (isJump)
        {
            //�W�����v���ɂ����͎�t�A�������ō�����ׂ�
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
            }
            else
            {

                isJump = false;
            }

        }


        playerRigidBody.velocity = pVelocity;

        isPreSpace = Input.GetKey(KeyCode.Space);
    }

   

    

}
