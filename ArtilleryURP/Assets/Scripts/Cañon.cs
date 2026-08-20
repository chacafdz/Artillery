using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cañon : MonoBehaviour
{
    [SerializeField] private GameObject BalaPrefab;
    private GameObject puntaCanon;
    private float rotacion;

    private void Start()
    {
        Transform encontrado = BuscarHijoRecursivo(transform, "PuntaCanon");
        if (encontrado == null)
        {
            Debug.LogError("Cañon: no se encontro 'PuntaCanon' en ningun nivel de la jerarquia. Verifica el nombre exacto en el editor.");
            return;
        }
        puntaCanon = encontrado.gameObject;
    }

    // Busca un hijo por nombre en cualquier nivel de profundidad, no solo hijos directos.
    private Transform BuscarHijoRecursivo(Transform padre, string nombre)
    {
        foreach (Transform hijo in padre)
        {
            if (hijo.name == nombre)
                return hijo;

            Transform resultado = BuscarHijoRecursivo(hijo, nombre);
            if (resultado != null)
                return resultado;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.VelociadadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }
        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (puntaCanon == null)
            {
                Debug.LogError("Cañon: puntaCanon es null, no se puede disparar.");
                return;
            }

            if (AdministradorJuego.DisparosPorJuego <= 0)
            {
                Debug.Log("No quedan disparos.");
                return;
            }

            GameObject temp = Instantiate(
                BalaPrefab,
                puntaCanon.transform.position,
                transform.rotation
            );

            Rigidbody tempRB = temp.GetComponent<Rigidbody>();

            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;

            tempRB.velocity =
                direccionDisparo.normalized * AdministradorJuego.VelocidadBala;

            AdministradorJuego.DisparosPorJuego--;

            Debug.Log(
                "Disparo realizado. Disparos restantes: " +
                AdministradorJuego.DisparosPorJuego
            );
        }
    }
}