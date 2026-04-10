using System.Collections.Generic;
using UnityEngine.Assertions;

namespace StatesOfEnemies
{
    public class GameStatesConfiguration
    {
        private int InitialState;

        public const int SeleccionandoPersonajesState = 1;
        public const int ConfiguracionState = 2;
        public const int CreacionDeSalaState = 3;
        public const int BatallaState = 4;
        public const int FinDeBatallaState = 5;
        public const int QueMasPodemosHacer = 6;
        public const int EsperaDeSincro = 7;

        private readonly Dictionary<int, IGameState> _states;

        public GameStatesConfiguration()
        {
            _states = new Dictionary<int, IGameState>();
        }


        public void AddInitialState(int id, IGameState state)
        {
            _states.Add(id, state);
            InitialState = id;
        }

        public void AddState(int id, IGameState state)
        {
            _states.Add(id, state);
        }

        public IGameState GetState(int stateId)
        {
            Assert.IsTrue(_states.ContainsKey(stateId), $"State with id {stateId} do not exit");
            return _states[stateId];
        }

        public IGameState GetInitialState()
        {
            return GetState(InitialState);
        }
    }
}