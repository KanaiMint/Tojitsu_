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

    public float kBoomerangCoolTimeMax = 0;
    private float BoomerangCoolTime = 0;

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

        //接地判定を得る
        isGround = ground.IsGround();

        //毎フレームx方向リセット
        pVelocity.x = 0;

        //地面に触れていない間重力加算
        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            pVelocity.x = moveSpeed;
        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            pVelocity.x = -moveSpeed;
        }

        if (BoomerangCoolTime > 0)
        {
            BoomerangCoolTime -= Time.deltaTime;
        }


        if (BoomerangCoolTime <= 0.0f)
        {
            
            if (Input.GetMouseButton(0))
            {
                GameObject boomerang = Instantiate(BoomerangPrefab, this.transform.position, Quaternion.Euler(0.0f, 0.0f, ShotRot));
                BoomerangCoolTime = kBoomerangCoolTimeMax;
            }
        }
        



            if (isGround)
        {

            pVelocity.y = 0;
            Debug.Log("a");

            //ジャンプ処理、長押しでは反応しない
            if (Input.GetKey(KeyCode.Space) && !isPreSpace)
            {
                //y方向の移動ベクトルに代入
                pVelocity.y = jumpSpeed;
                //ジャンプ時のy座標保存
                jumpPos = transform.position.y;
                //ジャンプフラグをtrueに
                isJump = true;
            }
            else
            {
                //押されていない間はジャンプフラグをfalseに
                isJump = false;
            }
        }
        else if (isJump)
        {
            //ジャンプ中にも入力受付、長押しで高く飛べる
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                //y方向の移動ベクトルに代入
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
