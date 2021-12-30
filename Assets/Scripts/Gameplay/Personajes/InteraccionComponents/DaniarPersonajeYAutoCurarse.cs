using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeYAutoCurarse : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            AplicarDanio(target, _personaje.damage);
            _personaje.health += 100;
            Debug.Log(target.health);
        }
    }
}