using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using ServiceLocatorPath;
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
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().ComienzaAContarElTiempo(3);
        }

        public void FinishConfiguration()
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().DejaDeContarElTiempo();
        }

        
        public async Task<PersonajeStateResult> DoAction(object data)
        {
            //Debug.Log("Estas en estado de jugar");
            while (_mediadorDeEstadosDelJuego.EstanJugando())
            {
                if (_mediadorDeEstadosDelJuego.SeTerminoElJuego())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.FinDeJuego);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            //Debug.Log("Estas en estado de pausa");
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Pausa);
        }
    }
}