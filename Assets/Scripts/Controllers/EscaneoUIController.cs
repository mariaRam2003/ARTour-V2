using UnityEngine;
using UnityEngine.UIElements;

public class EscaneoUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement botonMenuHamburguesa;
    private VisualElement menuHamburguesaVisual; // La instancia del men�
    private Button botonSimular;
    [SerializeField] private MenuHamburguesaUIController menuHamburguesaController;
    public VisualTreeAsset menuHamburguesaUXML; // Asigna aqu� el MenuHamburguesa.uxml desde inspector
    public CambiadorDePantallas cambiador;

    private bool menuInicializado = false;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Validar que los componentes del men� hamburguesa est�n asignados
        if (menuHamburguesaUXML == null)
        {
            Debug.LogError("MenuHamburguesaUXML no est� asignado en el inspector.");
            return;
        }

        if (menuHamburguesaController == null)
        {
            Debug.LogError("MenuHamburguesaUIController no est� asignado en el inspector.");
            return;
        }

        // Solo advertir sobre CambiadorDePantallas, pero no impedir que contin�e
        if (cambiador == null)
        {
            Debug.LogWarning("CambiadorDePantallas no est� asignado en el inspector. El bot�n de simular conexi�n no funcionar�.");
        }

        // Instancia el men� hamburguesa y lo a�ade al root
        menuHamburguesaVisual = menuHamburguesaUXML.CloneTree();
        root.Add(menuHamburguesaVisual);

        // Inicializa el controlador con el VisualElement del men� instanciado
        menuHamburguesaController.Inicializar(menuHamburguesaVisual);
        menuInicializado = true;

        // Configurar el bot�n del men� hamburguesa
        botonMenuHamburguesa = root.Q<VisualElement>("icono_menu_hamburguesa");
        if (botonMenuHamburguesa != null)
        {
            botonMenuHamburguesa.RegisterCallback<ClickEvent>(evt => OnBotonMenuHamburguesaClicked());
        }
        else
        {
            Debug.LogError("No se encontr� el VisualElement 'icono_menu_hamburguesa' en el UXML.");
        }

        // Configurar el bot�n de simular conexi�n
        ConfigurarBotonSimularConexion();
    }

    private void OnDisable()
    {
        if (botonMenuHamburguesa != null)
        {
            botonMenuHamburguesa.UnregisterCallback<ClickEvent>(evt => OnBotonMenuHamburguesaClicked());
        }
    }

    private void ConfigurarBotonSimularConexion()
    {
        botonSimular = root.Q<Button>("BotonSimularSensores");
        if (botonSimular != null)
        {
            // BYPASS TEMPORAL PARA SIMULAR CONEXI�N DE 3 SENSORES
            // TODO: Reemplazar este bypass por la l�gica real de conexi�n a sensores.
            // Cuando se detecten al menos 3 sensores, llamar a MostrarTour();
            // Ejemplo:
            // cambiador.MostrarTour():
            botonSimular.clicked += () =>
            {
                if (cambiador != null)
                {
                    Debug.Log("[BYPASS] Simulando conexi�n de 3 sensores...");
                    cambiador.MostrarTour();
                }
                else
                {
                    Debug.LogError("CambiadorDePantallas no est� asignado en el inspector.");
                }
            };
        }
        else
        {
            Debug.LogError("No se encontr� el Button 'BotonSimularSensores' en el UXML.");
        }
    }

    private void OnBotonMenuHamburguesaClicked()
    {
        Debug.Log("EscaneoUIController: �Click en icono de men� hamburguesa detectado!");

        // Validar que todo est� correctamente inicializado antes de mostrar el men�
        if (menuInicializado && menuHamburguesaController != null)
        {
            Debug.Log("EscaneoUIController: Menu inicializado, llamando a MostrarMenu...");
            menuHamburguesaController.MostrarMenu();
        }
        else
        {
            Debug.LogError("El men� hamburguesa no est� correctamente inicializado. Verifica las asignaciones en el inspector.");
        }
    }
}