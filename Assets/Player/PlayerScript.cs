using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{


    public Vector2 pVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;
    public float jumpHeight;
    public GroundCheck ground;
    public CeilingCheck ceiling;
    public Rigidbody2D playerRigidBody;

    public GameObject BoomerangPrefab;

    public float ShotRot = 0;

    private float isPreSpace;

    private float jumpPos = 0.0f;
    private bool isJump = false;
    private bool isBoomerangJump = false;

    public bool isGround = false;
    public bool isCeiling = false;

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

    private Animator animator;

    public GameObject TargetCircle;
    public float TargetCirclePosPlus = 5.0f;

    float horizontalInputR;
    float VerticalInputtR;

    private Vector2 CameraSize = new Vector2 (13.5f,7.5f);

    public AudioClip soundJumpClip; // 再生する効果音のAudioClip
    public AudioClip soundBoomeranJumpClip; // 再生する効果音のAudioClip
    public AudioClip sounddamageClip; // 再生する効果音のAudioClip
    private AudioSource audioSource;

    public GameObject ReSpawnPoint;
    public GameObject BreakBoomeran;
    public float RandomScale = 0.5f;
    public bool GetIsGround()
    {
        return isGround;
    }

    public void Init()
    {
        isGround = false;
        isCeiling = false;
        pVelocity = Vector3.zero;
        playerRigidBody.velocity = Vector3.zero;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        // AudioSourceコンポーネントを取得する
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("HorizontalR")) > 0.5f || Mathf.Abs(Input.GetAxis("VerticalR")) > 0.5f)
        {
            horizontalInputR = Input.GetAxis("HorizontalR");

            VerticalInputtR = Input.GetAxis("VerticalR");
        }

    }
        void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInputt = Input.GetAxis("Vertical");


        float DeadZone = 0.3f;

        if(ReSpawnPoint == null)
        {
            ReSpawnPoint = GameObject.Find("RespawnPoint");
        }
        else
        {

        }


        if(this.transform.position.y < -15.0f)
        {
            Init();
            this.transform.position = ReSpawnPoint.transform.position;
        }

        if (invincibleTime > 0)
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

        if(Mathf.Abs(horizontalInputR) > 0 || Mathf.Abs(VerticalInputtR) > 0)
        {
            Radian = Mathf.Atan2(VerticalInputtR * 10000, horizontalInputR * 10000);
        }

        //Debug.Log(horizontalInputR);
        //Debug.Log(VerticalInputtR);
        ShotRot = Radian * 180.0f / Mathf.PI;

        Vector3 psfoapofpadofpasodfpoapfd = new Vector3(Mathf.Cos(Radian) * TargetCirclePosPlus, Mathf.Sin(Radian) * TargetCirclePosPlus, 0.0f);
        psfoapofpadofpasodfpoapfd += this.transform.position;
        //TargetCircle.transform.localPosition = new Vector3(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"), 0.0f);

        psfoapofpadofpasodfpoapfd.x = Mathf.Clamp(psfoapofpadofpasodfpoapfd.x, -CameraSize.x, CameraSize.x);
        psfoapofpadofpasodfpoapfd.y = Mathf.Clamp(psfoapofpadofpasodfpoapfd.y, -CameraSize.y, CameraSize.y);

        TargetCircle.transform.position = psfoapofpadofpasodfpoapfd;

        isGround = false;
        isGround = ground.IsGround();
        isCeiling = ceiling.IsCeiling();

        //Debug.Log(isGround);

        pVelocity.x = 0;

        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        //�E�ړ�
        if (Input.GetKey(KeyCode.D) || horizontalInput > DeadZone) 
        {
            pVelocity.x = moveSpeed;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A) || horizontalInput < -DeadZone)
        {
            pVelocity.x = -moveSpeed;
        }

        if (BoomerangCoolTime > 0)
        {
            BoomerangCoolTime -= Time.deltaTime;
        }


        if (BoomerangCoolTime <= 0.0f)
        {

            if (Input.GetMouseButton(0) || Input.GetAxis("Fire1") != 0)
            {
                float ShotPosPuls = 0.32f;
                Vector3 ShotPos = new Vector3(ShotPosPuls, ShotPosPuls * 2.0f - 0.08f, 0.0f);

                ShotPos.x *= Mathf.Cos(Radian);
                ShotPos.y *= Mathf.Sin(Radian);

                GameObject boomerang = Instantiate(BoomerangPrefab, this.transform.position + ShotPos, Quaternion.Euler(0.0f, 0.0f, ShotRot));
                BoomerangCoolTime = kBoomerangCoolTimeMax;
            }
        }




        if (isGround)
        {

            pVelocity.y = 0;

            //�W�����v�����A�������ł͔������Ȃ�
            if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Jump") != 0) && isPreSpace == 0)
            {
                PlayjumpSound();
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
            if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Jump") != 0) && jumpPos + jumpHeight > transform.position.y && isCeiling == false)
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
            if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Jump") != 0 ) && jumpPos + jumpHeight > transform.position.y && isCeiling == false)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
                
            }
            else
            {

                isJump = false;
            }

        }


        if (isJump == true || isBoomerangJump == true)
        {

            animator.SetBool("isJump", true);



        }
        else
        {
            if (isGround == true)
            {
                animator.SetBool("isJump", false);
            }
            if (pVelocity.x != 0)
            {

                animator.SetBool("isWalk", true);

            }
            else
            {
                animator.SetBool("isWalk", false);
            }

        }

        if (pVelocity.x > 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (pVelocity.x < 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }



        playerRigidBody.velocity = pVelocity;

        prePos = this.transform.position;
        isPreSpace = Input.GetAxis("Jump");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool canJump = false;

        if (collision.tag == "Boomerang")
        {
           
            float preplayerBottom = prePos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 4;
            float playerBottom = this.transform.position.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 4;
            float tragetTop = collision.transform.position.y + collision.gameObject.transform.localScale.y * 0.2f;
            //tragetTop = collision.ClosestPoint(this.transform.position).y;

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
            PlayjumpBoomeranSound();
            for(int i = 0; i < 5; i++)
            {
                Vector3 vector = new Vector3(transform.position.x + Random.Range(-RandomScale, RandomScale), transform.position.y + Random.Range(-RandomScale, RandomScale), 0);
                GameObject bumeran1 = Instantiate(BreakBoomeran, vector, Quaternion.identity);

            }
           
        }

        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {


            if (invincible == false)
            {
                if (collision.tag == "EnemyBullet")
                {
                    Destroy(collision.gameObject);
                }
                PlayDamageSound();
                HP--;
                invincibleTime = kMaxInvincibleTime;
                invincible = true;
            }
        }
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    bool canJump = false;

    //    if (collision.tag == "Boomerang")
    //    {

    //        float preplayerBottom = prePos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 4;
    //        float playerBottom = this.transform.position.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 4;
    //        float tragetTop = collision.transform.position.y + collision.gameObject.transform.localScale.y * 0.2f;
    //        //tragetTop = collision.ClosestPoint(this.transform.position).y;

    //        if (preplayerBottom >= tragetTop || playerBottom >= tragetTop)
    //        {
    //            canJump = true;
    //            jumpPos = transform.position.y;

    //            Destroy(collision.gameObject);
    //        }

    //        Debug.Log("Fucccccccccccccccccccck");

    //    }

    //    if (canJump)
    //    {
    //        //y�����̈ړ��x�N�g���ɑ��
    //        pVelocity.y = jumpSpeed;
    //        //�W�����v����y���W�ۑ�
    //        jumpPos = transform.position.y;
    //        //�W�����v�t���O��true��
    //        isBoomerangJump = true;
    //    }
    //}

    private void PlayjumpSound()
    {
        // 効果音を再生する
        audioSource.PlayOneShot(soundJumpClip);
    }
    private void PlayjumpBoomeranSound()
    {
        // 効果音を再生する
        audioSource.PlayOneShot(soundBoomeranJumpClip);
    }
    private void PlayDamageSound()
    {
       
        // 効果音を再生する
        audioSource.PlayOneShot(sounddamageClip);
    }
}
