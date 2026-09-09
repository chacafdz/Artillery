using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownDificultad : MonoBehaviour
{
    public Opciones opciones;
    private TMP_Dropdown dificultad;

    private void Start()
    {
        dificultad = GetComponent<TMP_Dropdown>();
        dificultad.onValueChanged.AddListener(delegate { opciones.CambiarDificultad(dificultad.value); });
    }
}