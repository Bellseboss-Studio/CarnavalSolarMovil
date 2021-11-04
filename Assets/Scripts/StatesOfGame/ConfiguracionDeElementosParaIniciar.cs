using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

namespace StatesOfEnemies
{
    public class ConfiguracionDeElementosParaIniciar : IGameState
    {
        private readonly IMediatorConfiguration _mediator;

        public ConfiguracionDeElementosParaIniciar(IMediatorConfiguration mediator)
        {
            _mediator = mediator;
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            _mediator.ShowLoad();
            while (!ServiceLocator.Instance.GetService<IPlayFabCustom>().IsAllCompleted())
            {
                yield return new WaitForSeconds(0.1f);
            }
            _mediator.HideLoad();
            behavior.SetNextState(GameStatesConfiguration.SeleccionandoPersonajesState);
        }
    }
}