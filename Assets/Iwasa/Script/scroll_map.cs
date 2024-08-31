using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_map : MonoBehaviour
{
    public float scrollSpeed = 2.0f; // スクロール速度

    void Update()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}
