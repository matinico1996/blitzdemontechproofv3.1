using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoDisparo : MonoBehaviour
{
    [SerializeField]
    float velocidad = 10f;

    int direccion = 1;

    public void SetDireccion(int dir)
    {
        direccion = dir;
        if (dir == -1)
        {
            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * velocidad * direccion);
    }
}
