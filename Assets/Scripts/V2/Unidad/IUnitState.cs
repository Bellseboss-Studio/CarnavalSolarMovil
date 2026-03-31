using UnityEngine;

namespace V2.Unidad
{
    /// <summary>
    /// Interfaz para los estados de la unidad.
    /// </summary>
    public interface IUnitState
    {
        void Enter(UnitMediator mediator);
        void Update(UnitMediator mediator, float deltaTime);
        void Exit(UnitMediator mediator);
    }
}

