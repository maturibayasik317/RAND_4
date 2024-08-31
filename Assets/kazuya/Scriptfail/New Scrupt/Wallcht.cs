using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wallcht : MonoBehaviour
{
    public  GameObject WallObject;
    public GameObject RockObject;
    public Vector2 dfRock;
    private bool PlaCh = false;
    public int selection = 0;
    void Start()
    {

    }

    void Update()
    {
        if (PlaCh)
        {
            Instantiate(RockObject, dfRock, Quaternion.identity);
            PlaCh = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(WallObject);    
            PlaCh = true;
        }

    }
}
