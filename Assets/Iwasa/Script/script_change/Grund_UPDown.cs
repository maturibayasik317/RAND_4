using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_UPDown : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
    public float moveDistance = 5f; // 移動距離
    public bool onlyMoveWithPlayer = false; // プレイヤーが触れているときのみ移動するかどうか

    private Vector3 startPosition; // プラットフォームの初期位置
    private bool movingUp = true; // プラットフォームが上に移動しているかどうか
    private bool playerOnPlatform = false; // プレイヤーがプラットフォームに触れているかどうか
    private Transform playerTransform; // プレイヤーのTransform
    private Vector3 lastPlatformPosition; // 前回のフレームでのプラットフォームの位置

    void Start()
    {
        startPosition = transform.position; // プラットフォームの初期位置を保存
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

        // プラットフォームが上に移動している場合
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // 上に移動
            if (transform.position.y >= startPosition.y + moveDistance) // 移動範囲を超えたら
            {
                movingUp = false; // 下に移動するように設定
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); // 下に移動
            if (transform.position.y <= startPosition.y - moveDistance) // 移動範囲を超えたら
            {
                movingUp = true; // 上に移動するように設定
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
        Gizmos.DrawLine(startPosition - Vector3.up * moveDistance, startPosition + Vector3.up * moveDistance); // 移動範囲を表示する線を描画
    }
}
