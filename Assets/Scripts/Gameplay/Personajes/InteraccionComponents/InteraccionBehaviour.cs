using System.Collections.Generic;

namespace Gameplay
{
    public abstract class InteraccionBehaviour
    {
        
        protected Personaje _personaje;
        private IInteraccionComponent _interaccionComponent;

        public abstract void EjecucionDeInteraccion(Personaje target);

        public void AplicarInteraccion(Personaje origen)
        {
            //behaviour.AplicarInteraccion(origen);
            //Debug.Log(_personaje.name + origen.name);
            origen.GetInteractionComponent().EjecucionDeInteraccion(_personaje);
        }


        public void Interactuar(List<Personaje> target)
        {
            target[0].GetInteractionComponent().AplicarInteraccion(_personaje);
        }

        public void Configurate(Personaje personaje, IInteraccionComponent interaccionComponent)
        {
            _personaje = personaje;
            _interaccionComponent = interaccionComponent;
        }
        
    }
}