using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFuerza : MonoBehaviour
{
    private Camera camaraPrincipal;

    private void Start()
    {
        camaraPrincipal = Camera.main;
    }

    private void LateUpdate()
    {
        if (camaraPrincipal == null) return;
        transform.rotation = camaraPrincipal.transform.rotation;
    }
}