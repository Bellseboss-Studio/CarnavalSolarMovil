using System;
using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using Gameplay.UsoDeCartas;
using StatesOfEnemies;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public class ConfiguracionDelJuegoState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;
        private IFactoriaCarta _factoriaCarta;
        private IColocacionCartas _colocacionCartas;
        private CartasConfiguracion _cartasConfiguracion;
        private GameObject _canvasDeLasCartas;

        public ConfiguracionDelJuegoState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego, IFactoriaCarta factoriaCarta, IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
            _factoriaCarta = factoriaCarta;
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            _factoriaCarta.Configurate(_colocacionCartas, _cartasConfiguracion, _canvasDeLasCartas);
            _factoriaCarta.CrearPrimerasCartas();
            Debug.Log("Estas en estado de configurar juego");
            //if (_mediadorDeEstadosDelJuego.SeConfiguroElJuego())
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.SincronizacionDeJugadores);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.ConfiguracionDelJuego);
        }
    }
}