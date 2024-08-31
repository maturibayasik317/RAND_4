using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SceneField] public GameObject WarpObj;
    public bool WarpTrigger = false;//���[�v�̔������Ă��邩�̊m�F
    private int count = 0;
    public int selection;//���[�v�Q�[�g�̃��[�h�I��
    private GameObject Player;//���C���̃v���C���[
    private GameObject Player2;//��Q�̃v���C���[
    private GameObject Manager;//�v���C���[�}�l�[�W���[
    PlCor plCor;
    PlCor plCor2;
    PlayerManager PlMane;

    void Start()
    {
        Manager = GameObject.Find("PlayerManager");
        PlMane = Manager.GetComponent<PlayerManager>();
        Player = GameObject.Find("Player(Clone)");
        plCor = Player.GetComponent<PlCor>();
        if(PlMane.Player2 != null)
        {
            Debug.Log("Player2");
            Player2 = GameObject.Find("Player 2(Clone)");
            plCor2 = Player2.GetComponent<PlCor>();
        }
    }

    void Update()
    {
        if (WarpTrigger)//���[�v�̑ҋ@����
        {
            count++;
            if (count == 10)
            {
                WarpTrigger = false;
                count = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count == 0)
            {
                if (plCor.PlayerNo == 1)
                {
                    if (plCor.Playerindex == 1 && selection == 0)//�l�p
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    if (plCor.Playerindex == 2 && selection == 1)//�O�p
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    if (plCor.Playerindex == 3 && selection == 2)//��
                    {
                        collision.gameObject.transform.position = WarpObj.transform.position;
                        WarpTrigger = true;
                    }
                    
                }
                if(PlMane.Player2 != null)
                {
                    if (plCor2.PlayerNo == 2)//�}���`�v���C���̃v���C���[2�̏ꍇ
                    {
                        if (plCor2.Playerindex == 1 && selection == 4)//�l�p
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                        if (plCor2.Playerindex == 2 && selection == 5)//�O�p
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                        if (plCor2.Playerindex == 3 && selection == 6)//�ۂ̌`
                        {
                            collision.gameObject.transform.position = WarpObj.transform.position;
                            WarpTrigger = true;
                        }
                    }
                }
                
                if (selection == 3)//���ׂĂ̌`�̃v���C���[���΂�
                {
                    collision.gameObject.transform.position = WarpObj.transform.position;
                    WarpTrigger = true;
                }

            }
        }
    }
}
