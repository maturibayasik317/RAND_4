using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SceneField] public GameObject WarpObj;
    public bool WarpTrigger = false;//ワープの発動しているかの確認
    private int count = 0;
    public int selection;//ワープゲートのモード選択
    private GameObject Player;//メインのプレイヤー
    private GameObject Player2;//第２のプレイヤー
    private GameObject Manager;//プレイヤーマネージャー
    PlCor plCor;
    PlCor plCor2;
    PlayerManager PlMane;

    void Start()
    {
        Manager = GameObject.Find("PlayerManager");
        PlMane = Manager.GetComponent<PlayerManager>();
        Player = GameObject.Find("Player(Clone)");
        plCor = Player.GetComponent<PlCor>();
        if(PlMane.Player2 != null)
        {
            Debug.Log("Player2");
            Player2 = GameObject.Find("Player 2(Clone)");
            plCor2 = Player2.GetComponent<PlCor>();
        }
    }

    void Update()
    {
        if (WarpTrigger)//ワープの待機時間
        {
            count++;
            if (count == 10)
            {
                WarpTrigger = false;
                count = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count == 0)
            {
                if (plCor.PlayerNo == 1)
                {
                    if (plCor.Playerindex == 1 && selection == 0)//四角
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    if (plCor.Playerindex == 2 && selection == 1)//三角
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    if (plCor.Playerindex == 3 && selection == 2)//丸
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    
                }
                if(PlMane.Player2 != null)
                {
                    if (plCor2.PlayerNo == 2)//マルチプレイ時のプレイヤー2の場合
                    {
                        if (plCor2.Playerindex == 1 && selection == 4)//四角
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                        if (plCor2.Playerindex == 2 && selection == 5)//三角
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                        if (plCor2.Playerindex == 3 && selection == 6)//丸の形
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                    }
                }
                
                if (selection == 3)//すべての形のプレイヤーを飛ばす
                {
                    collision.gameObject.transform.position = WarpObj.transform.position;
                    WarpTrigger = true;
                }

            }
        }
    }
}
