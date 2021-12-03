using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public abstract class RutaComponent : MonoBehaviour
    {
        protected Transform target;
        protected NavMeshAgent _navMeshAgent;
        protected Personaje _personaje;

        public abstract void DesplazarHaciaElTarget(GameObject personaje);

        public abstract void SetTargetsToNavMesh(List<Personaje> targets);

        public abstract void DejarDeDesplazar();

        public void Configuration(NavMeshAgent navMeshAgent, Personaje personaje)
        {
            _navMeshAgent = navMeshAgent;
            _personaje = personaje;
        }
    }
}