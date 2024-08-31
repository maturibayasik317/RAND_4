using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeltConveyor : MonoBehaviour
{
    public float conveyorSpeed = 2.0f; // コンベアベルトの速度
    public bool moveRight = true; // 移動方向を制御するチェックボックス

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D called.");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ItemBox"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 移動方向を決定する
                float direction = moveRight ? 1.0f : -1.0f;
                rb.velocity = new Vector2(conveyorSpeed * direction, rb.velocity.y);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // プレイヤーかItemBoxに対して処理を行う
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ItemBox"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ベルトコンベアの方向に速度を与える
                Vector2 movement = transform.right * conveyorSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + movement);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D called.");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ItemBox"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 離れたときに移動を停止
            }
        }
    }
}
