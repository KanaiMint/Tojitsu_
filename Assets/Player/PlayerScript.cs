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
    private bool isBoomerangJump = false;

    public bool isGround = false;

    private Vector3 MousePos = Vector3.zero;
    private Vector3 CameraMousePos = Vector3.zero;
    private Vector3 ThisPos = Vector3.zero;

    public float kBoomerangCoolTimeMax = 0;
    private float BoomerangCoolTime = 0;

    private Vector3 prePos = Vector3.zero;

    public bool invincible;
    private float invincibleTime;
    public float kMaxInvincibleTime = 2.0f;
    private int HP;
    public int MaxHP;

    public float kDamagedLookTimeMax = 0.2f;
    public float DamagedLookTime = 0.0f;
    public bool SeePlayer = false;

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

        if(invincibleTime > 0)
        {
            invincible = true;

            if (DamagedLookTime > kDamagedLookTimeMax)
            {
                if (SeePlayer == false)
                {
                    SeePlayer = true;
                }
                else
                {
                    SeePlayer = false;
                }
                DamagedLookTime = 0.0f;
            }
            else
            {

                DamagedLookTime += Time.deltaTime;
            }
            invincibleTime -= Time.deltaTime;
        }
        else
        {
            invincible = false;
            SeePlayer = true;
        }

        if (SeePlayer == false)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

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
        else if (isBoomerangJump)
        {
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
            }
            else
            {

                isBoomerangJump = false;
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

        prePos = this.transform.position;
        isPreSpace = Input.GetKey(KeyCode.Space);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool canJump = false;

        if (collision.tag == "Boomerang")
        {

            float preplayerBottom = prePos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float playerBottom = this.transform.position.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float tragetTop = collision.transform.position.y + (collision.gameObject.transform.localScale.y * 0.2f);
            tragetTop = collision.ClosestPoint(this.transform.position).y;

            //DebugPoint.transform.position = new Vector3(prePos.x, playerBottom, prePos.z);
            //DebugPoint2.transform.position = new Vector3(collision.transform.position.x, tragetTop, collision.transform.position.z);

            //�v���C���[�̈ʒu(����)���`���[�N(���)���ォ�Ŕ�����Ƃ�
            if (preplayerBottom >= tragetTop || playerBottom >= tragetTop)
            {
                canJump = true;
                jumpPos = transform.position.y;
                //collision.GetComponent<ChokeScript>().GeneratePowder();

                Destroy(collision.gameObject);
            }



        }

        if (canJump)
        {
            //y�����̈ړ��x�N�g���ɑ��
            pVelocity.y = jumpSpeed;
            //�W�����v����y���W�ۑ�
            jumpPos = transform.position.y;
            //�W�����v�t���O��true��
            isBoomerangJump = true;
        }

        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {
           

            if (invincible == false)
            {
                if (collision.tag == "EnemyBullet")
                {
                    Destroy(collision.gameObject);
                }

                HP--;
                invincibleTime = kMaxInvincibleTime;
                invincible = true;
            }
        }
    }
}
