using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon2 : MonoBehaviour
{
    public float launchForce = 10f; // 発射力
    public Vector2 launchDirection = new Vector2(1, 1); // 発射方向
    public float preparationTime = 1f; // 準備時間
    public float gravityRestoreTime = 0.5f; // 重力を戻すタイミング
    public float enableControlDelay = 1f; //プレイヤーの操作を戻すタイミング
    public string playerName; // プレイヤーの名前
    private bool isPlayerInCannon = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;

    public enum LaunchType { Square, Triangle, Ball } // 発射対象を定義
    public int selectedLaunchType; // Inspectorで選択するためのフィールド

    private const int SquareIndex = 1;
    private const int TriangleIndex = 2;
    private const int BallIndex = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == playerName)
        {
            PlCor playerScript = collision.gameObject.GetComponent<PlCor>();

            // プレイヤーの状態を確認
            if (playerScript != null)
            {
                if ((playerScript.Playerindex == SquareIndex && selectedLaunchType == SquareIndex) ||
                    (playerScript.Playerindex == TriangleIndex && selectedLaunchType == TriangleIndex) ||
                    (playerScript.Playerindex == BallIndex && selectedLaunchType == BallIndex))
                {
                    // 状態が一致した場合のみ処理を続行
                    collision.gameObject.GetComponent<PlCor>().enabled = false;

                    player = collision.gameObject;
                    playerRigidbody = player.GetComponent<Rigidbody2D>();
                    playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

                    if (playerRigidbody != null && playerSpriteRenderer != null)
                    {
                        isPlayerInCannon = true;
                        playerRigidbody.velocity = Vector2.zero;
                        playerRigidbody.isKinematic = true;
                        playerSpriteRenderer.enabled = false;
                        StartCoroutine(LaunchPlayer());
                    }
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == playerName)
        {
            isPlayerInCannon = false;
            StopCoroutine(LaunchPlayer());
        }
    }

    //プレイヤーの発射処理

    private IEnumerator LaunchPlayer()
    {
        yield return new WaitForSeconds(preparationTime);
        if (isPlayerInCannon && playerRigidbody != null && playerSpriteRenderer != null)
        {
            // ここでプレイヤーの状態を確認
            PlCor playerScript = player.GetComponent<PlCor>();
            if (playerScript != null)
            {
                // プレイヤーの状態に応じた処理を実行
                if (playerScript.Playerindex == SquareIndex && selectedLaunchType == SquareIndex)
                {
                    // プレイヤーが四角状態で、発射タイプが四角の場合
                    LaunchPlayerWithForce();
                }
                else if (playerScript.Playerindex == TriangleIndex && selectedLaunchType == TriangleIndex)
                {
                    // プレイヤーが三角状態で、発射タイプが三角の場合
                    LaunchPlayerWithForce();
                }
                else if (playerScript.Playerindex == BallIndex && selectedLaunchType == BallIndex)
                {
                    // プレイヤーが丸状態で、発射タイプが丸の場合
                    LaunchPlayerWithForce();
                }
                else
                {
                    // 状態が一致しない場合は発射できない
                    Debug.Log($"Launch type ({selectedLaunchType}) does not match player's state ({playerScript.Playerindex}). No launch.");
                }
            }
        }
    }

    private void LaunchPlayerWithForce()
    {
        playerRigidbody.isKinematic = false;
        playerSpriteRenderer.enabled = true;

        Vector2 force = launchDirection.normalized * launchForce;
        playerRigidbody.AddForce(force, ForceMode2D.Impulse);

        playerRigidbody.gravityScale = 0;

        StartCoroutine(RestoreGravity());

        // プレイヤー操作を復活させるタイミングを指定時間後に設定
        StartCoroutine(EnablePlayerControlAfterDelay(enableControlDelay));
    }

    private IEnumerator EnablePlayerControlAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<PlCor>().enabled = true;
    }



    private IEnumerator RestoreGravity()
    {
        yield return new WaitForSeconds(gravityRestoreTime);
        playerRigidbody.gravityScale = 1;
    }

    private void Square()
    {
        // Squareに関する処理
        Debug.Log("Square launched!");
    }

    private void Triangle()
    {
        // Triangleに関する処理
        Debug.Log("Triangle launched!");
    }

    private void Ball()
    {
        // Ballに関する処理
        Debug.Log("Ball launched!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
            }
        }
    }

    //プレイヤーの発射方向
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3)(launchDirection.normalized * launchForce);

        Gizmos.DrawLine(startPosition, endPosition);
        Gizmos.DrawSphere(endPosition, 0.2f);
    }
}
