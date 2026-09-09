using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuerzaDisparo : MonoBehaviour
{
    [Header("Referencias")]
    public Slider sliderFuerza;

    [Header("Configuracion")]
    public float fuerzaMinima = 1f;   // valor inicial de la barra (evita disparos con fuerza 0)
    public float fuerzaMaxima = 150f;  // limite superior de la barra
    public float velocidadCarga = 90f; // unidades por segundo que sube/baja la barra

    private float valorActual;
    private bool subiendo = true;

    // Factor entre 0 y 1 que se usa como multiplicador de la velocidad de la bala
    public float ValorNormalizado => valorActual / fuerzaMaxima;

    private void Awake()
    {
        if (sliderFuerza != null)
        {
            sliderFuerza.minValue = 0f;
            sliderFuerza.maxValue = fuerzaMaxima;
        }
        valorActual = fuerzaMinima;
        ActualizarSlider();
    }

    // Llamar cada frame mientras la tecla ModificarFuerza este presionada
    public void Cargar()
    {
        float delta = velocidadCarga * Time.deltaTime;
        Debug.Log("Cargando fuerza: " + valorActual + " - Slider asignado: " + (sliderFuerza != null));
        if (subiendo)
        {
            valorActual += delta;
            if (valorActual >= fuerzaMaxima)
            {
                valorActual = fuerzaMaxima;
                subiendo = false;
            }
        }
        else
        {
            valorActual -= delta;
            if (valorActual <= fuerzaMinima)
            {
                valorActual = fuerzaMinima;
                subiendo = true;
            }
        }

        ActualizarSlider();
    }

    // Llamar al momento de disparar: devuelve el factor de fuerza actual y reinicia la barra
    public float ObtenerFactorYReiniciar()
    {
        float factor = ValorNormalizado;
        valorActual = fuerzaMinima;
        subiendo = true;
        ActualizarSlider();
        return factor;
    }

    private void ActualizarSlider()
    {
        if (sliderFuerza != null) sliderFuerza.value = valorActual;
    }
}