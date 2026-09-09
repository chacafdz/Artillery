using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Opciones", menuName = "Herramientas/Opciones", order = 1)]
public class Opciones : PuntajePersistente
{
    public float velocidadbola = 30;

    [Tooltip("Limites de seguridad para la velocidad de la bola.")]
    public float velocidadMinima = 10f;
    public float velocidadMaxima = 50f;

    public enum dificultad
    {
        facil,
        normal,
        dificil
    }

    public dificultad NivelDificultad;

    // Suma un delta a la velocidad actual (uso puntual, ej. power-ups).
    public void CambiarVelocidad(float deltaVelocidad)
    {
        velocidadbola += deltaVelocidad;
        velocidadbola = Mathf.Clamp(velocidadbola, velocidadMinima, velocidadMaxima);
    }

    // Fija la velocidad a un valor absoluto (uso correcto para un Slider de UI).
    public void FijarVelocidad(float nuevaVelocidad)
    {
        velocidadbola = Mathf.Clamp(nuevaVelocidad, velocidadMinima, velocidadMaxima);
    }

    public void CambiarDificultad(int nuevaDificultad)
    {
        NivelDificultad = (dificultad)nuevaDificultad;
    }
}