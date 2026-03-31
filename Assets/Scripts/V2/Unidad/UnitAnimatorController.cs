using UnityEngine;

namespace V2.Unidad
{
    public class UnitAnimatorController : MonoBehaviour
    {
        [Header("Animator de la unidad")] [SerializeField]
        private Animator animator;

        [Header("Nombres de parámetros/animaciones")] [SerializeField]
        private string idleParam = "Idle";

        [SerializeField] private string walkParam = "Walk";
        [SerializeField] private string deathParam = "Death";
        [SerializeField] private string attackParam = "Attack";

        /// <summary>
        /// Cambia la animación a idle.
        /// </summary>
        public void PlayIdle()
        {
            if (animator != null)
            {
                animator.SetBool(walkParam, false);
                animator.SetBool(idleParam, true);
            }
        }

        /// <summary>
        /// Cambia la animación a caminar.
        /// </summary>
        public void PlayWalk()
        {
            if (animator != null)
            {
                animator.SetBool(idleParam, false);
                animator.SetBool(walkParam, true);
            }
        }

        /// <summary>
        /// Cambia la animación a muerte.
        /// </summary>
        public void PlayDeath()
        {
            if (animator != null)
            {
                animator.SetTrigger(deathParam);
            }
        }

        /// <summary>
        /// Permite setear cualquier parámetro bool del Animator.
        /// </summary>
        public void SetBool(string param, bool value)
        {
            if (animator != null)
                animator.SetBool(param, value);
        }

        /// <summary>
        /// Permite setear cualquier parámetro float del Animator.
        /// </summary>
        public void SetFloat(string param, float value)
        {
            if (animator != null)
                animator.SetFloat(param, value);
        }

        /// <summary>
        /// Permite reproducir un trigger.
        /// </summary>
        public void SetTrigger(string param)
        {
            if (animator != null)
                animator.SetTrigger(param);
        }

        public void PlayAttack()
        {
            if (animator != null)
            {
                // Asumiendo que tenés un trigger llamado "Attack" en tu Animator
                animator.SetTrigger(attackParam);
            }
        }
    }
}