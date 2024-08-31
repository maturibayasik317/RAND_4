using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameObject PlayerObj;
    private GameObject PlayerObj2;

    public static Vector2 CheckPoint = Vector2.zero;//���Ԓn�_�̍��W���L�^
    private float DeatTime;//�v���C���[���ł܂ł̃^�C��
    public Vector2 dfPlayer = new Vector2();//�f�t�H���g�̐����ʒu

    public GameObject Player1;
    public GameObject Player2;
    public bool Alive = true;//��������
    public bool BallChec = false;//�{�[����Ԃ̃v���C���[�����]�����Ă��邩�̃`�F�b�N
    public bool CheckPlayer = false;//�v���C���[���`�F�b�N�|�C���g�ɐG��Ă��̊m�F
    public bool CheckGool = false;//�v���C���[�̃S�[������



    void Start()
    {
        if (Player2 == null)
        {
            //���ԃ|�C���g���ݒ肳��Ă����炻�̍��W����X�^�[�g
            if (SceneManeger.ReStart == true)
            {
                if (CheckPoint != Vector2.zero)
                {
                    Player1 = Instantiate(Player1, CheckPoint, Quaternion.identity);
                }
                else
                {
                    Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                }

            }
            else
            {
                Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
            }
        }
        else
        {
            //���ԃ|�C���g���ݒ肳��Ă����炻�̍��W����X�^�[�g
            if (SceneManeger.ReStart == true)
            {
                if (CheckPoint != Vector2.zero)//���ԃ|�C���g���ݒ肳��Ă��Ȃ��ꍇ
                {
                    Player1 = Instantiate(Player1, CheckPoint, Quaternion.identity);
                    Player2 = Instantiate(Player2, CheckPoint, Quaternion.identity);
                }
                else//���ԃ|�C���g���ݒ肳��Ă���ꍇ�A���̒n�_����ĊJ����
                {
                    Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                    Player2 = Instantiate(Player2, dfPlayer, Quaternion.identity);
                }

            }
            else//�����X�|���n�_
            {
                Player1 = Instantiate(Player1, dfPlayer, Quaternion.identity);
                Player2 = Instantiate(Player2, dfPlayer, Quaternion.identity);
            }
        }
        
    }

    void Update()
    {
        PlayerObj = Player1;
        PlayerObj2 = Player2;
        if (Alive)
        {
            if (CheckPlayer)
            {
                CheckPoint = Player1.transform.position;
                CheckPlayer = false;
            }
            if (CheckGool)
            {
                CheckPoint = Vector2.zero;
                CheckGool = false;
            }
        }
        else
        {
            //�v���C���[�����S����ɂȂ����Ƃ��Ɉ�莞�ԑ҂��āA
            ++DeatTime;
            if (DeatTime == 200.0f)
            {//�v���C���[�������ăQ�[���I�[�o�[�V�[����
                Destroy(Player1);//�v���C���[�̍폜
                Destroy(Player2);
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }
}
