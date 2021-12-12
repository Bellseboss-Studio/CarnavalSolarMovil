using System;
using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using StatesOfEnemies;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class ConfiguracionDelJuegoState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public ConfiguracionDelJuegoState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            Debug.Log("Estas en estado de configurar juego");
            if (_mediadorDeEstadosDelJuego.SeConfiguroElJuego())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego);
        }
    }
}