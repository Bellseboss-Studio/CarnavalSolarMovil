using UnityEngine;
using UnityEngine.AI;

namespace V2.Unidad
{
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        public Vector3 destination;
        private UnitMediator _mediator;

        public void Configure(UnitMediator mediator)
        {
            _mediator = mediator;
        }

        [ContextMenu("Agregar")]
        public void Agregar()
        {
            agent.destination = destination;
        }

        private void Reset()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void MoveTo(Vector3 position)
        {
            if (_mediator == null) return;
            agent.speed = _mediator.GetStat().MovementVelocity;
            agent.SetDestination(position);
        }

        public void Stop()
        {
            if (agent == null) return;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }

        public void SetAutoRotation(bool isEnabled)
        {
            if (agent == null) return;
            agent.updateRotation = isEnabled;
        }

        /// <summary>
        /// Devuelve la velocidad actual del agente. Retorna true si el agente existe.
        /// </summary>
        public bool TryGetAgentSpeed(out float speed)
        {
            speed = 0f;
            if (agent != null)
            {
                speed = agent.velocity.magnitude;
                return true;
            }

            return false;
        }
    }
}