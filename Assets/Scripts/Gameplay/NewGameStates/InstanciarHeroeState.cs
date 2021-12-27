using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using ServiceLocatorPath;

namespace Gameplay.NewGameStates
{
    public class InstanciarHeroeState : IEstadoDeJuego
    {
        private readonly IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public InstanciarHeroeState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
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
            await Task.Delay(TimeSpan.FromMilliseconds(1.00));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Pausa);
        }
    }
}