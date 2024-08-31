using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_RightLeft : MonoBehaviour
{
    public float moveSpeed = 3f; // �ړ����x
    public float moveDistance = 5f; // �ړ�����
    public bool onlyMoveWithPlayer = false; // �v���C���[���G��Ă���Ƃ��݈̂ړ����邩�ǂ���
    public bool Leftscroll = false; // �������Ɉړ����邩�ǂ���

    private Vector3 startPosition; // �v���b�g�t�H�[���̏����ʒu
    private bool movingRight; // �v���b�g�t�H�[�����E�Ɉړ����Ă��邩�ǂ���
    private bool playerOnPlatform = false; // �v���C���[���v���b�g�t�H�[���ɐG��Ă��邩�ǂ���
    private Transform playerTransform; // �v���C���[��Transform
    private Vector3 lastPlatformPosition; // �O��̃t���[���ł̃v���b�g�t�H�[���̈ʒu

    void Start()
    {
        startPosition = transform.position; // �v���b�g�t�H�[���̏����ʒu��ۑ�
        movingRight = !Leftscroll; // ����������ݒ� (Leftscroll��false�Ȃ�E����)
        lastPlatformPosition = transform.position; // �����ʒu��O��̈ʒu�Ƃ��ĕۑ�
    }

    void Update()
    {
        // onlyMoveWithPlayer��true�̏ꍇ�A�v���C���[���G��Ă��Ȃ��Ɠ����Ȃ�
        if (onlyMoveWithPlayer && !playerOnPlatform)
        {
            lastPlatformPosition = transform.position; // �v���b�g�t�H�[���̈ʒu���X�V
            return; // �v���C���[�����Ȃ��ꍇ�A������Update���I��
        }

        // �v���b�g�t�H�[�����E�Ɉړ����Ă���ꍇ
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // �E�Ɉړ�
            if (transform.position.x >= startPosition.x + moveDistance) // �ړ��͈͂𒴂�����
            {
                movingRight = false; // ���Ɉړ�����悤�ɐݒ�
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // ���Ɉړ�
            if (transform.position.x <= startPosition.x - moveDistance) // �ړ��͈͂𒴂�����
            {
                movingRight = true; // �E�Ɉړ�����悤�ɐݒ�
            }
        }

        // �v���C���[���v���b�g�t�H�[����ɂ���ꍇ�A�v���C���[�̈ʒu���X�V
        if (playerOnPlatform && playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition; // �v���b�g�t�H�[���̓������v�Z
            playerTransform.position += platformMovement; // �v���C���[�̈ʒu���X�V
        }

        lastPlatformPosition = transform.position; // �v���b�g�t�H�[���̈ʒu���X�V
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ
        {
            playerOnPlatform = true; // �v���C���[���v���b�g�t�H�[����ɂ��邱�Ƃ��L�^
            playerTransform = collision.transform; // �v���C���[��Transform��ۑ�
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �Փ˂��I�������I�u�W�F�N�g���v���C���[�̏ꍇ
        {
            playerOnPlatform = false; // �v���C���[���v���b�g�t�H�[����ɂ��Ȃ����Ƃ��L�^
            playerTransform = null; // �v���C���[��Transform�����Z�b�g
        }
    }

    private void OnDrawGizmosSelected()
    {
        // �V�[���r���[�Ɉړ��͈͂�\��
        Gizmos.color = Color.green; // ���̐F��΂ɐݒ�
        Gizmos.DrawLine(startPosition - Vector3.right * moveDistance, startPosition + Vector3.right * moveDistance); // �ړ��͈͂�\���������`��
    }
}
