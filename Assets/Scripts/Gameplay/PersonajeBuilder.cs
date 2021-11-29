using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class PersonajeBuilder
    {
        private Personaje _personaje;
        private DistanciaComponent _distanciaComponent;
        private RutaComponent _rutaComponent;
        private TargetComponent _targetComponent;
        private InteraccionComponent _interaccionComponent;
        private float _velocidadDeInteraccion;

        public PersonajeBuilder WithPersonaje(Personaje personaje)
        {
            _personaje = personaje;
            return this;
        }

        public PersonajeBuilder WithTargetComponent(TargetComponent targetComponent)
        {
            _targetComponent = targetComponent;
            return this;
        }

        public PersonajeBuilder WithDistanciaComponent(DistanciaComponent distanciaComponent)
        {
            _distanciaComponent = distanciaComponent;
            return this;
        }
        
        public PersonajeBuilder WithInteraccionComponent(InteraccionComponent interaccionComponent)
        {
            _interaccionComponent = interaccionComponent;
            return this;
        }
        
        public PersonajeBuilder WithRutaComponent(RutaComponent rutaComponent)
        {
            _rutaComponent = rutaComponent;
            return this;
        }
        
        public PersonajeBuilder WithVelocidadDeInteraccion(float velocidadDeInteraccion)
        {
            _velocidadDeInteraccion = velocidadDeInteraccion;
            return this;
        }
        
        public Personaje Build()
        {
            var personaje = Object.Instantiate(_personaje);
            personaje.SetComponents(_targetComponent, _distanciaComponent, _interaccionComponent, _rutaComponent, _velocidadDeInteraccion);
            return personaje;
        }
    }
}