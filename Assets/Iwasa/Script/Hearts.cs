using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public Heart_catch heartCatchScript; // Heart_catch スクリプトへの参照

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player" )
        {
            // Heart_catch スクリプトのメソッドを呼び出してイラストを更新
            if (heartCatchScript != null)
            {
                heartCatchScript.CollectHeart();
            }
            // ハートオブジェクトを破壊
            Destroy(gameObject);
        }
    }
}
