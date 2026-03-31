using UnityEngine;
using UnityEngine.AI;

namespace V2.Unidad
{
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        public Vector3 destination;

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
            agent.SetDestination(position);
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