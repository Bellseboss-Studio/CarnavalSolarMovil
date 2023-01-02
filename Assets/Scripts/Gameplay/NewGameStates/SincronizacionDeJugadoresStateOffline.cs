using System;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using ServiceLocatorPath;

namespace Gameplay.NewGameStates
{
    public class SincronizacionDeJugadoresStateOffline : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;

        public SincronizacionDeJugadoresStateOffline(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego)
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
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.ColocandoHeroe);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores);
        }
    }
}