using System;
using System.Threading.Tasks;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay.PersonajeStates
{
    public class BuscarTargetState : IPersonajeState
    {

        private readonly TargetComponent _targetComponent;

        public BuscarTargetState(TargetComponent targetComponent)
        {
            _targetComponent = targetComponent;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            _targetComponent.BuscaTusTargets();
            if (_targetComponent.GetTargets().Count == 0)
            {
                
                Debug.Log("estas en el estado buscar target, pero no se encontro ningun target");
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                return new PersonajeStateResult(PersonajeStatesConfiguration.BuscarTargetState);
            }
            Debug.Log("Estas en el estado buscar target");
            return new PersonajeStateResult(PersonajeStatesConfiguration.DesplazarseHaciaElTargetState);
        }
    }
}