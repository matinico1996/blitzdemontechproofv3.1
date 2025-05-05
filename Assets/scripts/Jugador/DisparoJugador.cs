using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] GameObject bolitadefuego;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform personaje;
    private Vector3 objetivo;
    [SerializeField] private Camera camara;
    



    public void Awake()
    {
        
       firePoint = transform.Find("firePoint");

    }

    void Update()
    {

        // rotacion 360 del firePoint alrededor del personaje.Recomiendo no tocarlo
        objetivo = camara.ScreenToWorldPoint(Input.mousePosition);
        objetivo.z = 0;

        Vector2 direccion = objetivo - personaje.position;

        float anguloGrados = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        float distancia = Vector3.Distance(firePoint.position, personaje.position);

        firePoint.position = personaje.position + (Quaternion.Euler(0 , 0, anguloGrados) * Vector3.right * distancia);


        firePoint.rotation = Quaternion.Euler(0, 0, anguloGrados );
        





       

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("¡Disparo activado!");

            GameObject nuevaBolita = Instantiate(bolitadefuego, firePoint.position, firePoint.rotation);

            //nuevaBolita.GetComponent<movimientoDisparo>().SetDireccion(mirandoDerecha ? 1 : -1);

            Debug.Log("Bolita de fuego instanciada en: " + nuevaBolita.transform.position);
        }
    }
}