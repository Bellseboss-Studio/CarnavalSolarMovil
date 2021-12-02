using UnityEngine;

namespace Gameplay
{
    public abstract class InteraccionComponent : MonoBehaviour
    {
        private Personaje _personaje;
        public void Configurate(Personaje origen)
        {
            _personaje = origen;
        }
        
        public void Interactuar(Personaje target)
        {
            target.GetInteractionComponent().AplicarInteraccion(_personaje);
        }

        public void AplicarInteraccion(Personaje origen)
        {
            //behaviour.AplicarInteraccion(origen);
            //Debug.Log(_personaje.name + origen.name);
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
        }

        public abstract void EjecucionDeInteraccion(Personaje target);

    }
}