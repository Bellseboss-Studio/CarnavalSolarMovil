using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class PersonajeBuilder
    {
        private Personaje _personaje;
        private RutaBehaviour _rutaComponent;
        private TargetBehaviour _targetBehaviour;
        private InteraccionBehaviour _interaccionBehaviour;
        private Vector3 _position;
        private GameObject _prefab;
        private EstadisticasCarta _estadisticasCarta;

        public PersonajeBuilder WithPersonaje(Personaje personaje)
        {
            _personaje = personaje;
            return this;
        }

        public PersonajeBuilder WithTargetComponent(TargetBehaviour targetComponent)
        {
            _targetBehaviour = targetComponent;
            return this;
        }

        public PersonajeBuilder WithInteraccionComponent(InteraccionBehaviour interaccionBehaviour)
        {
            _interaccionBehaviour = interaccionBehaviour;
            return this;
        }
        
        public PersonajeBuilder WithRutaComponent(RutaBehaviour rutaComponent)
        {
            _rutaComponent = rutaComponent;
            return this;
        }

        public PersonajeBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        
        public PersonajeBuilder With3DObject(GameObject gameObject)
        {
            _prefab = gameObject;
            return this;
        }

        public PersonajeBuilder WithEstadisticasCarta(EstadisticasCarta estadisticasCarta)
        {
            _estadisticasCarta = estadisticasCarta;
            return this;
        }
        
        public Personaje Build()
        {
            var personaje = Object.Instantiate(_personaje);
            personaje.SetComponents(_targetBehaviour, _interaccionBehaviour, _rutaComponent, _estadisticasCarta, _prefab);
            personaje.transform.position = _position;
            return personaje;
        }
    }
}