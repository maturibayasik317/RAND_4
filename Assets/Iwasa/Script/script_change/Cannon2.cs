using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon2 : MonoBehaviour
{
    public float launchForce = 10f; // ���˗�
    public Vector2 launchDirection = new Vector2(1, 1); // ���˕���
    public float preparationTime = 1f; // ��������
    public float gravityRestoreTime = 0.5f; // �d�͂�߂��^�C�~���O
    public float enableControlDelay = 1f; //�v���C���[�̑����߂��^�C�~���O
    public string playerName; // �v���C���[�̖��O
    private bool isPlayerInCannon = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;

    public enum LaunchType { Square, Triangle, Ball } // ���ˑΏۂ��`
    public int selectedLaunchType; // Inspector�őI�����邽�߂̃t�B�[���h

    private const int SquareIndex = 1;
    private const int TriangleIndex = 2;
    private const int BallIndex = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == playerName)
        {
            PlCor playerScript = collision.gameObject.GetComponent<PlCor>();

            // �v���C���[�̏�Ԃ��m�F
            if (playerScript != null)
            {
                if ((playerScript.Playerindex == SquareIndex && selectedLaunchType == SquareIndex) ||
                    (playerScript.Playerindex == TriangleIndex && selectedLaunchType == TriangleIndex) ||
                    (playerScript.Playerindex == BallIndex && selectedLaunchType == BallIndex))
                {
                    // ��Ԃ���v�����ꍇ�̂ݏ����𑱍s
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

    //�v���C���[�̔��ˏ���

    private IEnumerator LaunchPlayer()
    {
        yield return new WaitForSeconds(preparationTime);
        if (isPlayerInCannon && playerRigidbody != null && playerSpriteRenderer != null)
        {
            // �����Ńv���C���[�̏�Ԃ��m�F
            PlCor playerScript = player.GetComponent<PlCor>();
            if (playerScript != null)
            {
                // �v���C���[�̏�Ԃɉ��������������s
                if (playerScript.Playerindex == SquareIndex && selectedLaunchType == SquareIndex)
                {
                    // �v���C���[���l�p��ԂŁA���˃^�C�v���l�p�̏ꍇ
                    LaunchPlayerWithForce();
                }
                else if (playerScript.Playerindex == TriangleIndex && selectedLaunchType == TriangleIndex)
                {
                    // �v���C���[���O�p��ԂŁA���˃^�C�v���O�p�̏ꍇ
                    LaunchPlayerWithForce();
                }
                else if (playerScript.Playerindex == BallIndex && selectedLaunchType == BallIndex)
                {
                    // �v���C���[���ۏ�ԂŁA���˃^�C�v���ۂ̏ꍇ
                    LaunchPlayerWithForce();
                }
                else
                {
                    // ��Ԃ���v���Ȃ��ꍇ�͔��˂ł��Ȃ�
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

        // �v���C���[����𕜊�������^�C�~���O���w�莞�Ԍ�ɐݒ�
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
        // Square�Ɋւ��鏈��
        Debug.Log("Square launched!");
    }

    private void Triangle()
    {
        // Triangle�Ɋւ��鏈��
        Debug.Log("Triangle launched!");
    }

    private void Ball()
    {
        // Ball�Ɋւ��鏈��
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

    //�v���C���[�̔��˕���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3)(launchDirection.normalized * launchForce);

        Gizmos.DrawLine(startPosition, endPosition);
        Gizmos.DrawSphere(endPosition, 0.2f);
    }
}
