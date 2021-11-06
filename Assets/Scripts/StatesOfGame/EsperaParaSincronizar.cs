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
            _mediador.SincronizaJugadores();
            while (!_mediador.EstanLosJugadoresSincronizados())
            {
                yield return new WaitForSeconds(0.1f);    
            }
            yield return new WaitForSeconds(_mediador.ColocarTemporalizador());
            behavior.SetNextState(GameStatesConfiguration.BatallaState);
        }
    }
}