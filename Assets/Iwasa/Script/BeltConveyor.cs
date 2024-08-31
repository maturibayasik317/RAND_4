using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeltConveyor : MonoBehaviour
{
    public float conveyorSpeed = 2.0f; // �R���x�A�x���g�̑��x
    public bool moveRight = true; // �ړ������𐧌䂷��`�F�b�N�{�b�N�X

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D called.");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ItemBox"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �ړ����������肷��
                float direction = moveRight ? 1.0f : -1.0f;
                rb.velocity = new Vector2(conveyorSpeed * direction, rb.velocity.y);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �v���C���[��ItemBox�ɑ΂��ď������s��
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ItemBox"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �x���g�R���x�A�̕����ɑ��x��^����
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
                rb.velocity = Vector2.zero; // ���ꂽ�Ƃ��Ɉړ����~
            }
        }
    }
}
