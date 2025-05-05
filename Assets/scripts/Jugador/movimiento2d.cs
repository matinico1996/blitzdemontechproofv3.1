using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class movimiento2d : MonoBehaviour
{
    public float velocidadMovimento;
    private Rigidbody2D fisica;
    public float FuerzSalto = 8.0f;

  

    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float movement = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(movement * velocidadMovimento, fisica.velocity.y);

       


       // if (Mathf.Abs(movement) > 0f)
        {
            //transform.rotation = movement < 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.identity;

            //esta parte es el problema?//
            //
            //if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.localScale = new Vector3(-1,1, 1);
                //dispara de lado//
            }
        }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(fisica.velocity.y) < 0.001f)
        {
            fisica.AddForce(new Vector2(0, FuerzSalto), ForceMode2D.Impulse);
        }
    }
}
