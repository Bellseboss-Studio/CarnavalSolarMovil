using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.NewGameStates
{
    public class MediadorDeEstadosDelJuego : MonoBehaviour, IMediadorDeEstadosDelJuego
    {
        //[SerializeField] private Button finalizarConfiguracionButton, pauseButton, jugarButton, finalizarJuegoButton;
        [SerializeField] private RectTransform textoIndicativoColocarHeroe;
        [SerializeField] private FactoriaCarta _factoriaCarta;
        [SerializeField] private ColocacionCartas _colocacionCartas;
        [SerializeField] private CartasConfiguracion cartasConfiguracion;
        [SerializeField] private GameObject canvasDeLasCartas, canvasPrincipal;
        [SerializeField] private bool jugadoresSincronizados;
        [SerializeField] private FactoriaPersonaje _factoriaPersonaje;
        private ConfiguracionDeLosEstadosDelJuego _configuracionDeLosEstadosDelJuego;
        private bool _juegoPausado = true;
        private bool _juegoTerminado = false;
        private bool _juegoConfigurado = false;
        private bool _puedeSalirDelBucleDeEstados = false;
        private bool _seColocoElHeroe = false;

        private void Awake()
        {
            cartasConfiguracion = Instantiate(cartasConfiguracion);
        }

        private void Start()
        {
            _configuracionDeLosEstadosDelJuego = new ConfiguracionDeLosEstadosDelJuego();
            _configuracionDeLosEstadosDelJuego.AddInitialState(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego,
                new ConfiguracionDelJuegoState(this, _factoriaCarta, _colocacionCartas, cartasConfiguracion, canvasPrincipal, _factoriaPersonaje, canvasDeLasCartas));
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
            foreach (var cartaTemplate in cartasConfiguracion.GetCartasTemplate())
            {
                ServiceLocator.Instance.GetService<IBarajaDelPlayer>().AddCarta(cartaTemplate.Id);
            }
            _colocacionCartas.Configurate();
            ServiceLocator.Instance.GetService<IEnemyInstantiate>().Configuration(_factoriaCarta);
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
            return _juegoTerminado;
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
            return ServiceLocator.Instance.GetService<IServicioDeTiempo>().SeEstaColocandoElHeroe();
        }

        public Vector3 PedirColocacionDeHeroe()
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    return hit.point;
                }
            }
            return Vector3.zero;
        }

        private void OnDisable()
        {
            SalirDelBuclePrincipal();
        }
    }
}