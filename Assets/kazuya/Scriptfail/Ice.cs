using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var matarial = GetComponent<Rigidbody2D>().sharedMaterial;
        if (GameObject.Find("Player2(Clone)"))
        {
            matarial.friction = 1;
        }
        else
        {
            matarial.friction = 0;
        }
    }
}
