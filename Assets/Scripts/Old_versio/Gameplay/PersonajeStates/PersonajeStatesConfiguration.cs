using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.PersonajeStates
{
    public class PersonajeStatesConfiguration
    {
        public const int CongeladoState = 0;
        public const int BuscarTargetState = 1;
        public const int DesplazarseHaciaElTargetState = 2;
        public const int InteractuarConElTargetState = 3;

        private readonly Dictionary<int, IPersonajeState> _states;

        public PersonajeStatesConfiguration()
        {
            _states = new Dictionary<int, IPersonajeState>();
        }
        
        public void AddState(int id, IPersonajeState state)
        {
            _states.Add(id, state);
        }

        public IPersonajeState GetState(int stateId)
        {
            Assert.IsTrue(_states.ContainsKey(stateId), $"State with id {stateId} do not exit");
            return _states[stateId];
        }
    }
}