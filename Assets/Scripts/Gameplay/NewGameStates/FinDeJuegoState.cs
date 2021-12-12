using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class FinDeJuegoState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public FinDeJuegoState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            Debug.Log("Estas en estado de Fin de juego");
            _mediadorDeEstadosDelJuego.SalirDelBuclePrincipal();
            await Task.Yield();
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.FinDeJuego);
        }
    }
}