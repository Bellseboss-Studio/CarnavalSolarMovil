using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay
{
    internal class CongeladoState : IPersonajeState
    {

        private readonly Personaje _personaje;
        private readonly InteraccionComponent _interaccionComponent;

        public CongeladoState(Personaje personaje, InteraccionComponent interaccionComponent)
        {
            _personaje = personaje;
            _interaccionComponent = interaccionComponent;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            //Debug.Log("estas en el estado congelado");
            if (_personaje.LaPartidaEstaCongelada())
            {
                //Debug.Log("estas y seguiras en el estado congelado");
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(PersonajeStatesConfiguration.CongeladoState);
            }
            await Task.Yield();
            _interaccionComponent.InteraccionInicial(_personaje);
            return new PersonajeStateResult(PersonajeStatesConfiguration.BuscarTargetState);
        }
    }
}