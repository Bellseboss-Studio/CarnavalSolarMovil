using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AtacarYReducirVelocidadDeMovimientoYAtaqueAlObjetivo : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            _interaccionComponent.ReducirVelocidadDeMovimientoYAtaqueAlObjetivo(target, 20, _personaje.velocidadDeInteraccion);
            AplicarDanio(target, _personaje.damage);
            Debug.Log(target.health);
        }
    }
}