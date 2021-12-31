using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeYReflejarDanio : InteraccionBehaviour
    {
        public override void AplicarInteraccion(Personaje origen)
        {
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
            if (origen == null) return;
            AplicarDanio(origen,origen.damage);
            origen.ActualizarBarraDeVida();
            if (origen.health <= 0)
            {
                origen.Muerte();
            }
            if (_personaje.health <= 0)
            {
                _personaje.Muerte();
            }
        }
    }
}