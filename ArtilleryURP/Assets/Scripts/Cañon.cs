using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class Cañon : MonoBehaviour
{
    public static bool Bloqueado;
    public AudioClip ClipDisparo;
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo;

    [SerializeField] private GameObject BalaPrefab;
    public GameObject ParticulasDisparo;

    [Header("Fuerza de disparo")]
    public FuerzaDisparo fuerzaDisparo;

    private GameObject puntaCanon;
    private float rotacion;

    public CanonControls canonControls;
    private InputAction apuntar;
    private InputAction modificarFuerza;
    private InputAction disparar;

    private void Awake()
    {
        canonControls = new CanonControls();
    }

    private void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarFuerza = canonControls.Canon.ModificarFuerza;
        disparar = canonControls.Canon.Disparar;
        apuntar.Enable();
        modificarFuerza.Enable();
        disparar.Enable();

        // Al soltar la tecla de ModificarFuerza (Espacio), se dispara con la fuerza acumulada
        modificarFuerza.canceled += Disparar;
    }

    private void OnDisable()
    {
        modificarFuerza.canceled -= Disparar;
        apuntar.Disable();
        modificarFuerza.Disable();
        disparar.Disable();
    }

    private void Start()
    {
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetComponent<AudioSource>();
        Transform encontrado = BuscarHijoRecursivo(transform, "PuntaCanon");
        if (encontrado == null)
        {
            Debug.LogError("Cañon: no se encontro 'PuntaCanon' en ningun nivel de la jerarquia. Verifica el nombre exacto en el editor.");
            return;
        }
        puntaCanon = encontrado.gameObject;
    }

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

    void Update()
    {
        rotacion += apuntar.ReadValue<float>() * AdministradorJuego.VelociadadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }
        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        // Mientras se mantenga presionada ModificarFuerza, la barra sube/baja en ping-pong
        if (modificarFuerza.IsPressed() && fuerzaDisparo != null && !Bloqueado)
        {
            fuerzaDisparo.Cargar();
        }
    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if (Bloqueado) return;
        if (AdministradorJuego.DisparosPorJuego <= 0) return;

        if (puntaCanon == null)
        {
            Debug.LogError("Cañon: puntaCanon es null, no se puede disparar.");
            return;
        }

        GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);

        Rigidbody tempRB = temp.GetComponent<Rigidbody>();
        SeguirCamara.objetivo = temp;
        Vector3 direccionDisparo = transform.rotation.eulerAngles;
        direccionDisparo.y = 90 - direccionDisparo.x;
        Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
        GameObject Particulas = Instantiate(ParticulasDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);

        // Factor de fuerza (0 a 1) segun donde se quedo la barra al soltar la tecla
        float factorFuerza = fuerzaDisparo != null ? fuerzaDisparo.ObtenerFactorYReiniciar() : 1f;

        tempRB.velocity = direccionDisparo.normalized * AdministradorJuego.VelocidadBala * factorFuerza;

        AdministradorJuego.DisparosPorJuego--;

        Debug.Log(
            "Disparo realizado. Fuerza: " + factorFuerza.ToString("P0") +
            " - Disparos restantes: " + AdministradorJuego.DisparosPorJuego);
        SourceDisparo.Play();
        Bloqueado = true;
    }
}