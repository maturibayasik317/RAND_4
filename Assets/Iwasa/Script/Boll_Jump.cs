using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boll_Jump : MonoBehaviour
{
    public float jumpMultiplier = 1f; // ジャンプ力の上昇倍率
    private float originalJumpPower; // 元のジャンプ力を保存

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player3")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                originalJumpPower = playerController.xSpeed; // 元のジャンプ力を保存
                playerController.xSpeed *= jumpMultiplier; // ジャンプ力を上げる
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player3")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.xSpeed = originalJumpPower; // ジャンプ力を元に戻す
            }
        }
    }
}
