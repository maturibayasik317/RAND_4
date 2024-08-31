using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    public GameObject WallObject;
    public GameObject RockObject;
    public Vector2 dfRock;
    private bool PlaCh = false;
    public int selection;
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
