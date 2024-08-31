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
    [SerializeField]public float xSpeed;//プレイヤーの速度
    [SerializeField] public float jumpPower;//プレイヤーのジャンプの高さ
    [SerializeField]private float initialJumpPower;
    [SerializeField]public int JumpCount = 1;//ジャンプできる回数
    [SerializeField] public float PlayerObject;
    public bool prri = false;//プレイヤーが止まっているかを確認している
    private SpriteRenderer spriteRenderer;
    public int RoteY;//転がる角度の変数
    public int RoteZ;
    public int RoteX;
    [SerializeField] private Sprite newSprite;    // インスペクターからスプライトを受け取っておく
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

        //2段ジャンプ禁止 //タグを作ってジャンプを禁止にする
        // ジャンプ操作
        if (JumpCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {// ジャンプ開始
                ++JumpCount;
                // ジャンプ力を計算
                rigidbody2D.gravityScale = 2;
                // ジャンプ力を適用
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            }
        }
    }
    private void Update()
    {
        if (spawn.Alive)
        {
            jumpUpdeate();//ジャンプの処理
            if (transform.position.y <= -10)
            {
                spawn.Alive = false;
            }
            xSpeed = dfSpeed;
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//オブジェクトの回転の初期化
            if (Input.GetKey(KeyCode.D) )
            {
                prri = true;
                // 右方向の移動入力
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                rigidbody2D.velocity = new Vector2(xSpeed, rigidbody2D.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                prri = true;
                //左方向の移動入力
                RoteY = 180;//Y軸を回転させるプログラム
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                rigidbody2D.velocity = new Vector2(-xSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if (Ice == false)
                {
                    // 入力なし
                    // // X方向の移動を停止
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
            // スプライトを差し替える
            spriteRenderer.sprite = newSprite;
        }
        sceneTitle();//シーンの切り替え
    }
    private void sceneTitle()
    {
        //押されたときにシーンを移動する
        if (Input.GetKeyDown(KeyCode.P)&&Input.GetKey(KeyCode.RightShift))
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ジャンプの制御
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
        if (collision.gameObject.CompareTag("midlie"))//個々のタグを変更する
        {
            //中間地点のオブジェクトにふれたときに座標保存をオンにする
            spawn.CheckPlayer = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            //プレイヤーのゴール判定
            spawn.CheckGool = true;
            SceneManeger.ReStart = false;
        }
    }
}
