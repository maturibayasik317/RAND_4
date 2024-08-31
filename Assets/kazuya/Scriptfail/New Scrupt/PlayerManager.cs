using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameObject PlayerObj;
    private GameObject PlayerObj2;

    public static Vector2 CheckPoint = Vector2.zero;//中間地点の座標を記録
    private float DeatTime;//プレイヤー消滅までのタイム
    public Vector2 dfPlayer = new Vector2();//デフォルトの生成位置

    public GameObject Player1;
    public GameObject Player2;
    public bool Alive = true;//生存判定
    public bool BallChec = false;//ボール状態のプレイヤーが坂を転がっているかのチェック
    public bool CheckPlayer = false;//プレイヤーがチェックポイントに触れてかの確認
    public bool CheckGool = false;//プレイヤーのゴール判定



    void Start()
    {
        if (Player2 == null)
        {
            //中間ポイントが設定されていたらその座標からスタート
            if (SceneManeger.ReStart == true)
            {
                if (CheckPoint != Vector2.zero)
                {
                    Player1 = Instantiate(Player1, CheckPoint, Quaternion.identity);
                }
                else
                {
                    Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                }

            }
            else
            {
                Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
            }
        }
        else
        {
            //中間ポイントが設定されていたらその座標からスタート
            if (SceneManeger.ReStart == true)
            {
                if (CheckPoint != Vector2.zero)//中間ポイントが設定されていない場合
                {
                    Player1 = Instantiate(Player1, CheckPoint, Quaternion.identity);
                    Player2 = Instantiate(Player2, CheckPoint, Quaternion.identity);
                }
                else//中間ポイントが設定されている場合、その地点から再開する
                {
                    Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                    Player2 = Instantiate(Player2, dfPlayer, Quaternion.identity);
                }

            }
            else//初期スポン地点
            {
                Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                Player2 = Instantiate(Player2, dfPlayer, Quaternion.identity);
            }
        }
        
    }

    void Update()
    {
        PlayerObj = Player1;
        PlayerObj2 = Player2;
        if (Alive)
        {
            if (CheckPlayer)
            {
                CheckPoint = Player1.transform.position;
                CheckPlayer = false;
            }
            if (CheckGool)
            {
                CheckPoint = Vector2.zero;
                CheckGool = false;
            }
        }
        else
        {
            //プレイヤーが死亡判定になったときに一定時間待って、
            ++DeatTime;
            if (DeatTime == 200.0f)
            {//プレイヤーを消してゲームオーバーシーンへ
                Destroy(Player1);//プレイヤーの削除
                Destroy(Player2);
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }
}
