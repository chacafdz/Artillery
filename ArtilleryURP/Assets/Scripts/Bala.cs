using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject particulasExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            Invoke("Explotar", 3);
        }
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Objetivo")
        {
            Explotar();
        }
    }

    public void Explotar()
    {
        GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity) as GameObject;
        Cañon.Bloqueado = false;
        SeguirCamara.objetivo = null;
        Destroy(this.gameObject);
    }
}