using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InteraccionComponent : MonoBehaviour, IInteraccionComponent
    {
        private InteraccionBehaviour _interaccionBehaviour;
        public void Configurate(Personaje origen, InteraccionBehaviour interaccionBehaviour)
        {
            _interaccionBehaviour = interaccionBehaviour;
            interaccionBehaviour.Configurate(origen, this);
        }
        
        public void Interactuar(List<Personaje> target)
        {
            _interaccionBehaviour.Interactuar(target);
        }

        public void AplicarInteraccion(Personaje origen)
        {
            _interaccionBehaviour.AplicarInteraccion(origen);
        }

        public void EjecucionDeInteraccion(Personaje target)
        {
            _interaccionBehaviour.EjecucionDeInteraccion(target);
        }

    }

    public interface IInteraccionComponent
    {
        
    }
}