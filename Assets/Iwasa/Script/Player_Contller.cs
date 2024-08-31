using UnityEngine;

public class Player_Contller : MonoBehaviour
{
    public float speed = 5.0f; // 移動速度
    private Rigidbody2D rb;
    private bool facingRight = true; // プレイヤーが右を向いているかどうか
    private float moveHorizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Flip();
    }

    void Move()
    {
        // 十字キーの水平方向の入力を取得
        float moveHorizontal = Input.GetAxis("Horizontal");

        // プレイヤーの速度を設定
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    void Flip()
    {
        if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
