using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class InteraccionDaniarPersonaje : InteraccionComponent
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            target.health -= 2;
            Debug.Log(target.health);
            if (target.health <= 0)
            {
                target.Muerte();
            }
        }
    }
}