using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using ServiceLocatorPath;

namespace Gameplay.NewGameStates
{
    public class ColocandoHeroeState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public ColocandoHeroeState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
        }

        public void InitialConfiguration()
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().ComienzaAContarElTiempo(1);
        }

        public void FinishConfiguration()
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().DejaDeContarElTiempo();
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            while (_mediadorDeEstadosDelJuego.SeCololoElHeroe())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Pausa);
        }
    }
}