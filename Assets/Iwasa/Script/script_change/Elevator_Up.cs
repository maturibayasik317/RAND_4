using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Up : MonoBehaviour
{
    public string playerName = "Player"; // 上昇させるプレイヤー名
    public float liftSpeed = 2f; // 上昇速度

    // Square(), Triangle(), Ball() メソッドの状態を設定できる変数
    public bool canLiftSquare = true;
    public bool canLiftTriangle = true;
    public bool canLiftBall = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // プレイヤー名が一致し、かつ有効な形状メソッドが設定されているかを確認
        if (collision.gameObject.name.Contains(playerName) &&
            (canLiftSquare || canLiftTriangle || canLiftBall))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // プレイヤーを上昇させる
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, liftSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // プレイヤーがリフトから出たとき、上昇を停止させる
        if (collision.gameObject.name.Contains(playerName))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // プレイヤーの垂直速度をゼロに設定
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            }
        }
    }
}
