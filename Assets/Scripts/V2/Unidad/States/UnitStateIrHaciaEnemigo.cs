using UnityEngine;

namespace V2.Unidad.States
{
    public class UnitStateIrHaciaEnemigo : IUnitState
    {
        public void Enter(UnitMediator mediator)
        {
            if (mediator.AnimatorController != null)
                mediator.AnimatorController.PlayWalk();
            // Podés poner animación de caminar acá si querés
        }

        public void Update(UnitMediator mediator, float deltaTime)
        {
            var target = mediator.GetTarget();
            if (target != null)
            {
                mediator.MoveTo(target.transform.position);
                float distancia = Vector3.Distance(mediator.transform.position, target.transform.position);
                float rango = mediator.GetStat().DistanciaDeAtaque;
                if (distancia <= rango)
                {
                    mediator.ChangeState(new UnitStateAttack());
                }
            }
            else
            {
                mediator.ChangeState(new UnitStateBuscarEnemigo());
            }
        }

        public void Exit(UnitMediator mediator)
        {
        }
    }
}
