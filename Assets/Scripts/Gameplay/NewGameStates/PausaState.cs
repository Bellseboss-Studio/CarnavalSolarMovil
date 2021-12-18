using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class PausaState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public PausaState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            Debug.Log("Estas en estado de pausa");
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