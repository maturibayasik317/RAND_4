using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCheck : MonoBehaviour
{
    //壁とボタン系
    public GameObject button ;//ボタンオブジェクト
    public GameObject wall; // 壁のオブジェクト

    private void OnTriggerEnter2D(Collider2D other)//ボタンと壁について
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject,1.0f);
            Destroy(wall, 1.0f);//壁を破壊
            Debug.Log("壁破壊");
        }
    }
}
