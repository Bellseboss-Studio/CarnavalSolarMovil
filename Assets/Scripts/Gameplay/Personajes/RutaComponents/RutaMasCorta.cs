using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public class RutaMasCorta : RutaComponent
    {
        public override void DesplazarHaciaElTarget(GameObject personaje)
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.destination = personaje.transform.position;
            }
        }

        public override void SetTargetsToNavMesh(List<Personaje> targets)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.destination = targets[0].transform.position;
            _navMeshAgent.stoppingDistance = _personaje.distanciaDeInteraccion;
        }

        public override void DejarDeDesplazar()
        {
            
           if (_navMeshAgent!= null) _navMeshAgent.enabled = false;
        }
        
    }
}