using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using UnityEditor.SceneManagement;
using System.Linq;
using System;

public class PlCor : MonoBehaviour
{
    private float initialJumPower;
    private SpriteRenderer spriteRenderer;//�v���C���[�̃X�v���C�g��ς��邽��

    private Vector2 Player;//�v���C���[�̌��݂̍��W
    private bool prri = false;//�v���C���[�������Ă��邩�ǂ������m�F����
    private int RoteX;//�v���C�[�̉�]�̊p�x�����߂�
    private int RoteY;
    private int RoteZ;
    private GameObject PlayerManager;
    private BoxCollider2D BoxCol;//�l�p�̃R���C�_�[
    private PolygonCollider2D PolCol;//�O�p�̃R���C�_�[
    private CircleCollider2D CirCol;//�ۂ̃R���C�_�[
    //�O�p�`�̒��_�̍��W
    Vector2[] newTriangle = new Vector2[]
    {
        new Vector2(0.001981445f,0.5127149f),
        new Vector2(-0.5187848f,-0.4691465f),
        new Vector2(0.4776618f,-0.4550597f)
    };
    private bool Sloperight = false;//�E�����
    private bool SlopeLeft = false;//�������
    private bool slope = false;
    private GameObject wallcht;
    private GameObject wallcht2;
    Wallcht wch;
    Wall2 wall2;


    [SerializeField] public float dfSpeed;//�v���C���[�̃f�t�H���g�̑��x
    [SerializeField] public float XSpeed;
    public float dfjumpPower;//�f�t�H���g�W�����v�p���[
    public float BallJumpPower;
    [SerializeField] public float jumpPower;//�v���C���[�̃W�����v��
    [SerializeField] public int JumpCount = 1;//�W�����v�ł����
    public Sprite Square1;//�v���C���[�Ŏg���l�p�̃X�v���C�g
    public Sprite Square2;//�v���C���[�Ŏg���l�p�̃X�v���C�g
    public Sprite SquareLong1;//�v���C���[�̒����l�p�̃X�v���C�g
    public Sprite SquareLong2;//�v���C���[�̒����l�p�̃X�v���C�g
    public Sprite Triangle1;//�v���C���[�Ŏg���O�p�̃X�v���C�g
    public Sprite Triangle2;//�v���C���[�Ŏg���O�p�̃X�v���C�g
    public Sprite Ball1;//�v���C���[�Ŏg���ۂ̃X�v���C�g
    public Sprite Ball2;//�v���C���[�Ŏg���ۂ̃X�v���C�g
    public Sprite Square3;//�v���C���[2�Ŏg���l�p�̃X�v���C�g
    public Sprite Square4;//�v���C���[2�Ŏg���l�p�̃X�v���C�g
    public Sprite SquareLong3;//�v���C���[2�̒����l�p�̃X�v���C�g
    public Sprite SquareLong4;//�v���C���[2�̒����l�p�̃X�v���C�g
    public Sprite Triangle3;//�v���C���[2�Ŏg���O�p�̃X�v���C�g
    public Sprite Triangle4;//�v���C���[2�Ŏg���O�p�̃X�v���C�g
    public Sprite Ball3;//�v���C���[2�Ŏg���ۂ̃X�v���C�g
    public Sprite Ball4;//�v���C���[2�Ŏg���ۂ̃X�v���C�g
    public float ScaleX;//�v���C���[�̃X�P�[��
    public float ScaleY;//�v���C���[�̃X�P�[��
    public float ScaleZ;//�v���C���[�̃X�P�[��
    public float BallScaleX;//�{�[���̃X�P�[��
    public float BallScaleY;//�{�[���̃X�P�[��
    public float BallScaleZ;//�{�[���̃X�P�[��
    public float LongScaleX;//�����l�p�̏ꍇ�̃X�P�[��
    public float LongScaleY;//�����l�p�̏ꍇ�̃X�P�[��
    public float LongScaleZ;//�����l�p�̏ꍇ�̃X�P�[��
    public bool Ice = false;//�M�~�b�N�̕X����
    public int PlayerNo;//�v���C���[�i���o�[

    public int Playerindex = 1;//�v���C���[�̌`�����ǂ̏�Ԃɂ���̂��������Ă���
    public int index = 1;

    private new Rigidbody2D rigidbody;
    PlayerManager PlMane;//�v���C���[�}�l�[�W���[�ƌĂ΂��I�u�W�F�N�g�̎Q��

    void Start()
    {
        PlayerManager = GameObject.Find("PlayerManager");
        PlMane = PlayerManager.GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();//�X�v���C�g�̃Z�b�g
        new Vector2(transform.position.x, transform.position.y);
        wallcht = GameObject.Find("Wallcht");//��I�u�W�F�N�g�̎Q��
        wch = wallcht.GetComponent<Wallcht>();
        wallcht2 = GameObject.FindWithTag("Wallcht");
        if(wallcht2 != null)
        {
            wall2 = wallcht2.GetComponent<Wall2>();
        }
    }


    void Update()
    {
        if (PlMane.Alive == true&&PlayerNo == 1)
        {
            float lsh = Input.GetAxis("Horizontal");
            if (transform.position.y <= -10)
            {
                PlMane.Alive = false;
            }
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//�v���C���[�̊p�x�����肵�Ă���
            if (Input.GetKey(KeyCode.D)||(lsh == 1))
            {
                prri = true;
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //�E�����̓��͏���
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A)||(lsh == -1))
            {
                prri = true;
                //�v���C���[��Y������]������
                RoteY = 180;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //�������̓��͏���
                rigidbody.velocity = new Vector2(-XSpeed, rigidbody.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if (Ice == false)
                {
                    //�����L�[��������Ă��Ȃ��ꍇ��X���̃x���V�e�B��0�ɕύX
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                else if (Playerindex == 2)
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                prri = false;
            }
            //�v���C���[�̎�ނ̕ύX����
            if (Input.GetKeyDown(KeyCode.Q)&&slope == false)
            {
                ++Playerindex;//�v���C���[�̃��[�h�̒l��ǉ�
                index = Playerindex;
                if (Playerindex == 4)//�͈͊O�̏ꍇ�ɔ͈͓��ɖ߂�
                {
                    Playerindex = 1;
                }
                Playerchange();//�X�v���C�g�̕ύX�����s
                index = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpUpdate();
            }
            switch (Playerindex)
            {
                case 1://�l��1�̏ꍇ�l�p�̏�����
                    {
                        Square();
                        break;
                    }
                case 2://�l��2�̏ꍇ�O�p�̏�����
                    {
                        Triangle();
                        break;
                    }
                case 3://�l��3�̏ꍇ�ۂ̏�����
                    {
                        Ball();
                        break;
                    }
            }
        }
        else//�v���C���[�����S���ɂ���p�̃X�v���C�g�ɕύX
        {
            if (Playerindex == 1)
            {
                if (Squareindex == 0)
                {
                    spriteRenderer.sprite = Square2;
                }
                else
                {
                    spriteRenderer.sprite = SquareLong2;
                }
            }
            else if (Playerindex == 2)
            {
                spriteRenderer.sprite = Triangle2;
            }
            else if (Playerindex == 3)
            {
                spriteRenderer.sprite = Ball2;
            }
        }
    if(PlMane.Player2 == null)
    {
        return;
    }
    else if(PlayerNo == 2)
    {
        if (PlMane.Alive == true)
        {
            float lsh = Input.GetAxis("Horizontal");
            if (transform.position.y <= -10)
            {
                PlMane.Alive = false;
            }
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//�v���C���[�̊p�x�����肵�Ă���
            if (Input.GetKey(KeyCode.RightArrow) || (lsh == 1))
            {
                prri = true;
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //�E�����̓��͏���
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || (lsh == -1))
            {
                prri = true;
                //�v���C���[��Y������]������
                RoteY = 180;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //�������̓��͏���
                rigidbody.velocity = new Vector2(-XSpeed, rigidbody.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if(Ice == false)
                {
                    //�����L�[��������Ă��Ȃ��ꍇ��X���̃x���V�e�B��0�ɕύX
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                else if(Playerindex == 2)
                    {
                        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                    }
                prri = false;
            }
            //�v���C���[�̎�ނ̕ύX����
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                ++Playerindex;//�v���C���[�̃��[�h�̒l��ǉ�
                index = Playerindex;
                if (Playerindex == 4)//�͈͊O�̏ꍇ�ɔ͈͓��ɖ߂�
                {
                    Playerindex = 1;
                }
                Playerchange();//�X�v���C�g�̕ύX�����s
                index = 0;
            }
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    JumpUpdate();
                }
                switch (Playerindex)
            {
                case 1://�l��1�̏ꍇ�l�p�̏�����
                    {
                        Square();
                        break;
                    }
                case 2://�l��2�̏ꍇ�O�p�̏�����
                    {
                        Triangle();
                        break;
                    }
                case 3://�l��3�̏ꍇ�ۂ̏�����
                    {
                        Ball();
                        break;
                    }
            }
        }
            else//�v���C���[�����S���ɂ���p�̃X�v���C�g�ɕύX
            {
                if (Playerindex == 1)
                {
                    if (Squareindex == 0)
                    {
                        spriteRenderer.sprite = Square4;
                    }
                    else
                    {
                        spriteRenderer.sprite = SquareLong4;
                    }
                }
                else if (Playerindex == 2)
                {
                    spriteRenderer.sprite = Triangle4;
                }
                else if (Playerindex == 3)
                {
                    spriteRenderer.sprite = Ball4;
                }
            }
        }
        sceneTitle();
    }
    private void FixedUpdate()
    {
        float lsh = Input.GetAxis("Horizontal");
        if (Playerindex == 3)
        {
            if(PlayerNo == 1)
            {
                transform.Rotate(new Vector3(0, 0, 0));
                if (Input.GetKey(KeyCode.D) || (lsh == 1))
                {
                    //�L�[�������ꂽ�Ƃ���Z������]������
                    RoteZ -= 10;
                }
                else if (Input.GetKey(KeyCode.A) || (lsh == -1))
                {
                    RoteZ -= 10;
                }

                if (SlopeLeft == true || Sloperight == true)
                {
                    if (XSpeed <= 19)
                    {
                        XSpeed += 1f;
                    }
                }
                else if (slope == true)
                {
                    XSpeed = 20;
                }
                else
                {
                    XSpeed = dfSpeed;//�v���C���[�̑��x������
                }
            }
            else if(PlayerNo == 2)
            {
                transform.Rotate(new Vector3(0, 0, 0));
                if (Input.GetKey(KeyCode.RightArrow) || (lsh == 1))
                {
                    //�L�[�������ꂽ�Ƃ���Z������]������
                    RoteZ -= 10;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || (lsh == -1))
                {
                    RoteZ -= 10;
                }

                if (SlopeLeft == true || Sloperight == true)
                {
                    if (XSpeed <= 19)
                    {
                        XSpeed += 1f;
                    }
                }
                else if (slope == true)
                {
                    XSpeed = 20;
                }
                else
                {
                    XSpeed = dfSpeed;//�v���C���[�̑��x������
                }
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlMane.Alive)
        {
            //�W�����v�̐���
            if (collision.gameObject.CompareTag("Ground") ||collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("sloperLeft") || collision.gameObject.CompareTag("sloperight") || collision.gameObject.CompareTag("slope") || collision.gameObject.CompareTag("Ice"))
            {
                JumpCount = 0;
            }
            if (Playerindex == 2)
            {
            }
            if (Playerindex == 3)
            {
                //�E������̍��]����
                if (collision.gameObject.CompareTag("sloperight"))
                {
                    rigidbody.gravityScale = 20;
                    Sloperight = true;
                }
                //������̍��]����
                else if (collision.gameObject.CompareTag("sloperLeft"))
                {
                    rigidbody.gravityScale = 20;
                    SlopeLeft = true;
                }
                else if (collision.gameObject.CompareTag("slope"))
                {
                    rigidbody.gravityScale = 20;
                    slope = true;
                }
                else
                {

                    rigidbody.gravityScale = 2;
                    Sloperight = false;
                    SlopeLeft = false;
                    slope = false;
                }
            }
            else
            {
                XSpeed = dfSpeed;
            }
            if (collision.gameObject.CompareTag("Ice"))
            {
                Ice = true;
            }
            else
            {
                Ice = false;
            }
            //�v���C���[�̎��S����
            if (collision.gameObject.CompareTag("Dead"))
            {
                    PlMane.Alive = false;
            }//��ɂ��������Ƃ��̔���
            if (collision.gameObject.CompareTag("Rock"))
            {
                if (Playerindex == 1 && wch.selection == 0)
                {
                    PlMane.Alive = true;
                }
                else if (Playerindex == 2 && wch.selection == 1)
                {
                    PlMane.Alive = true;
                }
                else if (Playerindex == 3 && wch.selection == 2)
                {
                    PlMane.Alive = true;
                }
                else if(Playerindex == 1&&wall2.selection == 0)
                {
                    PlMane.Alive = true;
                }
                else PlMane.Alive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("midlie"))//�X�̃^�O��ύX����
        {
            //���Ԓn�_�̃I�u�W�F�N�g�ɂӂꂽ�Ƃ��ɍ��W�ۑ����I���ɂ���
            PlMane.CheckPlayer = true;
            Debug.Log("CheckPlayer");
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            //�v���C���[�̃S�[������
            PlMane.CheckGool = true;
            SceneManeger.ReStart = false;
        }
    }

    void JumpUpdate()//�W�����v�̏���
    {
        //�Q�i�W�����v�֎~�I�I
        if (JumpCount == 0)
        {//�W�����v�����J�n
                rigidbody.gravityScale = 2;
                ++JumpCount;
                //�W�����v�̍�����ݒ�
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpCount == 0)
        {
            rigidbody.gravityScale = 2;
            ++JumpCount;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
        }
        if(Playerindex == 2) 
        {
            if (context.performed)
            {
                RoteZ += 45;
            }
        }
    }
    private void sceneTitle()//�ً}���̃V�[���ύX
    {
        //�����ꂽ�Ƃ��ɃV�[�����ړ�����
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }
    int Squareindex = 0;
    public void Square()
    {
        BoxCol = GetComponent<BoxCollider2D>();
        if (PlayerNo == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ++Squareindex;
                if (Squareindex == 2)
                {
                    Squareindex = 0;
                }
            }
            if (Squareindex == 0)//�l�p��Z���l�p�ւƕω�������
            {
                spriteRenderer.sprite = Square1;
                PlMane.Player1.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                BoxCol.size = new Vector2(0.8605145f, 0.8269166f);
            }
            else if (Squareindex == 1)//�l�p�������O�l�p�ɕω�������
            {
                if (PlMane.Alive)
                {
                    spriteRenderer.sprite = SquareLong1;
                    PlMane.Player1.transform.localScale = new Vector3(LongScaleX, LongScaleY, LongScaleZ);
                    BoxCol.size = new Vector2(36.432f, 7.289754f);
                }
                else if (PlMane.Alive == false)
                {
                    spriteRenderer.sprite = SquareLong2;
                }
            }
        }
        if (PlMane.Player2 != null)
        {
            if(PlayerNo == 2)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ++Squareindex;
                    if (Squareindex == 2)
                    {
                        Squareindex = 0;
                    }
                }
                if (Squareindex == 0)//�l�p��Z���l�p�ւƕω�������
                {
                    spriteRenderer.sprite = Square3;
                    PlMane.Player2.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                    BoxCol.size = new Vector2(0.8605145f, 0.8269166f);
                }
                else if (Squareindex == 1)//�l�p�������O�l�p�ɕω�������
                {
                    if (PlMane.Alive)
                    {
                        spriteRenderer.sprite = SquareLong3;
                        PlMane.Player2.transform.localScale = new Vector3(LongScaleX, LongScaleY, LongScaleZ);
                        BoxCol.size = new Vector2(36.432f, 7.289754f);
                    }
                    else if (PlMane.Alive == false)
                    {
                        spriteRenderer.sprite = SquareLong4;
                    }
                }
            }
            
        }
    }

    public void OnSquare(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Playerindex == 1)
            {
                ++Squareindex;
                if (Squareindex == 2)
                {
                    Squareindex = 0;
                }
                return;
            }
            else if(slope == false)
            {
                Playerindex = 1;
                index = Playerindex;
                Playerchange();//�X�v���C�g�̕ύX�����s
                index = 0;
            }

        }
        
    }

   public void Triangle()
    {
        if (PlMane.Alive)
            if(PlayerNo == 1)
            {
                //�L�[���������Ƃ�45����]����
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RoteZ += 45;
                }
            }
            else if(PlayerNo == 2)
            {
                //�L�[���������Ƃ�45����]����
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    RoteZ += 45;
                }
            }

    }
    public void OnTriangle(InputAction.CallbackContext context)
    {
        if(Playerindex == 2)
        {
            return;
        }
        else if(slope == false)
        {
            Playerindex = 2;
            index = Playerindex;
            Playerchange();//�X�v���C�g�̕ύX�����s
            index = 0;
        }
        
    }

   public void Ball()
    {
        //�{�[������ɂ��鎞�ɑ��x��ύX���āA�ړ�������B
        if (Sloperight == true)
        {
            if (XSpeed >= 19)
            {
                jumpPower = BallJumpPower;
            }
            if (prri == false)
            {
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
                if (JumpCount == 1)
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
            }
        }
        else if (SlopeLeft == true)
        {
            if (XSpeed >= 19)
            {
                jumpPower = BallJumpPower;
            }
            if (prri == false)
            {
                rigidbody.velocity = new Vector2(-XSpeed, rigidbody.velocity.y);
                if (JumpCount == 1)
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
            }
        }
        else if (slope == true)
        {
            rigidbody.gravityScale = 20;
            if (prri == false)
            {
                XSpeed = 1;
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
            }

        }
        else 
        {
            jumpPower = dfjumpPower;
        }
    }
    public void OnBall(InputAction.CallbackContext context)
    {
        if(Playerindex == 3)
        {
            return;
        }
        else if(slope == false)
        {
            Playerindex = 3;
            index = Playerindex;
            Playerchange();//�X�v���C�g�̕ύX�����s
            index = 0;
        }
        
    }

    //�v���C���[�̌`��ς��鏈��
    void Playerchange()
    {
        if(PlayerNo == 1)
        {
            if (index == 1 || index == 4)
            {
                GameObject.Destroy(PlMane.Player1.GetComponent<PolygonCollider2D>());
                GameObject.Destroy(PlMane.Player1.GetComponent<CircleCollider2D>());
                spriteRenderer.sprite = Square1;
                PlMane.Player1.AddComponent<BoxCollider2D>();
                Squareindex = 0;
                jumpPower = dfjumpPower;
                XSpeed = dfSpeed;//�v���C���[�̑��x������
                PlMane.Player1.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                RoteZ = 0;
            }
            if (index == 2)
            {
                GameObject.Destroy(PlMane.Player1.GetComponent<BoxCollider2D>());
                GameObject.Destroy(PlMane.Player1.GetComponent<CircleCollider2D>());
                PlMane.Player1.AddComponent<PolygonCollider2D>();
                spriteRenderer.sprite = Triangle1;
                PolCol = GetComponent<PolygonCollider2D>();
                PolCol.SetPath(0, newTriangle);
                jumpPower = dfjumpPower;
                XSpeed = dfSpeed;//�v���C���[�̑��x������
                PlMane.Player1.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                RoteZ = 0;
            }
            if (index == 3)
            {
                GameObject.Destroy(PlMane.Player1.GetComponent<BoxCollider2D>());
                GameObject.Destroy(PlMane.Player1.GetComponent<PolygonCollider2D>());
                PlMane.Player1.AddComponent<CircleCollider2D>();
                spriteRenderer.sprite = Ball1;
                CirCol = GetComponent<CircleCollider2D>();
                PlMane.Player1.transform.localScale = new Vector3(BallScaleX, BallScaleY, BallScaleZ);
                RoteZ = 0;
            } 
        }
        if (PlayerNo == 2)
        {
            if (index == 1 || index == 4)
            {
                GameObject.Destroy(PlMane.Player2.GetComponent<PolygonCollider2D>());
                GameObject.Destroy(PlMane.Player2.GetComponent<CircleCollider2D>());
                spriteRenderer.sprite = Square3;
                PlMane.Player2.AddComponent<BoxCollider2D>();
                Squareindex = 0;
                jumpPower = dfjumpPower;
                XSpeed = dfSpeed;//�v���C���[�̑��x������
                PlMane.Player2.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                RoteZ = 0;
            }
            if (index == 2)
            {
                GameObject.Destroy(PlMane.Player2.GetComponent<BoxCollider2D>());
                GameObject.Destroy(PlMane.Player2.GetComponent<CircleCollider2D>());
                PlMane.Player2.AddComponent<PolygonCollider2D>();
                spriteRenderer.sprite = Triangle2;
                PolCol = GetComponent<PolygonCollider2D>();
                PolCol.SetPath(0, newTriangle);
                jumpPower = dfjumpPower;
                XSpeed = dfSpeed;//�v���C���[�̑��x������
                PlMane.Player2.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                RoteZ = 0;
            }
            if (index == 3)
            {
                GameObject.Destroy(PlMane.Player2.GetComponent<BoxCollider2D>());
                GameObject.Destroy(PlMane.Player2.GetComponent<PolygonCollider2D>());
                PlMane.Player2.AddComponent<CircleCollider2D>();
                spriteRenderer.sprite = Ball2;
                CirCol = GetComponent<CircleCollider2D>();
                PlMane.Player2.transform.localScale = new Vector3(BallScaleX, BallScaleY, BallScaleZ);
                RoteZ = 0;
            }
        }
    }
}
