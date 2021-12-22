using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.PersonajeStates
{
    public class InteractuarConElTargetState : IPersonajeState
    {

        private readonly Personaje _personaje;

        public InteractuarConElTargetState(Personaje personaje)
        {
            _personaje = personaje;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            var targets = _personaje.GetTargetComponent().GetTargets();
            //Debug.Log("estas en el estado interactuar");
            if (targets.Count > 0) _personaje.GetInteractionComponent().Interactuar(targets);
            await Task.Delay(TimeSpan.FromSeconds(_personaje.velocidadDeInteraccion));
            return new PersonajeStateResult(PersonajeStatesConfiguration.BuscarTargetState);
        }
    }
}