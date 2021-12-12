using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.NewGameStates
{
    public class MediadorDeEstadosDelJuego : MonoBehaviour, IMediadorDeEstadosDelJuego
    {
        [SerializeField] private Button finalizarConfiguracionButton, pauseButton, jugarButton, finalizarJuegoButton;
        private ConfiguracionDeLosEstadosDelJuego _configuracionDeLosEstadosDelJuego;
        private bool _juegoPausado = false;
        private bool _juegoTerminado = false;
        private bool _juegoConfigurado = false;
        [SerializeField] private bool jugadoresSincronizados;
        private bool _puedeSalirDelBucleDeEstados = false;

        private void Start()
        {
            _configuracionDeLosEstadosDelJuego = new ConfiguracionDeLosEstadosDelJuego();
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego, new ConfiguracionDelJuegoState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores, new SincronizacionDeJugadoresState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Jugando, new JugandoState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.Pausa, new PausaState(this));
            _configuracionDeLosEstadosDelJuego.AddState(ConfiguracionDeLosEstadosDelJuego.FinDeJuego, new FinDeJuegoState(this));
            StartState(_configuracionDeLosEstadosDelJuego.GetState(1));
            finalizarConfiguracionButton.onClick.AddListener(() => _juegoConfigurado = true);
            pauseButton.onClick.AddListener(() => _juegoPausado = true);
            jugarButton.onClick.AddListener(() => _juegoPausado = false);
            finalizarJuegoButton.onClick.AddListener(() => _juegoTerminado = true);
        }
        
        private async void StartState(IEstadoDeJuego state, object data = null)
        {
            while (_puedeSalirDelBucleDeEstados == false)
            {
                var resultData = await state.DoAction(data);
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
    }
}