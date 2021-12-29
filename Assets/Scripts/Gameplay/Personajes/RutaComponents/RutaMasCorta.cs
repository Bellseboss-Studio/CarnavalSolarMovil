using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public class RutaMasCorta : RutaBehaviour
    {
        public override void DesplazarHaciaElTarget(GameObject personaje)
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.enabled = true;
                if (!_personaje.gameObject.activeSelf) return;
                _navMeshAgent.destination = personaje.transform.position;
                _navMeshAgent.speed = _personaje.velocidadDeMovimiento * (_personaje.escudo == 0 ? 1 : _personaje.escudo);
            }
        }

        public override void SetTargetsToNavMesh(List<Personaje> targets)
        {
            _navMeshAgent.enabled = true;
            if (!_personaje.gameObject.activeSelf) return;
            _navMeshAgent.destination = targets[0].transform.position;
            _navMeshAgent.stoppingDistance = _personaje.distanciaDeInteraccion;
            _navMeshAgent.speed = _personaje.velocidadDeMovimiento * (_personaje.escudo == 0 ? 1 : _personaje.escudo);
        }

        public override void DejarDeDesplazar()
        {
            
           if (_navMeshAgent!= null) _navMeshAgent.enabled = false;
        }
    }
}