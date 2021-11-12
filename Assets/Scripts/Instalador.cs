using Photon.Pun;
using ServiceLocatorPath;
using StatesOfEnemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Instalador : MonoBehaviour, IMediatorGeneral, IMediatorConfiguration, IMediatorBattle, IMediatorDeEspera
{
    [SerializeField] private Player player1, player2;
    [SerializeField] private PlaceOfPlayer placeOfPlayer1, placeOfPlayer2;
    [SerializeField] private ControladorDeUI uiController;
    [SerializeField] private GameBehavior gameBehavior;
    [SerializeField] private GameObject uiBatalla, uiDeGanastePerdiste;
    [SerializeField] private TextMeshProUGUI textoDeGanastePerdiste;
    [SerializeField] private Button nuevaBatalla, finalizar;
    [SerializeField] private GameObject opcionesDeEleccion;
    [SerializeField] private TMP_InputField nombreDeSala;

    private bool eligio;
    private bool quiereOtraBatalla;
    private bool _eligioCrearUnise;
    private bool _eligioCrear;

    public void CrearSala(bool cierto)
    {
        _eligioCrearUnise = true;
        _eligioCrear = cierto;
    }

    private void Start()
    {
        nuevaBatalla.onClick.AddListener(() =>
        {
            quiereOtraBatalla = true;
            eligio = true;
        });
        finalizar.onClick.AddListener(() =>
        {
            eligio = true;
        });
        placeOfPlayer1.Configure();
        placeOfPlayer2.Configure();
        uiController.Configure(player1);
        StartCoroutine(gameBehavior.StartState(gameBehavior.Configuration(this)));
    }

    private void ColocarleCosasAlPlayer2()
    {
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1));
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1));
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1));
    }

    public void ShowLoad()
    {
        uiController.ShowLoading();
    }

    public void HideLoad()
    {
        uiController.HideLoading();
    }

    public void MostrarElLetreroDeGanarOPerder()
    {
        //debo mostrar un panel con el texto
        textoDeGanastePerdiste.text = "Prueba inicial";
        uiDeGanastePerdiste.SetActive(true);
    }

    public bool EligioLoQueQuiereHacer()
    {
        return eligio;
    }

    public bool QuiereHacerOtraBatalla()
    {
        return quiereOtraBatalla;
    }

    public void ReiniciaTodosLosEstados()
    {
        player1.Restart();
        player2.Restart();
        uiController.Restart();
    }

    public void OcultarElLetreroDeGanarOPerder()
    {
        uiDeGanastePerdiste.SetActive(false);
    }

    public bool TerminoDeElegir => uiController.GetTerminoDeElegir();
    public void HideStore()
    {
        uiController.HideStore();
    }

    public void ConfiguraElSegundoPlayer()
    {
        ColocarleCosasAlPlayer2();
    }

    public bool ElegidoSiCrearUnise()
    {
        return _eligioCrearUnise;
    }

    public void MuestraLasOpcionesParaElJugador()
    {
        opcionesDeEleccion.SetActive(true);
    }

    public bool EligioCrear()
    {
        return _eligioCrear;
    }

    public string GetNombreDeSala()
    {
        return nombreDeSala.text;
    }

    public void OcultarOpcionesAlJugador()
    {
        opcionesDeEleccion.SetActive(false);
    }

    public void MostrarUnPanelDeCarga()
    {
        
        //implementar
    }

    public void OcultarPanelDeCarga()
    {
        //implementar
    }

    public void ResetLaParteDeUnirseCrearSala()
    {
        _eligioCrear = false;
        _eligioCrearUnise = false;
    }

    public void ShowStore()
    {
        uiController.LoadPersonajes();
        uiController.ShowStore();
    }

    public bool OncePlayersIsDead()
    {
        return player1.IsDead() || player2.IsDead();
    }

    public void ConfigurePlayers()
    {
        player1.Configurarlo();
        player2.Configurarlo();
    }

    public void MuestraLaUiDeBatalla()
    {
        uiBatalla.SetActive(true);
    }

    public void SincronizaJugadores()
    {
        
    }

    public bool EstanLosJugadoresSincronizados()
    {
        return false;
    }

    public float ColocarTemporalizador()
    {
        //mostrar un temporalizador para empezar el juego
        return 2;
    }
}