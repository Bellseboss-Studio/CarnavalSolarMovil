using System.Collections.Generic;
using StatesOfEnemies;
using UnityEngine.Assertions;

namespace Gameplay.NewGameStates
{
    public class ConfiguracionDeLosEstadosDelJuego
    {
        private int InitialState;

        public const int ConfiguracionDelJuego = 1;
        public const int SincronizacionDeJugadores = 2;
        public const int Jugando = 3;
        public const int Pausa = 4;
        public const int FinDeJuego = 5;
        
        private readonly Dictionary<int, IEstadoDeJuego> _states;

        public ConfiguracionDeLosEstadosDelJuego()
        {
            _states = new Dictionary<int, IEstadoDeJuego>();
        }

        public void AddInitialState(int id, IEstadoDeJuego state)
        {
            _states.Add(id, state);
            InitialState = id;
        }

        public void AddState(int id, IEstadoDeJuego state)
        {
            _states.Add(id, state);
        }

        public IEstadoDeJuego GetState(int stateId)
        {
            Assert.IsTrue(_states.ContainsKey(stateId), $"State with id {stateId} do not exit");
            return _states[stateId];
        }

        public IEstadoDeJuego GetInitialState()
        {
            return GetState(InitialState);
        }
    }
}