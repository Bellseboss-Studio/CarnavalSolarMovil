using System;
using Gameplay.Personajes.InteraccionComponents;
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

        public PersonajeBuilder WithTargetComponent(string targetComponentId)
        {
            switch (targetComponentId)
            {
                case "buscarEnemigoMasCercano":
                    _targetBehaviour = new BuscarEnemigoMasCercano();
                    break;
                
                case "buscarTresEnemigosMasCercanos":
                    _targetBehaviour = new BuscartresEnemigosMasCercanos();
                    break;
                
                default:
                    throw new Exception($"El componente con la id {targetComponentId} no existe");
            }
            return this;
        }

        public PersonajeBuilder WithInteraccionComponent(string interaccionBehaviourId)
        {
            switch (interaccionBehaviourId)
            {
                case "DaniarPersonaje":
                    _interaccionBehaviour = new DaniarPersonaje();
                    break;

                case "atacarConDanioProgresivo":
                    _interaccionBehaviour = new AtacarConDanioProgresivo();
                    break;
                
                case "daniarPersonajeSegunLaDistancia":
                    _interaccionBehaviour = new DaniarPersonajeSegunLaDistancia();
                    break;
                
                case "daniarPersonajeYAutoCurarse":
                    _interaccionBehaviour = new DaniarPersonajeYAutoCurarse();
                    break;
                
                case "daniarPersonajeYReflejarDanio":
                    _interaccionBehaviour = new DaniarPersonajeYReflejarDanio();
                    break;

                default:
                    throw new Exception($"El componente con la id {interaccionBehaviourId} no existe");
            }
            return this;
        }
        
        public PersonajeBuilder WithRutaComponent(string rutaComponentId)
        {
            switch (rutaComponentId)
            {
                case "rutaMasCorta":
                    _rutaComponent = new RutaMasCorta();
                    break;
                default:
                    throw new Exception($"El componente con la id {rutaComponentId} no existe");
                break;
            }
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