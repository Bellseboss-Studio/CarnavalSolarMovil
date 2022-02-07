using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public class RutaComponent : MonoBehaviour, IRutaComponent
    {
        public RutaBehaviour _rutaBehaviour;
        

        public void DesplazarHaciaElTarget(GameObject personaje)
        {
            _rutaBehaviour.DesplazarHaciaElTarget(personaje);
        }

        public void SetTargetsToNavMesh(List<Personaje> targets)
        {
            _rutaBehaviour.SetTargetsToNavMesh(targets);
        }

        public void DejarDeDesplazar()
        {
            _rutaBehaviour.DejarDeDesplazar();
        }

        public void Configuration(NavMeshAgent navMeshAgent, Personaje personaje, RutaBehaviour rutaBehaviour, SinApply sinApply)
        {
            _rutaBehaviour = rutaBehaviour;
            _rutaBehaviour.Configurate(navMeshAgent, personaje, this, sinApply);
        }

        public void setVelocityToNavMesh()
        {
            _rutaBehaviour.SetVelocityToNavMesh();
        }
    }
}