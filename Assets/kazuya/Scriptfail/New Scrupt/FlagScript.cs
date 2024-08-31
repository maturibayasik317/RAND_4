using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;//オブジェクトのスプライトを変更させる

    public Sprite Flag_1;
    public Sprite Flag_2;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = Flag_2;
        }
    }
}
