using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    [SerializeField] public float dfSpeed;
    [SerializeField]public float xSpeed;//�v���C���[�̑��x
    [SerializeField] public float jumpPower;//�v���C���[�̃W�����v�̍���
    [SerializeField]private float initialJumpPower;
    [SerializeField]public int JumpCount = 1;//�W�����v�ł����
    [SerializeField] public float PlayerObject;
    public bool prri = false;//�v���C���[���~�܂��Ă��邩���m�F���Ă���
    private SpriteRenderer spriteRenderer;
    public int RoteY;//�]����p�x�̕ϐ�
    public int RoteZ;
    public int RoteX;
    [SerializeField] private Sprite newSprite;    // �C���X�y�N�^�[����X�v���C�g���󂯎���Ă���
    public bool Ice = false;
    public int GravitySensor { get; private set; }
    private GameObject characterChg;
    Spawn spawn;

    void Start()
    {
        characterChg = GameObject.Find("CharacterChg");
        spawn = characterChg.GetComponent<Spawn>();
        rigidbody2D =  GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
      new Vector2(transform.position.x,transform.position.y);
    }
    void jumpUpdeate()
    {

        //2�i�W�����v�֎~ //�^�O������ăW�����v���֎~�ɂ���
        // �W�����v����
        if (JumpCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {// �W�����v�J�n
                ++JumpCount;
                // �W�����v�͂��v�Z
                rigidbody2D.gravityScale = 2;
                // �W�����v�͂�K�p
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            }
        }
    }
    private void Update()
    {
        if (spawn.Alive)
        {
            jumpUpdeate();//�W�����v�̏���
            if (transform.position.y <= -10)
            {
                spawn.Alive = false;
            }
            xSpeed = dfSpeed;
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//�I�u�W�F�N�g�̉�]�̏�����
            if (Input.GetKey(KeyCode.D) )
            {
                prri = true;
                // �E�����̈ړ�����
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                rigidbody2D.velocity = new Vector2(xSpeed, rigidbody2D.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                prri = true;
                //�������̈ړ�����
                RoteY = 180;//Y������]������v���O����
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                rigidbody2D.velocity = new Vector2(-xSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if (Ice == false)
                {
                    // ���͂Ȃ�
                    // // X�����̈ړ����~
                    rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                }
                else if(spawn.index == 1)
                {
                    rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                }
                prri = false;
            }
        }
        else
        {
            // �X�v���C�g�������ւ���
            spriteRenderer.sprite = newSprite;
        }
        sceneTitle();//�V�[���̐؂�ւ�
    }
    private void sceneTitle()
    {
        //�����ꂽ�Ƃ��ɃV�[�����ړ�����
        if (Input.GetKeyDown(KeyCode.P)&&Input.GetKey(KeyCode.RightShift))
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�W�����v�̐���
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("sloperLeft")||collision.gameObject.CompareTag("sloperight")||collision.gameObject.CompareTag("slope")||collision.gameObject.CompareTag("Ice"))
        {
            JumpCount = 0;
        }
        //rigidbody2D.gravityScale = 10;
        if (collision.gameObject.CompareTag("Ice"))
        {
            Ice = true;
        }
        else
        {
            Ice = false;
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("midlie"))//�X�̃^�O��ύX����
        {
            //���Ԓn�_�̃I�u�W�F�N�g�ɂӂꂽ�Ƃ��ɍ��W�ۑ����I���ɂ���
            spawn.CheckPlayer = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            //�v���C���[�̃S�[������
            spawn.CheckGool = true;
            SceneManeger.ReStart = false;
        }
    }
}
