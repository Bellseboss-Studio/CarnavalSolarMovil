using System;
using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using StatesOfEnemies;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class SincronizacionDeJugadoresState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public SincronizacionDeJugadoresState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }


        public async Task<PersonajeStateResult> DoAction(object data)
        {
            Debug.Log("Estas en estado de sincronizar jugadores");
            if (_mediadorDeEstadosDelJuego.SeSincronizaronLosJugadores())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Jugando);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores);
        }
    }
}