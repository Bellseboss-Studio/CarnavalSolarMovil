using System;
using Gameplay.UsoDeCartas;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.NewGameStates
{
    public class MediadorDeEstadosDelJuego : MonoBehaviour, IMediadorDeEstadosDelJuego
    {
        [SerializeField] private Button finalizarConfiguracionButton, pauseButton, jugarButton, finalizarJuegoButton;
        [SerializeField] private FactoriaCarta _factoriaCarta;
        [SerializeField] private ColocacionCartas _colocacionCartas;
        [SerializeField] private CartasConfiguracion cartasConfiguracion;
        [SerializeField] private GameObject canvasDeLasCartas;
        [SerializeField] private bool jugadoresSincronizados;
        [SerializeField] private FactoriaPersonaje _factoriaPersonaje;
        private ConfiguracionDeLosEstadosDelJuego _configuracionDeLosEstadosDelJuego;
        private bool _juegoPausado = true;
        private bool _juegoTerminado = false;
        private bool _juegoConfigurado = false;
        private bool _puedeSalirDelBucleDeEstados = false;

        private void Awake()
        {
            cartasConfiguracion = Instantiate(cartasConfiguracion);
        }

        private void Start()
        {
            _configuracionDeLosEstadosDelJuego = new ConfiguracionDeLosEstadosDelJuego();
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego,
                new ConfiguracionDelJuegoState(this, _factoriaCarta, _colocacionCartas, cartasConfiguracion, canvasDeLasCartas, _factoriaPersonaje));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores, new SincronizacionDeJugadoresState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Jugando, new JugandoState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Pausa, new PausaState(this, _factoriaCarta));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.FinDeJuego, new FinDeJuegoState(this));
            StartState(_configuracionDeLosEstadosDelJuego.GetState(1));
            finalizarConfiguracionButton.onClick.AddListener(() => _juegoConfigurado = true);
            pauseButton.onClick.AddListener(() => _juegoPausado = true);
            jugarButton.onClick.AddListener(() => _juegoPausado = false);
            finalizarJuegoButton.onClick.AddListener(() => _juegoTerminado = true);
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
            return _juegoPausado;
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

        private void OnDisable()
        {
            SalirDelBuclePrincipal();
        }
    }
}