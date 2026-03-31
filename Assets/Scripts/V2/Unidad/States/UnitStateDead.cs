namespace V2.Unidad.States
{
    public class UnitStateDead : IUnitState
    {
        public void Enter(UnitMediator mediator)
        {
            if (mediator.AnimatorController != null)
                mediator.AnimatorController.PlayDeath();
            if (mediator.AIController != null)
                mediator.AIController.enabled = false;
            if (mediator.Movement != null)
                mediator.Movement.enabled = false;
            if (mediator.StatsController != null)
                mediator.StatsController.enabled = false;
            if (mediator.HealthController != null)
                mediator.HealthController.enabled = false;
            // Podés agregar lógica para desactivar colisionadores, etc.
        }

        public void Update(UnitMediator mediator, float deltaTime)
        {
            // No hace nada estando muerto
        }

        public void Exit(UnitMediator mediator)
        {
            // No debería salir de este estado
        }
    }
}
