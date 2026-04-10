using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AtacarConDanioProgresivo : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            _interaccionComponent.AplicarDanioProgresivamente(_personaje, target, _personaje.damage, 3, 1);
            //Debug.Log(target.health);
        }
    }
}