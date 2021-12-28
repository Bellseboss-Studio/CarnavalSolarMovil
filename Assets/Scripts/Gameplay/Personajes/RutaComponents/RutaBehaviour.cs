using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Personajes.RutaComponents
{
    public abstract class RutaBehaviour
    {
        protected NavMeshAgent _navMeshAgent;
        protected Personaje _personaje;
        protected SinApply _sinApply;
        private IRutaComponent _rutaComponent;
        
        public abstract void DesplazarHaciaElTarget(GameObject personaje);

        public abstract void SetTargetsToNavMesh(List<Personaje> targets);

        public abstract void DejarDeDesplazar();


        public void Configurate(NavMeshAgent navMeshAgent, Personaje personaje, IRutaComponent rutaComponent, SinApply sinApply)
        {
            _sinApply = sinApply;
            _navMeshAgent = navMeshAgent;
            _personaje = personaje;
            _rutaComponent = rutaComponent;
        }

        public void ConfigureSinApply(GameObject nuevoTargetGameObject)
        {
            _sinApply.Configure(nuevoTargetGameObject);
        }
    }
}