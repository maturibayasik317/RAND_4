using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Up : MonoBehaviour
{
    public string playerName = "Player"; // �㏸������v���C���[��
    public float liftSpeed = 2f; // �㏸���x

    // Square(), Triangle(), Ball() ���\�b�h�̏�Ԃ�ݒ�ł���ϐ�
    public bool canLiftSquare = true;
    public bool canLiftTriangle = true;
    public bool canLiftBall = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // �v���C���[������v���A���L���Ȍ`�󃁃\�b�h���ݒ肳��Ă��邩���m�F
        if (collision.gameObject.name.Contains(playerName) &&
            (canLiftSquare || canLiftTriangle || canLiftBall))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // �v���C���[���㏸������
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, liftSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �v���C���[�����t�g����o���Ƃ��A�㏸���~������
        if (collision.gameObject.name.Contains(playerName))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // �v���C���[�̐������x���[���ɐݒ�
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            }
        }
    }
}
