using UnityEngine;

public class FlameCollision : MonoBehaviour
{
    public string invincibility; // ���G�̃I�u�W�F�N�g�̖��O

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[�^�O�������Ă��邪���G�̖��O�ƈ�v����I�u�W�F�N�g�͔j�󂵂Ȃ�
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name != invincibility)
            {
                Destroy(other.gameObject);
                Debug.Log("Player�͉��ɂ��j��");
            }
            else
            {
                Debug.Log("���G�̃I�u�W�F�N�g�͔j�󂳂�Ȃ�");
            }
        }
    }
}
