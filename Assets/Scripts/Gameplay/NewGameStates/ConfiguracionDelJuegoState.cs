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
        private FactoriaPersonaje _factoriaPersonaje;

        
        
        public ConfiguracionDelJuegoState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego, IFactoriaCarta factoriaCarta, IColocacionCartas colocacionCartas, CartasConfiguracion cartasConfiguracion, GameObject canvasDeLasCartas, FactoriaPersonaje factoriaPersonaje)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
            _factoriaCarta = factoriaCarta;
            _colocacionCartas = colocacionCartas;
            _cartasConfiguracion = cartasConfiguracion;
            _canvasDeLasCartas = canvasDeLasCartas;
            _factoriaPersonaje = factoriaPersonaje;
        }
        
        public void InitialConfiguration()
        {
            
        }

        public void FinishConfiguration()
        {
            
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            _factoriaCarta.Configurate(_colocacionCartas, _cartasConfiguracion, _canvasDeLasCartas, _factoriaPersonaje);
            //Debug.Log("Estas en estado de configurar juego");
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