using UnityEngine;
using System.Collections;

namespace StatesOfEnemies
{
    public class BatallaState : IGameState
    {
        private readonly IMediatorBattle _mediator;

        public BatallaState(IMediatorBattle mediator)
        {
            _mediator = mediator;
            //Buscamos a los dos players
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            Debug.Log("Batalla");
            //_mediator.ConfigurePlayers();
            _mediator.MuestraLaUiDeBatalla();
            while (!_mediator.OncePlayersIsDead())
            {
                yield return new WaitForSeconds(0.1f);
            }
            behavior.SetNextState(GameStatesConfiguration.FinDeBatallaState);
        }
    }
}