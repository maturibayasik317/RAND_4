using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;//�I�u�W�F�N�g�̃X�v���C�g��ύX������

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
