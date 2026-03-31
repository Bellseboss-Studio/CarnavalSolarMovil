using UnityEngine;
using V2.Unidad;
using V2.Unidad.States;

namespace V2.Unidad
{
    /// <summary>
    /// Clase base para todas las unidades del juego. Centraliza la comunicación entre los distintos controladores de la unidad.
    /// Heredá de esta clase para crear unidades personalizadas.
    /// </summary>
    public class UnitMediator : MonoBehaviour
    {
        [Header("Referencias de controladores")] [SerializeField]
        protected UnitMovement movement;

        [SerializeField] protected UnitAnimatorController animatorController;
        [SerializeField] protected UnitHealthController healthController;
        [SerializeField] protected UnitStatsController statsController;
        [SerializeField] protected UnitAIController aiController;

        [Header("Target actual de la unidad")] [SerializeField]
        protected Transform target;

        private bool _wasMoving;

        [Header("Estado actual de la unidad")] [SerializeField]
        protected string currentStateName;

        protected IUnitState CurrentState;
        [SerializeField] protected float stateTimer;
        protected IUnitState NextState;

        #region Unity Events

        protected virtual void Awake()
        {
            // Inicialización de referencias si es necesario
            CurrentState = new UnitStateIdle();
            currentStateName = CurrentState.GetType().Name;
            CurrentState.Enter(this);
            // Por defecto, usa el buscador simple que retorna el target actual
            _enemyTargetFinder = new TargetEnemyFinder();
        }

        protected virtual void Start()
        {
            Configure();
            if (target != null)
            {
                MoveTo(target.position);
            }
        }

        void Update()
        {
            HandleAnimation();
            float deltaTime = Time.deltaTime;
            stateTimer += deltaTime;
            if (CurrentState != null)
            {
                CurrentState.Update(this, deltaTime);
            }

            if (NextState != null)
            {
                // Cambio de estado post-Update para evitar problemas de stack
                if (CurrentState != null)
                    CurrentState.Exit(this);
                CurrentState = NextState;
                currentStateName = CurrentState.GetType().Name;
                stateTimer = 0f;
                CurrentState.Enter(this);
                NextState = null;
            }

            OnUnitUpdate();

            // Transición automática a Dead si la vida llega a 0
            if (healthController != null && healthController.GetCurrentHp() <= 0 && !(CurrentState is UnitStateDead))
            {
                ChangeState(new UnitStateDead());
            }
        }

        #endregion

        #region Movimiento

        /// <summary>
        /// Mueve la unidad a la posición indicada.
        /// </summary>
        public virtual void MoveTo(Vector3 position)
        {
            if (movement != null)
                movement.MoveTo(position);
        }

        #endregion

        #region Salud

        /// <summary>
        /// Aplica daño a la unidad. Sobrescribir para lógica personalizada.
        /// </summary>
        public virtual void TakeDamage(float amount)
        {
            if (healthController != null)
                healthController.TakeDamage(amount);
        }

        /// <summary>
        /// Lógica de muerte de la unidad. Sobrescribir para efectos personalizados.
        /// </summary>
        public virtual void Die()
        {
            ChangeState(new UnitStateDead());
        }

        #endregion

        #region Animación

        /// <summary>
        /// Ejecuta una animación específica.
        /// </summary>
        public virtual void PlayAnimation(string animationName)
        {
            // Ejemplo: animatorController.Play(animationName);
        }

        #endregion

        #region Stats

        #endregion

        #region IA

        /// <summary>
        /// Ejecuta una acción de IA personalizada.
        /// </summary>
        public virtual void ExecuteAI()
        {
            // Ejemplo: aiController.Execute();
        }

        #endregion

        #region Targeting

        /// <summary>
        /// Cambia el objetivo actual de la unidad.
        /// </summary>
        public virtual void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public virtual UnitMediator GetTarget()
        {
            return target != null ? target.GetComponent<UnitMediator>() : null;
        }

        #endregion

        #region Eventos

        // Ejemplo de eventos para notificar cambios de estado
        public event UnitEvent OnDeath;
        public event UnitEvent OnDamage;

        protected void RaiseOnDeath()
        {
            OnDeath?.Invoke(this);
        }

        protected void RaiseOnDamage()
        {
            OnDamage?.Invoke(this);
        }

        #endregion

        #region Update

        /// <summary>
        /// Detecta si la unidad está en movimiento y actualiza la animación.
        /// </summary>
        protected virtual void HandleAnimation()
        {
            // Ahora las animaciones las controla la máquina de estados (en cada estado concreto)
        }

        /// <summary>
        /// Hook para lógica personalizada por frame en subclases.
        /// </summary>
        protected virtual void OnUnitUpdate()
        {
        }

        /// <summary>
        /// Cambia el estado de la unidad (nuevo sistema desacoplado).
        /// </summary>
        public void ChangeState(IUnitState newState)
        {
            if (newState == null || (CurrentState != null && CurrentState.GetType() == newState.GetType()))
                return;
            NextState = newState;
        }

        #endregion

        private IEnemyTargetFinder _enemyTargetFinder;

        public void SetEnemyTargetFinder(IEnemyTargetFinder finder)
        {
            _enemyTargetFinder = finder;
        }

        public Transform BuscarEnemigoCercano()
        {
            // Utiliza el buscador inyectado si existe, sino fallback a target
            if (_enemyTargetFinder != null)
                return _enemyTargetFinder.BuscarEnemigoCercano(this);
            return target; // Fallback simple
        }

        public UnitAnimatorController AnimatorController => animatorController;
        public UnitHealthController HealthController => healthController;
        public UnitAIController AIController => aiController;
        public UnitMovement Movement => movement;
        public UnitStatsController StatsController => statsController;

        /// <summary>
        /// Configura la unidad con los stats dados por el ScriptableObject.
        /// </summary>
        public virtual void Configure()
        {
            if (healthController != null)
                healthController.SetHp(statsController.GetStat().HP);

            // Conectar eventos de muerte y daño
            if (healthController != null)
            {
                healthController.OnDeath += () =>
                {
                    Die();
                    RaiseOnDeath();
                };
                healthController.OnDamage += (float dmg) => { RaiseOnDamage(); };
            }
        }

        public UnitStatsData GetStat()
        {
            return statsController.GetStat();
        }
    }
}