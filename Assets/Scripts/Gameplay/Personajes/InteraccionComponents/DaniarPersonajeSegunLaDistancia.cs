using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeSegunLaDistancia : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            var distanciaOrigenTarget = _interaccionComponent.ObtenerDistancia(_personaje, target);
            var danioModificado = _personaje.damage + 50/distanciaOrigenTarget;
            AplicarDanio(target, danioModificado);
            Debug.Log(target.health);
        }
    }
}