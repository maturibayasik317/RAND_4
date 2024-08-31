using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SquareController : MonoBehaviour
{
    private int index = 0;
    [SerializeField] public GameObject[] Player;//生成元となるオブジェクト
    public GameObject childObject;// 入れ替え先となるオブジェクト
    [SerializeField] GameObject Square;//操作するオブジェクト
    private Vector2 player;//プレイヤーの座標を保存する
    Spawn spawn;

    void Start()
    {
        spawn = GetComponent<Spawn>();
    }

    void Update()
    {

        childObject = spawn.gameObject;//
        Square = childObject;
        //指定のキーが押されたとき
        if (Input.GetKeyDown(KeyCode.E))
        {//四角の形であれば
            if (GameObject.Find("Player(Clone)") || GameObject.Find("Player1(Clone)"))
            {
                //プレイヤーの座標を保存
                player = Square.transform.position;
                Destroy(Square);//今あるオブジェクトを削除
                ++index;//カウントを追加
                if (index == Player.Length) { index = 0; }
                //新しいオブジェクトを前のオブジェクト座標に生成
                childObject = Instantiate(Player[index], player, Quaternion.identity);
            } 
        }
    }
}
