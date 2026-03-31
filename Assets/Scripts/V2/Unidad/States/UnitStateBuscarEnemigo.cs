using UnityEngine;

namespace V2.Unidad.States
{
    public class UnitStateBuscarEnemigo : IUnitState
    {
        private float _timer;

        public void Enter(UnitMediator mediator)
        {
            _timer = 0f;
            if (mediator.AnimatorController != null)
                mediator.AnimatorController.PlayIdle();
        }

        public void Update(UnitMediator mediator, float deltaTime)
        {
            _timer += deltaTime;
            Transform enemigo = mediator.BuscarEnemigoCercano();
            if (enemigo != null)
            {
                mediator.SetTarget(enemigo);
                mediator.ChangeState(new UnitStateIrHaciaEnemigo());
            }
            else if (_timer > 2f)
            {
                mediator.ChangeState(new UnitStateIdle());
            }
        }

        public void Exit(UnitMediator mediator)
        {
        }
    }
}