using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class AumentarVelocidadDeAtaqueYDeMovimientoDeAliadosCercanosAlMorir : InteraccionBehaviour
    {
        public override void AplicarInteraccion(Personaje origen)
        {
            //behaviour.AplicarInteraccion(origen);
            //Debug.Log(_personaje.name + origen.name);
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
            if (_personaje.health <= 0)
            {
                var aliadosCercanos = _interaccionComponent.GetAliadosCercanos(_personaje);
                foreach (var aliadoCercano in aliadosCercanos)
                {
                    aliadoCercano.velocidadDeInteraccion *= 0.8f;
                    aliadoCercano.velocidadDeMovimiento *= 1.2f;
                }
                _personaje.Muerte();
            }
        }
        
    }
}