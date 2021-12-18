using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using Gameplay.UsoDeCartas;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class PausaState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;
        private readonly IFactoriaCarta _factoriaCarta;

        public PausaState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego, IFactoriaCarta factoriaCarta)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
            _factoriaCarta = factoriaCarta;
        }

        public void InitialConfiguration()
        {
            _factoriaCarta.CrearPrimerasCartas();
        }

        public void FinishConfiguration()
        {
            _factoriaCarta.DestruirLasCartas();
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            while (_mediadorDeEstadosDelJuego.EstaPausadoElJuego())
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
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Jugando);
        }
    }
}