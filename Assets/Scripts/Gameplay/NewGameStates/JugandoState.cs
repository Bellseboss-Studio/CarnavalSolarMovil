using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class JugandoState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public JugandoState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }

        public void InitialConfiguration()
        {
            
        }

        public void FinishConfiguration()
        {
            
        }

        
        public async Task<PersonajeStateResult> DoAction(object data)
        {
            //Debug.Log("Estas en estado de jugar");
            if (_mediadorDeEstadosDelJuego.SeTerminoElJuego())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.FinDeJuego);
            }

            if (_mediadorDeEstadosDelJuego.EstaPausadoElJuego())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Pausa);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Jugando);
        }
    }
}