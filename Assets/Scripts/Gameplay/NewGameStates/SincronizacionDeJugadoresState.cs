using System;
using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using ServiceLocatorPath;
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

        public void InitialConfiguration()
        {
            
        }

        public void FinishConfiguration()
        {
            
        }


        public async Task<PersonajeStateResult> DoAction(object data)
        {
            //Debug.Log("Estas en estado de sincronizar jugadores");
            if (_mediadorDeEstadosDelJuego.SeSincronizaronLosJugadores())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                ServiceLocator.Instance.GetService<IServicioDeEnergia>().Init();
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.InstanciarHeroe);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores);
        }
    }
}