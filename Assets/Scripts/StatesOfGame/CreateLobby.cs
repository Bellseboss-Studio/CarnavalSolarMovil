using UnityEngine;
using System.Collections;

namespace StatesOfEnemies
{
    public class CreateLobby : IGameState
    {
        private readonly IMediatorGeneral _mediator;

        public CreateLobby(IMediatorGeneral mediator)
        {
            _mediator = mediator;
            Debug.Log("Creamos el loby");
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            yield return new WaitForSeconds(0.1f);
            _mediator.ConfiguraElSegundoPlayer();
            behavior.SetNextState(GameStatesConfiguration.EsperaDeSincro);
        }
    }
}