using System.Collections;
using UnityEngine;

namespace StatesOfEnemies
{
    public class GameBehavior : MonoBehaviour, IBehavior
    {
        private int _nextState;
        private GameStatesConfiguration _gameStatesConfiguration;
        private GameObject targer;

        public IGameState Configuration(IMediatorGeneral mediator, IMediatorConfiguration configuration, IMediatorBattle battle)
        {
            _gameStatesConfiguration = new GameStatesConfiguration();
            _gameStatesConfiguration.AddInitialState(GameStatesConfiguration.ConfiguracionState, new ConfiguracionDeElementosParaIniciar(configuration));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.SeleccionandoPersonajesState, new SeleccionandoPersonajes(mediator));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.CreacionDeSalaState, new CreateLobby(mediator));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.BatallaState, new BatallaState(battle));
            _nextState = 0;
            return _gameStatesConfiguration.GetInitialState();
        }

        public IEnumerator StartState(IGameState state)
        {
            StartCoroutine(state.DoAction(this));
            while (GetNextState() == 0)
            {
                yield return new WaitForSeconds(1f);
            }

            if (GetNextState() != -1)
            {
                var nextClassState = _gameStatesConfiguration.GetState(GetNextState());
                CleanState();
                state = nextClassState;
                StartCoroutine(StartState(state));   
            }
            else
            {
                Debug.Log("Fin de los estados del juego");
            }
        }

        private void CleanState()
        {
            _nextState = 0;
        }

        public int GetNextState()
        {
            return _nextState;
        }

        public void SetNextState(int nextStateFromState)
        {
            _nextState = nextStateFromState;
        }

    }
}