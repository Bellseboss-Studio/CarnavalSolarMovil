using UnityEngine;

namespace Gameplay
{
    public class InteraccionDaniarPersonaje : InteraccionComponent
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            Debug.Log(target.health);
            target.health -= 2;
            Debug.Log(target.health);
        }
    }
}