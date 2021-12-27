using System.Collections;
using UnityEngine;

namespace StatesOfEnemies
{
    public class EsperaParaSincronizar : IGameState
    {
        private readonly IMediatorDeEspera _mediador;

        public EsperaParaSincronizar(IMediatorDeEspera mediador)
        {
            _mediador = mediador;
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            Debug.Log("Sincronizacion");
            MxManager.MxInstance.PlayMusicState(GameStatesConfiguration.EsperaDeSincro);
            _mediador.SincronizaJugadores();
            while (!_mediador.EstanLosJugadoresSincronizados())
            {
                _mediador.BuscarNuevosPlayers();
                yield return new WaitForSeconds(1f);    
            }
            while (!_mediador.TenemosTodosLosDatosDeLosPersonajes())
            {
                _mediador.CompartirInformacion();   
                yield return new WaitForSeconds(1f);    
            }
            yield return new WaitForSeconds(_mediador.ColocarTemporalizador());
            behavior.SetNextState(GameStatesConfiguration.BatallaState);
        }
    }
}