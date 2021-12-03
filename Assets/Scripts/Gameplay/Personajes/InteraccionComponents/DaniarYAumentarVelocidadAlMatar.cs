using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarYAumentarVelocidadAlMatar : InteraccionBehaviour
    {
        public override void EjecucionDeInteraccion(Personaje target)
        {
            if (target == null) return;
            target.health -= _personaje.damage;
            Debug.Log(target.health);
            if (target.health <= 0)
            {
                _personaje.velocidadDeMovimiento += (_personaje.velocidadDeMovimiento * .2f);
                target.Muerte();
            }
        }
    }
}