using System.Linq;
using Photon.Pun;
using System.Collections.Generic;
using DG.Tweening;
using ServiceLocatorPath;
using StatesOfEnemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Instalador : MonoBehaviour, IMediatorGeneral, IMediatorConfiguration, IMediatorBattle, IMediatorDeEspera, IMediatorCooldown
{
    [SerializeField] private Player player1, player2;
    [SerializeField] private PlaceOfPlayer placeOfPlayer1, placeOfPlayer2;
    [SerializeField] private ControladorDeUI uiController;
    [SerializeField] private GameBehavior gameBehavior;
    [SerializeField] private GameObject uiBatalla, uiDeGanastePerdiste;
    [SerializeField] private TextMeshProUGUI textoDeGanastePerdiste, textoFinalizar, textoNuevaBatalla;
    [SerializeField] private Button nuevaBatalla, finalizar;
    [SerializeField] private Image contenedorGanastePerdiste;
    [SerializeField] private GameObject opcionesDeEleccion;
    [SerializeField] private TMP_InputField nombreDeSala;
    [SerializeField] private TextMeshProUGUI testoLog;
    [SerializeField] private Button botonPruebaDeEnvioDeDatos;
    [SerializeField] private List<PanelDePoderesController> _panelDePoderesControllers;

    private bool eligio;
    private bool quiereOtraBatalla;
    private bool _eligioCrearUnise;
    private bool _eligioCrear;
    private GameObject player01;
    private GameObject player02;
    private bool player1Sincro, player2Sincro;
    private PlayerSincro propioPlayer, otroPlayer;

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
        botonPruebaDeEnvioDeDatos.onClick.AddListener(Call);
        StartCoroutine(gameBehavior.StartState(gameBehavior.Configuration(this)));
    }

    private void Call()
    {
        EnviaInformacionQueModifiqueElOtroPlayer();
    }

    private void ColocarleCosasAlPlayer2()
    {
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1,10));
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1,10));
        player2.AddPj(new Personaje(2,2,2,"https://i.ytimg.com/vi/-6vnomecItA/maxresdefault.jpg","PlaceHolder","PlaceHolder",1,10));
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
        //Victory or Lose mx 
        textoDeGanastePerdiste.text = "Prueba inicial";
        uiDeGanastePerdiste.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Insert(0, uiDeGanastePerdiste.GetComponent<Image>().DOFade(.4f, .4f));
        sequence.Insert(0, nuevaBatalla.gameObject.GetComponent<Image>().DOFade(1, 1));
        sequence.Insert(0, finalizar.gameObject.GetComponent<Image>().DOFade(1, 1));
        sequence.Insert(0, contenedorGanastePerdiste.DOFade(1, 1));
        sequence.Insert(0, textoNuevaBatalla.DOFade(1, 1));
        sequence.Insert(0, textoFinalizar.DOFade(1, 1));
        sequence.Insert(0, textoDeGanastePerdiste.DOFade(1, 1));
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
        quiereOtraBatalla = false;
        player1.Restart();
        player2.Restart();
        uiController.Restart();
    }

    public void OcultarElLetreroDeGanarOPerder()
    {
        var sequence = DOTween.Sequence();
        sequence.Insert(0, uiDeGanastePerdiste.GetComponent<Image>().DOFade(.4f, .4f));
        sequence.Insert(0, nuevaBatalla.gameObject.GetComponent<Image>().DOFade(0, 1));
        sequence.Insert(0, finalizar.gameObject.GetComponent<Image>().DOFade(0, 1));
        sequence.Insert(0, contenedorGanastePerdiste.DOFade(0, 1));
        sequence.Insert(0, textoNuevaBatalla.DOFade(0, 1));
        sequence.Insert(0, textoFinalizar.DOFade(0, 1));
        sequence.Insert(0, textoDeGanastePerdiste.DOFade(0, 1));
        sequence.onComplete += () => { uiDeGanastePerdiste.SetActive(false); };
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
    }

    public void DestroyPlayers()
    {
        player1.Destruyelo();
        player2.Destruyelo();
    }
    
    public void HideBattleUi()
    {
        uiBatalla.SetActive(false);
    }
    
    public void MuestraLaUiDeBatalla()
    {
        uiBatalla.SetActive(true);
        for (int i = 0; i < player1.GetPersonajes().Count; i++)
        {
            _panelDePoderesControllers[i].CooldownForNormalAttack();
            _panelDePoderesControllers[i].CooldownForSpecialAttack();
        }
    }

    public void BuscarSiHayDatosDelOtroJugador()
    {
    }

    public void SincronizaJugadores()
    {
        propioPlayer = ServiceLocator.Instance.GetService<IMultiplayer>().CrearPersonaje(((uno, dos, tres) =>
        {
            player1.Configurarlo(AtaqueNormal, AtaqueEspecial);
            player1Sincro = true;
        }));
        BuscarNuevosPlayers();
        if (propioPlayer.IsMine())
        {
            propioPlayer.ConfigurarPersonajes(player1.GetPersonajes(),ConvertirInformacionDePlayerEnJson(player1));
        }
    }

    private void AtaqueEspecial(string nameoffrom, string nameoftarger, float damage)
    {
        var stringDeDanio = JsonUtility.ToJson(new InformacionDeDanioHaciaPersonaje() { danioVieneDe = nameoffrom, personajeDeDanio = nameoftarger, danioAntesDeDescuentos = damage,tipoDeAtaque = "e"});
        propioPlayer.DanarOponente(stringDeDanio);
    }

    private void AtaqueNormal(string nameoffrom, string nameoftarger, float damage)
    {
        var stringDeDanio = JsonUtility.ToJson(new InformacionDeDanioHaciaPersonaje() { danioVieneDe = nameoffrom, personajeDeDanio = nameoftarger, danioAntesDeDescuentos = damage,tipoDeAtaque = "n"});
        propioPlayer.DanarOponente(stringDeDanio);
    }

    private void CuandoHacenDanioAMi(string jsonformat)
    {
        Debug.Log($">>>>>>>>>>>>>>>recibo danio antes de descuento antes de convertir {jsonformat}");
        testoLog.text += "\n" + jsonformat;
        var informacionDeDanio = JsonUtility.FromJson<InformacionDeDanioHaciaPersonaje>(jsonformat);
        player2.QuienHizoDanio(informacionDeDanio.danioVieneDe, informacionDeDanio.tipoDeAtaque);
        player1.HayDanio(informacionDeDanio.personajeDeDanio, informacionDeDanio.danioAntesDeDescuentos);
        Debug.Log($">>>>>>>>>>>>>>>recibo danio antes de descuento {informacionDeDanio.ToString()}");
    }

    public bool EstanLosJugadoresSincronizados()
    {
        return player1Sincro && player2Sincro;
    }

    public float ColocarTemporalizador()
    {
        //mostrar un temporalizador para empezar el juego
        return 5;
    }

    public void BuscarNuevosPlayers()
    {
        foreach (var playerSincro in FindObjectsOfType<PlayerSincro>())
        {
            if (playerSincro._EsOtroPlayer)
            {
                var primerPj = new Personaje();
                var segundoPj = new Personaje();
                var tercerPj = new Personaje();
                foreach (var pj in ServiceLocator.Instance.GetService<IPlayFabCustom>().GetPjs().Where(pj => pj.nombre == playerSincro.unoN))
                {
                    primerPj = pj;
                }
                foreach (var pj in ServiceLocator.Instance.GetService<IPlayFabCustom>().GetPjs().Where(pj => pj.nombre == playerSincro.dosN))
                {
                    segundoPj = pj;
                }
                foreach (var pj in ServiceLocator.Instance.GetService<IPlayFabCustom>().GetPjs().Where(pj => pj.nombre == playerSincro.tresN))
                {
                    tercerPj = pj;
                }
                player2.AddPj(primerPj);
                player2.AddPj(segundoPj);
                player2.AddPj(tercerPj);
                player2.Configurarlo(this);
                player2Sincro = true;
                jsonDelOtroPlayer = playerSincro.informacion;
                otroPlayer = playerSincro;
                otroPlayer.onGuardarInformacionDelOtro += CuandoHacenDanioAMi;
            }
        }
        jsonDeMiPropioPlayer = ConvertirInformacionDePlayerEnJson(player1);
        
        testoLog.text += $"{jsonDeMiPropioPlayer} =;= {jsonDelOtroPlayer}";
    }

    public string jsonDelOtroPlayer, jsonDeMiPropioPlayer;
    public void CompartirInformacion()
    {
        foreach (var playerSincro in FindObjectsOfType<PlayerSincro>())
        {
            if (playerSincro.IsMine())
            {
                jsonDeMiPropioPlayer = ConvertirInformacionDePlayerEnJson(player1);
                playerSincro.CompartirInformacion(jsonDeMiPropioPlayer);
                //Colocando comentario
            }
        }
    }

    public bool TenemosTodosLosDatosDeLosPersonajes()
    {
        return jsonDelOtroPlayer != null && jsonDeMiPropioPlayer != null;
    }

    public void EnviaInformacionQueModifiqueElOtroPlayer()
    {
        var stringDeDanio = JsonUtility.ToJson(new InformacionDeDanioHaciaPersonaje() { danioVieneDe = "otroPlayer", personajeDeDanio = "llorona", danioAntesDeDescuentos = 2 });
        propioPlayer.DanarOponente(stringDeDanio);
    }

    private string ConvertirInformacionDePlayerEnJson(Player player)
    {
        var playerJson = new PlayerSincroJson(player);
        //TODO comvertir la informacion del player como de sus personajes en json para compartirlo
        return $"{JsonUtility.ToJson(playerJson)}";
    }

    

    public void MusicaPersonajeGanador()
    {
        //Play some mx
    }

    public void MusicaPersonajePerdedor()
    {
        //Play some mx
    }

    public void RehabilitarMenu()
    {
        uiController.HabilitarMenu();
        eligio = false;
    }

    public void LimpiarPersonajesElejidos()
    {
        placeOfPlayer1.DesConfigurePjView();
        uiController.ClearPlayer();
    }

    public void CannotAttackAnymore()
    {
        placeOfPlayer1.DeshabilitaElDrag();
    }

    public void ConfiguraCooldownsPorPersonaje(List<Personaje> personajes)
    {
        for (int i = 0; i < personajes.Count; i++)
        {
            _panelDePoderesControllers[i].ConfigureSliderValues(personajes[i].cooldown, this);
        }
    }
}