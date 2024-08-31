using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    public GameObject button;//ボタンオブジェクト
    public GameObject Wall;//壁オブジェクト


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
            Wall.SetActive(true);
            }
        
        
    }
}
