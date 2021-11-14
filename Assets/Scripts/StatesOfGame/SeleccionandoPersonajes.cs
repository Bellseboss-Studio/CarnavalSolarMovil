using System.Collections;
using UnityEngine;

namespace StatesOfEnemies
{
    public class SeleccionandoPersonajes : IGameState
    {
        private readonly IMediatorGeneral _mediatorGeneral;

        public SeleccionandoPersonajes(IMediatorGeneral mediatorGeneral)
        {
            _mediatorGeneral = mediatorGeneral;
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            _mediatorGeneral.ShowStore();
            
            while (!_mediatorGeneral.TerminoDeElegir)
            {
                yield return new WaitForSeconds(0.1f);
            }
            behavior.SetNextState(GameStatesConfiguration.EsperaDeSincro);
            _mediatorGeneral.HideStore();
            yield return new WaitForSeconds(0.1f);
        }
    }
}