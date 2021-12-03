using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class InteraccionDaniarPersonaje : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            target.health -= _personaje.damage;
            Debug.Log(target.health);
            if (target.health <= 0)
            {
                target.Muerte();
            }
        }
    }
}