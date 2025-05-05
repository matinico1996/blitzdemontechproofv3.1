using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoBolita : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public float velocidad = 10f;
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

   
}
