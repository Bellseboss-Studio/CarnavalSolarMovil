using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Personajes.RutaComponents;
using UnityEngine;

namespace Gameplay.PersonajeStates
{
    public class DesplazarseHaciaElTargetState : IPersonajeState
    {
        private Personaje _personaje;
        private readonly RutaComponent _rutaComponent;
        private bool seDesplazo = false;

        public DesplazarseHaciaElTargetState(Personaje personaje, RutaComponent rutaComponent)
        {
            _personaje = personaje;
            _rutaComponent = rutaComponent;
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            var targets = _personaje.GetTargetComponent().GetTargets();
            //Debug.Log($"{_personaje.cosaDiferenciadora} {_personaje.transform.position} + {targets[0].cosaDiferenciadora} {targets[0].gameObject.transform.position}");
            float distanciaEntrePersonajes = Vector3.Distance(_personaje.transform.position, targets[0].gameObject.transform.position);
            if(distanciaEntrePersonajes > _personaje.distanciaDeInteraccion && targets.Count > 0 && _personaje != null && targets[0] != null) _rutaComponent.SetTargetsToNavMesh(targets);
            while (targets.Count > 0 && _personaje.isTargeteable && targets[0] != null && targets[0].isTargeteable && distanciaEntrePersonajes > _personaje.distanciaDeInteraccion)
            {
                //Debug.Log("estas en el estado desplazarse");
                _rutaComponent.SetTargetsToNavMesh(targets);
                distanciaEntrePersonajes = Vector3.Distance(_personaje.transform.position, targets[0].transform.position);
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                //Debug.Log(distanciaEntrePersonajes);
                seDesplazo = true;
            }
            if(seDesplazo) _rutaComponent.DejarDeDesplazar();
            seDesplazo = false;
            return new PersonajeStateResult(PersonajeStatesConfiguration.InteractuarConElTargetState, targets);
        }
    }
}