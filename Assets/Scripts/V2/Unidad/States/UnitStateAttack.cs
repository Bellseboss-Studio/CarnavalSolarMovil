using UnityEngine;

namespace V2.Unidad.States
{
    public class UnitStateAttack : IUnitState
    {
        private float _attackCooldown;
        private float _timer;
        private UnitMediator _targetMediator;

        public void Enter(UnitMediator mediator)
        {
            _timer = 0f;
            _attackCooldown = mediator.GetStat().CoolDownAttack;
            if (mediator.AnimatorController != null)
                mediator.AnimatorController.PlayAttack();
            // Buscar el mediador del objetivo
            var target = mediator.GetTarget();
            if (target != null)
                _targetMediator = target;
        }

        public void Update(UnitMediator mediator, float deltaTime)
        {
            _timer += deltaTime;
            if (_targetMediator == null || _targetMediator.HealthController.GetCurrentHp() <= 0)
            {
                mediator.ChangeState(new UnitStateBuscarEnemigo());
                return;
            }

            float distancia = Vector3.Distance(mediator.transform.position, _targetMediator.transform.position);
            float rango = mediator.GetStat().DistanciaDeAtaque;
            if (distancia > rango)
            {
                mediator.ChangeState(new UnitStateIrHaciaEnemigo());
                return;
            }

            if (_timer >= _attackCooldown)
            {
                float damage = mediator.GetStat().Ataque;
                _targetMediator.HealthController.TakeDamage(damage);
                if (mediator.AnimatorController != null)
                    mediator.AnimatorController.PlayAttack();
                _timer = 0f;
            }
        }

        public void Exit(UnitMediator mediator)
        {
            // Nada por ahora
        }
    }
}