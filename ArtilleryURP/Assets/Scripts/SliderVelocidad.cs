using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVelocidad : MonoBehaviour
{
    public Opciones opciones;
    Slider slider;

    public void Start()
    {
        slider = this.GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("SliderVelocidad: no se encontro un componente Slider en este GameObject. " +
                "Verifica que este script este sobre el objeto que tiene el componente Slider (UnityEngine.UI), no en un padre o hijo distinto.");
            return;
        }

        // Sincroniza el slider con el valor actual al arrancar, para que no "salte" al primer drag.
        slider.value = opciones.velocidadbola;

        slider.onValueChanged.AddListener(delegate { ControlarCambios(); });
    }

    public void ControlarCambios()
    {
        // Fija el valor absoluto del slider, en vez de sumarlo, para evitar
        // que arrastrar el slider acumule velocidad sin control.
        opciones.FijarVelocidad(slider.value);
    }
}