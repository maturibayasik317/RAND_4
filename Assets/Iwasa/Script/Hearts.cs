using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public Heart_catch heartCatchScript; // Heart_catch �X�N���v�g�ւ̎Q��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player" )
        {
            // Heart_catch �X�N���v�g�̃��\�b�h���Ăяo���ăC���X�g���X�V
            if (heartCatchScript != null)
            {
                heartCatchScript.CollectHeart();
            }
            // �n�[�g�I�u�W�F�N�g��j��
            Destroy(gameObject);
        }
    }
}
