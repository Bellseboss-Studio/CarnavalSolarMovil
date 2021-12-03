using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

namespace StatesOfEnemies
{
    public class CreateLobby : IGameState
    {
        private readonly IMediatorGeneral _mediator;

        public CreateLobby(IMediatorGeneral mediator)
        {
            _mediator = mediator;
            Debug.Log("Creamos el loby");
        }

        public IEnumerator DoAction(IBehavior behavior)
        {
            yield return new WaitForSeconds(0.1f);
            while (!ServiceLocator.Instance.GetService<IMultiplayer>().EstaListo())
            {
                yield return new WaitForSeconds(0.1f);
            }

            _mediator.MuestraLasOpcionesParaElJugador();
            
            while (!_mediator.ElegidoSiCrearUnise())
            {
                yield return new WaitForSeconds(0.1f);
            }

            if (_mediator.EligioCrear())
            {
                ServiceLocator.Instance.GetService<IMultiplayer>().CrearSala(_mediator.GetNombreDeSala());
            }
            else
            {
                ServiceLocator.Instance.GetService<IMultiplayer>().UnirseSala(_mediator.GetNombreDeSala());
            }
            _mediator.OcultarOpcionesAlJugador();
            _mediator.MostrarUnPanelDeCarga();
            while (!ServiceLocator.Instance.GetService<IMultiplayer>().TerminoDeProcesarLaSala())
            {
                yield return new WaitForSeconds(0.1f);
            }

            if (ServiceLocator.Instance.GetService<IMultiplayer>().FalloAlgo())
            {
                ServiceLocator.Instance.GetService<IMultiplayer>().ResetFlags();
                _mediator.ResetLaParteDeUnirseCrearSala();
                behavior.SetNextState(GameStatesConfiguration.CreacionDeSalaState);
            }
            else
            {
                behavior.SetNextState(GameStatesConfiguration.EsperaDeSincro);
            }
            _mediator.OcultarPanelDeCarga();
        }
    }
}