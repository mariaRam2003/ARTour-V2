using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class OnBoardingUIController : MonoBehaviour
{
    public CambiadorDePantallas cambiador;
    private VisualElement contenedorPantalla;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        contenedorPantalla = root.Q<VisualElement>("ContenedorPantalla");

        // Aplicar animaci�n de entrada
        IniciarAnimacionEntrada();

        // Bot�n siguiente
        Button botonSiguiente = root.Q<Button>("BotonSiguiente");
        if (botonSiguiente != null)
        {
            botonSiguiente.clicked += () =>
            {
                StartCoroutine(AnimacionSiguiente());
            };
        }

        // Bot�n iniciar (solo en �ltimo onboarding)
        Button botonIniciar = root.Q<Button>("BotonIniciar");
        if (botonIniciar != null)
        {
            botonIniciar.clicked += () =>
            {
                StartCoroutine(AnimacionIniciar());
            };
        }
    }

    void IniciarAnimacionEntrada()
    {
        if (contenedorPantalla != null)
        {
            // Empezar fuera de pantalla
            contenedorPantalla.AddToClassList("slide-enter");

            // Despu�s de un frame, animar hacia la posici�n normal
            StartCoroutine(AnimarEntrada());
        }
    }

    IEnumerator AnimarEntrada()
    {
        yield return null; // Esperar un frame

        contenedorPantalla.RemoveFromClassList("slide-enter");
        contenedorPantalla.AddToClassList("slide-enter-active");
    }

    IEnumerator AnimacionSiguiente()
    {
        // Inmediatamente cambiar a la siguiente pantalla ANTES de animar
        cambiador.SiguienteOnboarding();

        // La animaci�n de salida ya no es necesaria porque la nueva pantalla
        // se encarga de su propia animaci�n de entrada
        yield return null;
    }

    IEnumerator AnimacionIniciar()
    {
        Button botonIniciar = GetComponent<UIDocument>().rootVisualElement.Q<Button>("BotonIniciar");

        if (botonIniciar != null)
        {
            // Aplicar fadeout al bot�n
            botonIniciar.AddToClassList("fadeout");

            // Esperar que termine la animaci�n
            yield return new WaitForSeconds(0.8f);
        }

        // Guardar progreso y cambiar pantalla
        PlayerPrefs.SetInt("OnboardingCompletado", 1);
        cambiador.MostrarInicio();
    }
}