using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.NewGameStates
{
    public class MediadorDeEstadosDelJuego : MonoBehaviour, IMediadorDeEstadosDelJuego
    {
        //[SerializeField] private Button finalizarConfiguracionButton, pauseButton, jugarButton, finalizarJuegoButton;
        [SerializeField] private RectTransform textoIndicativoColocarHeroe;
        [SerializeField] private FactoriaCarta _factoriaCarta;
        [SerializeField] private ColocacionCartas _colocacionCartas;
        [SerializeField] private CartasConfiguracion cartasConfiguracion, heroesConfiguracion;
        [SerializeField] private GameObject canvasDeLasCartas, canvasPrincipal;
        [SerializeField] private bool jugadoresSincronizados;
        [SerializeField] private FactoriaPersonaje _factoriaPersonaje;
        private ConfiguracionDeLosEstadosDelJuego _configuracionDeLosEstadosDelJuego;
        [SerializeField] private TextMeshProUGUI textoDeGanadoPerdido;
        [SerializeField] private List<GameObject> cosasParaActivarCuandoGanaPierde;
        [SerializeField] private List<Image> imagenesParaActivarCuandoGanaPierde;
        [SerializeField] private List<Image> imagenesPanelesGanoPerdio;
        [SerializeField] private List<TextMeshProUGUI> textosParaActivarCuandoGanaPierde;
        [SerializeField] private Button botonContinuar;
        [SerializeField] private Transform _transformDondePosicionar;
        private bool _juegoPausado = true;
        private bool _juegoTerminado = false;
        private bool _juegoConfigurado = false;
        private bool _puedeSalirDelBucleDeEstados = false;
        private bool _seColocoElHeroe = false;

        private void Awake()
        {
            cartasConfiguracion = Instantiate(cartasConfiguracion);
            heroesConfiguracion = Instantiate(heroesConfiguracion);
        }

        private void Start()
        {
            _configuracionDeLosEstadosDelJuego = new ConfiguracionDeLosEstadosDelJuego();
            _configuracionDeLosEstadosDelJuego.AddInitialState(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego,
                new ConfiguracionDelJuegoState(this, _factoriaCarta, _colocacionCartas, cartasConfiguracion, canvasPrincipal, _factoriaPersonaje, canvasDeLasCartas, _transformDondePosicionar));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores, new SincronizacionDeJugadoresState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Jugando, new JugandoState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Pausa, new PausaState(this, _factoriaCarta));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.FinDeJuego, new FinDeJuegoState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.ColocandoHeroe, new ColocandoHeroeState(this, textoIndicativoColocarHeroe));
            StartState(_configuracionDeLosEstadosDelJuego.GetState(1));
            //finalizarConfiguracionButton.onClick.AddListener(() => _juegoConfigurado = true);
            //pauseButton.onClick.AddListener(() => _juegoPausado = true);
            //jugarButton.onClick.AddListener(() => _juegoPausado = false);
            //finalizarJuegoButton.onClick.AddListener(() => _juegoTerminado = true);
            //Este foreach es mientras seleccionamos las cartas en otra escena
            botonContinuar.onClick.AddListener(()=>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            });
            foreach (var cartaTemplate in heroesConfiguracion.GetCartasTemplate())
            {
                ServiceLocator.Instance.GetService<IBarajaDelPlayer>().AddHeroe(cartaTemplate.Value.Id);
            }
            foreach (var cartaTemplate in cartasConfiguracion.GetCartasTemplateLegales())
            {
                ServiceLocator.Instance.GetService<IBarajaDelPlayer>().AddCarta(cartaTemplate.Value.Id);
            }
            _colocacionCartas.Configurate();
            ServiceLocator.Instance.GetService<IEnemyInstantiate>().Configuration(_factoriaCarta, _factoriaPersonaje);
        }
        
        private async void StartState(IEstadoDeJuego state, object data = null)
        {
            while (!_puedeSalirDelBucleDeEstados)
            {
                state.InitialConfiguration();
                var resultData = await state.DoAction(data);
                state.FinishConfiguration();
                var nextState = _configuracionDeLosEstadosDelJuego.GetState(resultData.NextStateId);
                state = nextState;
                data = resultData.ResultData;
                
            }
        }

        public bool EstaPausadoElJuego()
        {
            return ServiceLocator.Instance.GetService<IServicioDeTiempo>().EstaPausadoElJuego();
        }

        public bool SeTerminoElJuego()
        {
            return ServiceLocator.Instance.GetService<IEstadoDePersonajesDelJuego>().TerminoElJuego();
        }

        public bool SeConfiguroElJuego()
        {
            return _juegoConfigurado;
        }

        public bool SeSincronizaronLosJugadores()
        {
            return jugadoresSincronizados;
        }

        public void SalirDelBuclePrincipal()
        {
            _puedeSalirDelBucleDeEstados = true;
        }

        public bool EstanJugando()
        {
            return ServiceLocator.Instance.GetService<IServicioDeTiempo>().EstanJugando();
        }

        public bool SeCololoElHeroe()
        {
            return _seColocoElHeroe;
        }

        private Vector3 point;
        private bool pedirUbicacionDeHeroe, tomoUbicacion;

        public Vector3 PedirColocacionDeHeroe()
        {
            pedirUbicacionDeHeroe = true;
            if (tomoUbicacion)
            {
                _factoriaCarta.CrearHeroe(point);
                _seColocoElHeroe = true;
                return point;
            }
            return Vector3.zero;
        }

        public void YaNoPedirColocacionDeHeroe()
        {
            pedirUbicacionDeHeroe = false;
        }

        public void MostrarMensajeDeQueSiPerdioGanoElJugador()
        {
            foreach (var o in cosasParaActivarCuandoGanaPierde)
            {
                o.SetActive(true);
            }
            var sequence = DOTween.Sequence();
            foreach (var imagen in imagenesParaActivarCuandoGanaPierde)
            {
                sequence.Insert(0, imagen.DOFade(1, 1));
            }
            
            foreach (var imagen in textosParaActivarCuandoGanaPierde)
            {
                sequence.Insert(0, imagen.DOFade(1, 1));
            }
            
            foreach (var imagen in imagenesPanelesGanoPerdio)
            {
                sequence.Insert(0, imagen.DOFade(0.82f, 1));
            }
            
            var textoColocar = "";
            if (ServiceLocator.Instance.GetService<IEstadoDePersonajesDelJuego>().GanoElPlayer())
            {
                textoColocar = "Ganaste la batalla";
            }
            else
            {
                textoColocar = "Perdiste la batalla";
            }
            
            textoDeGanadoPerdido.text = textoColocar;
        }

        public void OcultarCartas()
        {
            _colocacionCartas.OcultarCartas();
        }

        public void MostrarCartas()
        {
            _colocacionCartas.MostrarCartas();
        }

        public IFactoriaPersonajes GetFactoryHero()
        {
            return _factoriaPersonaje;
        }

        private void Update()
        {
            if (pedirUbicacionDeHeroe)
            {
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    //Debug.Log("coloca Al heroe");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        //Debug.Log(hit);
                        point = hit.point;
                        tomoUbicacion = true;
                    }
                }
            }
        }

        private void OnDisable()
        {
            SalirDelBuclePrincipal();
        }
    }
}