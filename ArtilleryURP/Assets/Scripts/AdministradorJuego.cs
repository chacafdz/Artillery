using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego SingletonAdministradorJuego;

    private static int velocidadBala = 30;
    private static int disparosPorJuego = 10;
    private static float velocidadRotacion = 1;

    public static int VelocidadBala
    {
        get { return velocidadBala; }
        set { velocidadBala = value; }
    }

    public static int DisparosPorJuego
    {
        get { return disparosPorJuego; }
        set { disparosPorJuego = value; }
    }

    public static float VelociadadRotacion
    {
        get { return velocidadRotacion; }
        set { velocidadRotacion = value; }
    }

    private void Awake()
    {
        if (SingletonAdministradorJuego == null)
        {
            SingletonAdministradorJuego = this;

            // Reiniciar los disparos al comenzar una sesión
            DisparosPorJuego = 10;
        }
        else
        {
            Debug.LogError("Ya existe una instancia de esta clase");
        }
    }
}