using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_RightLeft : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
    public float moveDistance = 5f; // 移動距離
    public bool onlyMoveWithPlayer = false; // プレイヤーが触れているときのみ移動するかどうか
    public bool Leftscroll = false; // 左方向に移動するかどうか

    private Vector3 startPosition; // プラットフォームの初期位置
    private bool movingRight; // プラットフォームが右に移動しているかどうか
    private bool playerOnPlatform = false; // プレイヤーがプラットフォームに触れているかどうか
    private Transform playerTransform; // プレイヤーのTransform
    private Vector3 lastPlatformPosition; // 前回のフレームでのプラットフォームの位置

    void Start()
    {
        startPosition = transform.position; // プラットフォームの初期位置を保存
        movingRight = !Leftscroll; // 初期方向を設定 (Leftscrollがfalseなら右方向)
        lastPlatformPosition = transform.position; // 初期位置を前回の位置として保存
    }

    void Update()
    {
        // onlyMoveWithPlayerがtrueの場合、プレイヤーが触れていないと動かない
        if (onlyMoveWithPlayer && !playerOnPlatform)
        {
            lastPlatformPosition = transform.position; // プラットフォームの位置を更新
            return; // プレイヤーがいない場合、ここでUpdateを終了
        }

        // プラットフォームが右に移動している場合
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // 右に移動
            if (transform.position.x >= startPosition.x + moveDistance) // 移動範囲を超えたら
            {
                movingRight = false; // 左に移動するように設定
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // 左に移動
            if (transform.position.x <= startPosition.x - moveDistance) // 移動範囲を超えたら
            {
                movingRight = true; // 右に移動するように設定
            }
        }

        // プレイヤーがプラットフォーム上にいる場合、プレイヤーの位置を更新
        if (playerOnPlatform && playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition; // プラットフォームの動きを計算
            playerTransform.position += platformMovement; // プレイヤーの位置を更新
        }

        lastPlatformPosition = transform.position; // プラットフォームの位置を更新
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 衝突したオブジェクトがプレイヤーの場合
        {
            playerOnPlatform = true; // プレイヤーがプラットフォーム上にいることを記録
            playerTransform = collision.transform; // プレイヤーのTransformを保存
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 衝突が終了したオブジェクトがプレイヤーの場合
        {
            playerOnPlatform = false; // プレイヤーがプラットフォーム上にいないことを記録
            playerTransform = null; // プレイヤーのTransformをリセット
        }
    }

    private void OnDrawGizmosSelected()
    {
        // シーンビューに移動範囲を表示
        Gizmos.color = Color.green; // 線の色を緑に設定
        Gizmos.DrawLine(startPosition - Vector3.right * moveDistance, startPosition + Vector3.right * moveDistance); // 移動範囲を表示する線を描画
    }
}
