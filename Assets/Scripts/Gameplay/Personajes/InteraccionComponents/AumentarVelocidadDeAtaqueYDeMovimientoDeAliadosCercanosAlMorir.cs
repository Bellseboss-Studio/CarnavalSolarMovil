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
                    _interaccionComponent.AumentarVelocidadMovimientoPersonajePorSegundos(aliadoCercano, 50, 5);
                    _interaccionComponent.DisminuirVelocidadInteraciconPersonajePorSegundos(aliadoCercano, 50, 5);
                }
                _personaje.Muerte();
            }
        }
        
    }
}