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
    private SpriteRenderer spriteRenderer;//プレイヤーのスプライトを変えるため

    private Vector2 Player;//プレイヤーの現在の座標
    private bool prri = false;//プレイヤーが動いているかどうかを確認する
    private int RoteX;//プレイーの回転の角度を決める
    private int RoteY;
    private int RoteZ;
    private GameObject PlayerManager;
    private BoxCollider2D BoxCol;//四角のコライダー
    private PolygonCollider2D PolCol;//三角のコライダー
    private CircleCollider2D CirCol;//丸のコライダー
    //三角形の頂点の座標
    Vector2[] newTriangle = new Vector2[]
    {
        new Vector2(0.001981445f,0.5127149f),
        new Vector2(-0.5187848f,-0.4691465f),
        new Vector2(0.4776618f,-0.4550597f)
    };
    private bool Sloperight = false;//右下り坂
    private bool SlopeLeft = false;//左下り坂
    private bool slope = false;
    private GameObject wallcht;
    private GameObject wallcht2;
    Wallcht wch;
    Wall2 wall2;


    [SerializeField] public float dfSpeed;//プレイヤーのデフォルトの速度
    [SerializeField] public float XSpeed;
    public float dfjumpPower;//デフォルトジャンプパワー
    public float BallJumpPower;
    [SerializeField] public float jumpPower;//プレイヤーのジャンプ力
    [SerializeField] public int JumpCount = 1;//ジャンプできる回数
    public Sprite Square1;//プレイヤーで使う四角のスプライト
    public Sprite Square2;//プレイヤーで使う四角のスプライト
    public Sprite SquareLong1;//プレイヤーの長い四角のスプライト
    public Sprite SquareLong2;//プレイヤーの長い四角のスプライト
    public Sprite Triangle1;//プレイヤーで使う三角のスプライト
    public Sprite Triangle2;//プレイヤーで使う三角のスプライト
    public Sprite Ball1;//プレイヤーで使う丸のスプライト
    public Sprite Ball2;//プレイヤーで使う丸のスプライト
    public Sprite Square3;//プレイヤー2で使う四角のスプライト
    public Sprite Square4;//プレイヤー2で使う四角のスプライト
    public Sprite SquareLong3;//プレイヤー2の長い四角のスプライト
    public Sprite SquareLong4;//プレイヤー2の長い四角のスプライト
    public Sprite Triangle3;//プレイヤー2で使う三角のスプライト
    public Sprite Triangle4;//プレイヤー2で使う三角のスプライト
    public Sprite Ball3;//プレイヤー2で使う丸のスプライト
    public Sprite Ball4;//プレイヤー2で使う丸のスプライト
    public float ScaleX;//プレイヤーのスケール
    public float ScaleY;//プレイヤーのスケール
    public float ScaleZ;//プレイヤーのスケール
    public float BallScaleX;//ボールのスケール
    public float BallScaleY;//ボールのスケール
    public float BallScaleZ;//ボールのスケール
    public float LongScaleX;//長い四角の場合のスケール
    public float LongScaleY;//長い四角の場合のスケール
    public float LongScaleZ;//長い四角の場合のスケール
    public bool Ice = false;//ギミックの氷判定
    public int PlayerNo;//プレイヤーナンバー

    public int Playerindex = 1;//プレイヤーの形が今どの状態にあるのかを示している
    public int index = 1;

    private new Rigidbody2D rigidbody;
    PlayerManager PlMane;//プレイヤーマネージャーと呼ばれるオブジェクトの参照

    void Start()
    {
        PlayerManager = GameObject.Find("PlayerManager");
        PlMane = PlayerManager.GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();//スプライトのセット
        new Vector2(transform.position.x, transform.position.y);
        wallcht = GameObject.Find("Wallcht");//岩オブジェクトの参照
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
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//プレイヤーの角度を決定している
            if (Input.GetKey(KeyCode.D)||(lsh == 1))
            {
                prri = true;
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //右方向の入力処理
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A)||(lsh == -1))
            {
                prri = true;
                //プレイヤーのY軸を回転させる
                RoteY = 180;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //左方向の入力処理
                rigidbody.velocity = new Vector2(-XSpeed, rigidbody.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if (Ice == false)
                {
                    //方向キーが押されていない場合にX軸のベロシティを0に変更
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                else if (Playerindex == 2)
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                prri = false;
            }
            //プレイヤーの種類の変更処理
            if (Input.GetKeyDown(KeyCode.Q)&&slope == false)
            {
                ++Playerindex;//プレイヤーのモードの値を追加
                index = Playerindex;
                if (Playerindex == 4)//範囲外の場合に範囲内に戻す
                {
                    Playerindex = 1;
                }
                Playerchange();//スプライトの変更を実行
                index = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpUpdate();
            }
            switch (Playerindex)
            {
                case 1://値が1の場合四角の処理へ
                    {
                        Square();
                        break;
                    }
                case 2://値が2の場合三角の処理へ
                    {
                        Triangle();
                        break;
                    }
                case 3://値が3の場合丸の処理へ
                    {
                        Ball();
                        break;
                    }
            }
        }
        else//プレイヤーが死亡時にそれ用のスプライトに変更
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
            transform.Rotate(new Vector3(RoteX, RoteY, RoteZ));//プレイヤーの角度を決定している
            if (Input.GetKey(KeyCode.RightArrow) || (lsh == 1))
            {
                prri = true;
                RoteY = 0;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //右方向の入力処理
                rigidbody.velocity = new Vector2(XSpeed, rigidbody.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || (lsh == -1))
            {
                prri = true;
                //プレイヤーのY軸を回転させる
                RoteY = 180;
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                //左方向の入力処理
                rigidbody.velocity = new Vector2(-XSpeed, rigidbody.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(RoteX, RoteY, RoteZ);
                if(Ice == false)
                {
                    //方向キーが押されていない場合にX軸のベロシティを0に変更
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                else if(Playerindex == 2)
                    {
                        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                    }
                prri = false;
            }
            //プレイヤーの種類の変更処理
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                ++Playerindex;//プレイヤーのモードの値を追加
                index = Playerindex;
                if (Playerindex == 4)//範囲外の場合に範囲内に戻す
                {
                    Playerindex = 1;
                }
                Playerchange();//スプライトの変更を実行
                index = 0;
            }
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    JumpUpdate();
                }
                switch (Playerindex)
            {
                case 1://値が1の場合四角の処理へ
                    {
                        Square();
                        break;
                    }
                case 2://値が2の場合三角の処理へ
                    {
                        Triangle();
                        break;
                    }
                case 3://値が3の場合丸の処理へ
                    {
                        Ball();
                        break;
                    }
            }
        }
            else//プレイヤーが死亡時にそれ用のスプライトに変更
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
                    //キーが押されたときにZ軸を回転させる
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
                    XSpeed = dfSpeed;//プレイヤーの速度を決定
                }
            }
            else if(PlayerNo == 2)
            {
                transform.Rotate(new Vector3(0, 0, 0));
                if (Input.GetKey(KeyCode.RightArrow) || (lsh == 1))
                {
                    //キーが押されたときにZ軸を回転させる
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
                    XSpeed = dfSpeed;//プレイヤーの速度を決定
                }
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlMane.Alive)
        {
            //ジャンプの制御
            if (collision.gameObject.CompareTag("Ground") ||collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("sloperLeft") || collision.gameObject.CompareTag("sloperight") || collision.gameObject.CompareTag("slope") || collision.gameObject.CompareTag("Ice"))
            {
                JumpCount = 0;
            }
            if (Playerindex == 2)
            {
            }
            if (Playerindex == 3)
            {
                //右下がりの坂を転がる
                if (collision.gameObject.CompareTag("sloperight"))
                {
                    rigidbody.gravityScale = 20;
                    Sloperight = true;
                }
                //左下りの坂を転がる
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
            //プレイヤーの死亡判定
            if (collision.gameObject.CompareTag("Dead"))
            {
                    PlMane.Alive = false;
            }//岩にあたったときの判定
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
        if (collision.gameObject.CompareTag("midlie"))//個々のタグを変更する
        {
            //中間地点のオブジェクトにふれたときに座標保存をオンにする
            PlMane.CheckPlayer = true;
            Debug.Log("CheckPlayer");
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            //プレイヤーのゴール判定
            PlMane.CheckGool = true;
            SceneManeger.ReStart = false;
        }
    }

    void JumpUpdate()//ジャンプの処理
    {
        //２段ジャンプ禁止！！
        if (JumpCount == 0)
        {//ジャンプ処理開始
                rigidbody.gravityScale = 2;
                ++JumpCount;
                //ジャンプの高さを設定
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
    private void sceneTitle()//緊急時のシーン変更
    {
        //押されたときにシーンを移動する
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
            if (Squareindex == 0)//四角を短い四角へと変化させる
            {
                spriteRenderer.sprite = Square1;
                PlMane.Player1.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                BoxCol.size = new Vector2(0.8605145f, 0.8269166f);
            }
            else if (Squareindex == 1)//四角をロング四角に変化させる
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
                if (Squareindex == 0)//四角を短い四角へと変化させる
                {
                    spriteRenderer.sprite = Square3;
                    PlMane.Player2.transform.localScale = new Vector3(ScaleX, ScaleY, ScaleZ);
                    BoxCol.size = new Vector2(0.8605145f, 0.8269166f);
                }
                else if (Squareindex == 1)//四角をロング四角に変化させる
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
                Playerchange();//スプライトの変更を実行
                index = 0;
            }

        }
        
    }

   public void Triangle()
    {
        if (PlMane.Alive)
            if(PlayerNo == 1)
            {
                //キーを押すごとに45°回転する
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RoteZ += 45;
                }
            }
            else if(PlayerNo == 2)
            {
                //キーを押すごとに45°回転する
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
            Playerchange();//スプライトの変更を実行
            index = 0;
        }
        
    }

   public void Ball()
    {
        //ボールが坂にある時に速度を変更して、移動させる。
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
            Playerchange();//スプライトの変更を実行
            index = 0;
        }
        
    }

    //プレイヤーの形を変える処理
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
                XSpeed = dfSpeed;//プレイヤーの速度を決定
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
                XSpeed = dfSpeed;//プレイヤーの速度を決定
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
                XSpeed = dfSpeed;//プレイヤーの速度を決定
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
                XSpeed = dfSpeed;//プレイヤーの速度を決定
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
