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
    private GameObject player01;
    private GameObject player02;
    private bool player1Sincro, player2Sincro;

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
        var crearPersonaje = ServiceLocator.Instance.GetService<IMultiplayer>().CrearPersonaje(((uno, dos, tres) =>
        {
            var unoP = Resources.Load<GameObject>($"Prefab/{uno}");
            var dosP = Resources.Load<GameObject>($"Prefab/{dos}");
            var tresP = Resources.Load<GameObject>($"Prefab/{tres}");
            var unoI = Instantiate(unoP);
            unoI.transform.position = placeOfPlayer1.GetPoints()[0].transform.position;
            unoI.transform.rotation = placeOfPlayer1.GetPoints()[0].transform.rotation;
            var dosI = Instantiate(dosP);
            dosI.transform.position = placeOfPlayer1.GetPoints()[1].transform.position;
            dosI.transform.rotation = placeOfPlayer1.GetPoints()[1].transform.rotation;
            var tresI = Instantiate(tresP);
            tresI.transform.position = placeOfPlayer1.GetPoints()[2].transform.position;
            tresI.transform.rotation = placeOfPlayer1.GetPoints()[2].transform.rotation;
            player1Sincro = true;
        }), (uno, dos, tres) =>
        {
            
        });
        foreach (var playerSincro in FindObjectsOfType<PlayerSincro>())
        {
            if (playerSincro._EsOtroPlayer)
            {
                Debug.Log($"Aqui debe de instanciar a {playerSincro.unoN} {playerSincro.dosN} {playerSincro.tresN}");
                var unoP = Resources.Load<GameObject>($"Prefab/{playerSincro.unoN}");
                var dosP = Resources.Load<GameObject>($"Prefab/{playerSincro.dosN}");
                var tresP = Resources.Load<GameObject>($"Prefab/{playerSincro.tresN}");
                var unoI = Instantiate(unoP);
                unoI.transform.position = placeOfPlayer2.GetPoints()[0].transform.position;
                unoI.transform.rotation = placeOfPlayer2.GetPoints()[0].transform.rotation;
                unoI.transform.localScale = new Vector3(-1, 1, 1);
                var dosI = Instantiate(dosP);
                dosI.transform.position = placeOfPlayer2.GetPoints()[1].transform.position;
                dosI.transform.rotation = placeOfPlayer2.GetPoints()[1].transform.rotation;
                dosI.transform.localScale = new Vector3(-1, 1, 1);
                var tresI = Instantiate(tresP);
                tresI.transform.position = placeOfPlayer2.GetPoints()[2].transform.position;
                tresI.transform.rotation = placeOfPlayer2.GetPoints()[2].transform.rotation;
                tresI.transform.localScale = new Vector3(-1, 1, 1);
                player2Sincro = true;
            }
        }
        if (crearPersonaje.IsMine())
        {
            crearPersonaje.ConfigurarPersonajes(player1.GetPersonajes());   
        }
    }

    public bool EstanLosJugadoresSincronizados()
    {
        return player1Sincro && player2Sincro;
    }

    public float ColocarTemporalizador()
    {
        //mostrar un temporalizador para empezar el juego
        return 2;
    }

    public void BuscarNuevosPlayers()
    {
        foreach (var playerSincro in FindObjectsOfType<PlayerSincro>())
        {
            if (playerSincro._EsOtroPlayer)
            {
                Debug.Log($"Aqui debe de instanciar a {playerSincro.unoN} {playerSincro.dosN} {playerSincro.tresN}");
                var unoP = Resources.Load<GameObject>($"Prefab/{playerSincro.unoN}");
                var dosP = Resources.Load<GameObject>($"Prefab/{playerSincro.dosN}");
                var tresP = Resources.Load<GameObject>($"Prefab/{playerSincro.tresN}");
                var unoI = Instantiate(unoP);
                unoI.transform.position = placeOfPlayer2.GetPoints()[0].transform.position;
                unoI.transform.rotation = placeOfPlayer2.GetPoints()[0].transform.rotation;
                unoI.transform.localScale = new Vector3(-1, 1, 1);
                var dosI = Instantiate(dosP);
                dosI.transform.position = placeOfPlayer2.GetPoints()[1].transform.position;
                dosI.transform.rotation = placeOfPlayer2.GetPoints()[1].transform.rotation;
                dosI.transform.localScale = new Vector3(-1, 1, 1);
                var tresI = Instantiate(tresP);
                tresI.transform.position = placeOfPlayer2.GetPoints()[2].transform.position;
                tresI.transform.rotation = placeOfPlayer2.GetPoints()[2].transform.rotation;
                tresI.transform.localScale = new Vector3(-1, 1, 1);
                player2Sincro = true;
            }
        }
    }
}