using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    public GameObject button;//�{�^���I�u�W�F�N�g
    public GameObject Wall;//�ǃI�u�W�F�N�g


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
            Wall.SetActive(true);
            }
        
        
    }
}
