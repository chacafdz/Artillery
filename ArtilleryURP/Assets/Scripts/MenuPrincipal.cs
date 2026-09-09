using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject MenuOpciones;
    public GameObject MenuInicial;

    public void Iniciarjuego()
    {
        SceneManager.LoadScene(1);

    }

    public void finalizarJuego()
    {
        Application.Quit();

    }
    private void Awake()
    {
        MenuInicial.SetActive(true);
        MenuOpciones.SetActive(false);
    }
    public void MostrarOpciones()
    {
        MenuInicial.SetActive(false);
        MenuOpciones.SetActive(true);
    }

    public void MostrarMenuInicial()
    {
        MenuOpciones.SetActive(false);
        MenuInicial.SetActive(true);

    }

}