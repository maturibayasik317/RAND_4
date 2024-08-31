using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Down : MonoBehaviour
{
    public string playerName = "Player"; // â∫ç~Ç≥ÇπÇÈÉvÉåÉCÉÑÅ[ñº
    public float descendSpeed = 2f; // â∫ç~ë¨ìx

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Object entered trigger: " + collision.gameObject.name);

        if (collision.gameObject.name.Contains(playerName))
        {
            Debug.Log("Player detected: " + collision.gameObject.name);

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Debug.Log("Applying descend speed: " + descendSpeed);
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -descendSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Object exited trigger: " + collision.gameObject.name);

        if (collision.gameObject.name.Contains(playerName))
        {
            Debug.Log("Player exit detected: " + collision.gameObject.name);

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Debug.Log("Stopping descend");
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            }
        }
    }
}
