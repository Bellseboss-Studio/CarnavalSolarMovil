using System.Collections;
using UnityEngine;

namespace StatesOfEnemies
{
    public class GameBehavior : MonoBehaviour, IBehavior
    {
        private int _nextState;
        private GameStatesConfiguration _gameStatesConfiguration;
        private GameObject targer;

        public IGameState Configuration(Instalador mediador)
        {
            _gameStatesConfiguration = new GameStatesConfiguration();
            _gameStatesConfiguration.AddInitialState(GameStatesConfiguration.ConfiguracionState, new ConfiguracionDeElementosParaIniciar(mediador));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.SeleccionandoPersonajesState, new SeleccionandoPersonajes(mediador));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.CreacionDeSalaState, new CreateLobby(mediador));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.EsperaDeSincro, new EsperaParaSincronizar(mediador));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.BatallaState, new BatallaState(mediador));
            _gameStatesConfiguration.AddState(GameStatesConfiguration.FinDeBatallaState, new BatallaTerminada(mediador));
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
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
                #else
                Application.Quit();
                #endif
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