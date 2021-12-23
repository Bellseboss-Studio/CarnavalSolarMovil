using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay
{
    internal class CongeladoState : IPersonajeState
    {

        private readonly Personaje _personaje;

        public CongeladoState(Personaje personaje)
        {
            _personaje = personaje;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            //Debug.Log("estas en el estado congelado");
            if (_personaje.LaPartidaEstaCongelada())
            {
                //Debug.Log("estas y seguiras en el estado congelado");
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                return new PersonajeStateResult(PersonajeStatesConfiguration.CongeladoState);
            }
            await Task.Yield();
            return new PersonajeStateResult(PersonajeStatesConfiguration.BuscarTargetState);
        }
    }
}