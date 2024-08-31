using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_UPDown : MonoBehaviour
{
    public float moveSpeed = 3f; // �ړ����x
    public float moveDistance = 5f; // �ړ�����
    public bool onlyMoveWithPlayer = false; // �v���C���[���G��Ă���Ƃ��݈̂ړ����邩�ǂ���

    private Vector3 startPosition; // �v���b�g�t�H�[���̏����ʒu
    private bool movingUp = true; // �v���b�g�t�H�[������Ɉړ����Ă��邩�ǂ���
    private bool playerOnPlatform = false; // �v���C���[���v���b�g�t�H�[���ɐG��Ă��邩�ǂ���
    private Transform playerTransform; // �v���C���[��Transform
    private Vector3 lastPlatformPosition; // �O��̃t���[���ł̃v���b�g�t�H�[���̈ʒu

    void Start()
    {
        startPosition = transform.position; // �v���b�g�t�H�[���̏����ʒu��ۑ�
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

        // �v���b�g�t�H�[������Ɉړ����Ă���ꍇ
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // ��Ɉړ�
            if (transform.position.y >= startPosition.y + moveDistance) // �ړ��͈͂𒴂�����
            {
                movingUp = false; // ���Ɉړ�����悤�ɐݒ�
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); // ���Ɉړ�
            if (transform.position.y <= startPosition.y - moveDistance) // �ړ��͈͂𒴂�����
            {
                movingUp = true; // ��Ɉړ�����悤�ɐݒ�
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
        Gizmos.DrawLine(startPosition - Vector3.up * moveDistance, startPosition + Vector3.up * moveDistance); // �ړ��͈͂�\���������`��
    }
}
