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
            mediator.Movement?.Stop();
            mediator.Movement?.SetAutoRotation(false);

            var target = mediator.GetTarget();
            if (target != null)
            {
                _targetMediator = target;
                LookAtTarget(mediator, _targetMediator);
            }

            if (mediator.AnimatorController != null)
                mediator.AnimatorController.PlayAttack();
        }

        public void Update(UnitMediator mediator, float deltaTime)
        {
            _timer += deltaTime;
            if (_targetMediator == null || _targetMediator.HealthController.GetCurrentHp() <= 0)
            {
                mediator.ChangeState(new UnitStateBuscarEnemigo());
                return;
            }

            LookAtTarget(mediator, _targetMediator);

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
            mediator.Movement?.SetAutoRotation(true);
        }

        private void LookAtTarget(UnitMediator mediator, UnitMediator target)
        {
            Vector3 direction = target.transform.position - mediator.transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                mediator.transform.rotation = targetRotation;
            }
        }
    }
}