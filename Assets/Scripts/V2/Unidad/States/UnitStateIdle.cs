namespace V2.Unidad.States
{
    public class UnitStateIdle : IUnitState
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
            if (_timer > 2f)
            {
                mediator.ChangeState(new UnitStateBuscarEnemigo());
            }
        }

        public void Exit(UnitMediator mediator)
        {
            // Cleanup si hace falta
        }
    }
}
