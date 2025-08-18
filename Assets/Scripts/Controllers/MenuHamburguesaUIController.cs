using UnityEngine;
using UnityEngine.UIElements;

public class MenuHamburguesaUIController : MonoBehaviour
{
    private VisualElement menu;
    private Button btnCerrar, btnReconectar, btnReiniciar, btnDiagnostico, btnReportar, btnSalir;
    public CambiadorDePantallas cambiador;

    private bool inicializado = false;

    public void Inicializar(VisualElement root)
    {
        Debug.Log("MenuHamburguesaUIController: Inicializando...");

        if (root == null)
        {
            Debug.LogError("El VisualElement root es null. No se puede inicializar el men� hamburguesa.");
            return;
        }

        menu = root.Q<VisualElement>("menu_hamburguesa");
        if (menu == null)
        {
            Debug.LogError("No se encontr� el elemento 'menu_hamburguesa' en el UXML.");
            return;
        }
        else
        {
            Debug.Log("MenuHamburguesaUIController: Elemento 'menu_hamburguesa' encontrado exitosamente");
        }

        btnCerrar = root.Q<Button>("boton_cerrar_menu");
        btnReconectar = root.Q<Button>("btn_reconectar");
        btnReiniciar = root.Q<Button>("btn_reiniciar");
        btnDiagnostico = root.Q<Button>("btn_diagnostico");
        btnReportar = root.Q<Button>("btn_reportar");
        btnSalir = root.Q<Button>("btn_salir_inicio");

        // Validar que todos los botones se encontraron
        if (btnCerrar == null) Debug.LogError("No se encontr� el bot�n 'boton_cerrar_menu'");
        if (btnReconectar == null) Debug.LogError("No se encontr� el bot�n 'btn_reconectar'");
        if (btnReiniciar == null) Debug.LogError("No se encontr� el bot�n 'btn_reiniciar'");
        if (btnDiagnostico == null) Debug.LogError("No se encontr� el bot�n 'btn_diagnostico'");
        if (btnReportar == null) Debug.LogError("No se encontr� el bot�n 'btn_reportar'");
        if (btnSalir == null) Debug.LogError("No se encontr� el bot�n 'btn_salir_inicio'");

        // Asignar eventos solo a los botones que existen
        if (btnCerrar != null)
            btnCerrar.clicked += OcultarMenu;

        if (btnSalir != null && cambiador != null)
            btnSalir.clicked += () => cambiador.MostrarInicio();
        else if (btnSalir != null && cambiador == null)
            Debug.LogError("CambiadorDePantallas no est� asignado en MenuHamburguesaUIController.");

        // Acciones extra
        if (btnReconectar != null)
            btnReconectar.clicked += () => Debug.Log("Reconectando sensores...");
        if (btnReiniciar != null)
            btnReiniciar.clicked += () => Debug.Log("Reiniciando tour...");
        if (btnDiagnostico != null)
            btnDiagnostico.clicked += () => Debug.Log("Mostrando diagn�stico...");
        if (btnReportar != null)
            btnReportar.clicked += () => Debug.Log("Reporte enviado...");

        inicializado = true;

        // Forzar ocultaci�n del men� por defecto (sobreescribir valores del UXML)
        menu.style.display = DisplayStyle.None;
        menu.style.visibility = Visibility.Hidden;

        Debug.Log("MenuHamburguesaUIController: Men� hamburguesa inicializado correctamente.");
    }

    public void MostrarMenu()
    {
        Debug.Log("MenuHamburguesaUIController: MostrarMenu() llamado");

        if (!inicializado)
        {
            Debug.LogError("El men� hamburguesa no est� inicializado. Llama a Inicializar() primero.");
            return;
        }

        if (menu != null)
        {
            Debug.Log("MenuHamburguesaUIController: Mostrando men� usando style.display");
            menu.style.display = DisplayStyle.Flex;
            menu.style.position = Position.Absolute;
            menu.style.top = 0;
            menu.style.left = 0;
            menu.style.right = 0;
            menu.style.bottom = 0;
            Debug.Log("MenuHamburguesaUIController: Men� mostrado exitosamente");
        }
        else
        {
            Debug.LogError("El elemento menu es null. No se puede mostrar el men�.");
        }
    }

    public void OcultarMenu()
    {
        if (!inicializado)
        {
            Debug.LogError("El men� hamburguesa no est� inicializado.");
            return;
        }

        if (menu != null)
        {
            menu.style.display = DisplayStyle.None;
            menu.style.visibility = Visibility.Hidden;
        }
    }
}