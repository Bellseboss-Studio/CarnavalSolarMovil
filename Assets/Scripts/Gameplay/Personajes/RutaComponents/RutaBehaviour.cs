using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public abstract class RutaBehaviour
    {
        protected NavMeshAgent _navMeshAgent;
        protected Personaje _personaje;
        private IRutaComponent _rutaComponent;
        
        public abstract void DesplazarHaciaElTarget(GameObject personaje);

        public abstract void SetTargetsToNavMesh(List<Personaje> targets);

        public abstract void DejarDeDesplazar();


        public void Configurate(NavMeshAgent navMeshAgent, Personaje personaje, IRutaComponent rutaComponent)
        {
            _navMeshAgent = navMeshAgent;
            _personaje = personaje;
            _rutaComponent = rutaComponent;
        }
    }
}