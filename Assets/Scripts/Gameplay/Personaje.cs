using System;
using Gameplay.PersonajeStates;
using UnityEngine;

namespace Gameplay
{
    public abstract class Personaje : MonoBehaviour
    {
        private TargetComponent _targetComponent;
        private DistanciaComponent _distanciaComponent;
        private InteraccionComponent _interaccionComponent;
        private RutaComponent _rutaComponent;
        private float _velocidadDeInteraccion;
        private PersonajeStatesConfiguration _personajeStatesConfiguration;
        public float health = 10;
        public bool enemigo;

        private void Awake()
        {
            _personajeStatesConfiguration = new PersonajeStatesConfiguration();
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.BuscarTargetState, new BuscarTargetState());
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.DesplazarseHaciaElTargetState, new DesplazarseHaciaElTargetState());
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.InteractuarConElTargetState, new InteractuarConElTargetState());
        }

        private void Start()
        {
            _interaccionComponent.Configurate(this);
            _personajeStatesConfiguration.GetState(1);
        }

        public void SetComponents(TargetComponent targetComponent, DistanciaComponent distanciaComponent, InteraccionComponent interaccionComponent, RutaComponent rutaComponent, float velocidadDeInteraccion)
        {
            _targetComponent = Instantiate(targetComponent, transform);
            _distanciaComponent = Instantiate(distanciaComponent, transform);
            _interaccionComponent = Instantiate(interaccionComponent, transform);
            //_rutaComponent = Instantiate(rutaComponent, transform);
            _velocidadDeInteraccion = velocidadDeInteraccion;
            _targetComponent.Configuracion(this);
        }

        public InteraccionComponent GetInteractionComponent()
        {
            return _interaccionComponent;
        }

        public TargetComponent GetTargetComponent()
        {
            return _targetComponent;
        }
    }
}