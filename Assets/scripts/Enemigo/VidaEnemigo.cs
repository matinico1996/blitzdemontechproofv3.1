using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    
    public int HP;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bolitadefuego")
        {
            HP--;

            if (HP <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
            Debug.Log("esta colision");
        }

    }
}
