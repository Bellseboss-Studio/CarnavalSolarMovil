using UnityEngine;

namespace Gameplay.Personajes.InteraccionComponents
{
    public class DaniarPersonajeYReflejarDanio : InteraccionBehaviour
    {
        public override void AplicarInteraccion(Personaje origen)
        {
            //behaviour.AplicarInteraccion(origen);
            //Debug.Log(_personaje.name + origen.name);
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
            if (origen == null) return;
            AplicarDanio(origen,origen.damage * .2f);
            Debug.Log(origen.health);
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