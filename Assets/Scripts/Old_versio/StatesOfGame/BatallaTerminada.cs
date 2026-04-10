using System.Collections;
using UnityEngine;

namespace StatesOfEnemies
{
    public class BatallaTerminada : IGameState
    {
        private readonly IMediatorConfiguration _mediator;

        public BatallaTerminada(IMediatorConfiguration mediator)
        {
            _mediator = mediator;
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            MxManager.MxInstance.PlayMusicState(GameStatesConfiguration.FinDeBatallaState);
            _mediator.MostrarElLetreroDeGanarOPerder();
            while (!_mediator.EligioLoQueQuiereHacer())
            {
                yield return new WaitForSeconds(0.1f);   
            }
            _mediator.LimpiarPersonajesElejidos();
            _mediator.DestroyPlayers();
            _mediator.RehabilitarMenu();
            
            if (_mediator.QuiereHacerOtraBatalla())
            {
                _mediator.ReiniciaTodosLosEstados();
                behavior.SetNextState(GameStatesConfiguration.SeleccionandoPersonajesState);
            }
            else
            {
                behavior.SetNextState(-1);
            }
            _mediator.OcultarElLetreroDeGanarOPerder();
        }
    }
}