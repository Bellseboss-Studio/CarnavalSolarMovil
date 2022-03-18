using UnityEngine;

namespace Gameplay.NewGameStates
{
    public interface IMediadorDeEstadosDelJuego
    {
        public bool EstaPausadoElJuego();
        public bool SeTerminoElJuego();
        public bool SeConfiguroElJuego();
        public bool SeSincronizaronLosJugadores();
        void SalirDelBuclePrincipal();
        bool EstanJugando();
        bool SeCololoElHeroe();
        Vector3 PedirColocacionDeHeroe();
        void YaNoPedirColocacionDeHeroe();
        void MostrarMensajeDeQueSiPerdioGanoElJugador();
        void OcultarCartas();
        void MostrarCartas();
        IFactoriaPersonajes GetFactoryHero();
        void CrearPrimerasCartas();
    }
}