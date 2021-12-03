using System;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using Gameplay.PersonajeStates;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public abstract class Personaje : MonoBehaviour
    {
        [SerializeField] private TargetComponent _targetComponent;
        private InteraccionComponent _interaccionComponent;
        private RutaComponent _rutaComponent;
        public float _velocidadDeInteraccion;
        private PersonajeStatesConfiguration _personajeStatesConfiguration;
        public float health = 10;
        public bool enemigo;
        public bool laPartidaEstaCongelada = false;
        private bool estaVivo = true;
        public bool isTargeteable = true;
        public float distanciaDeInteraccion;

        private void Awake()
        {
            
        }

        private void Start()
        {
            _interaccionComponent.Configurate(this);
            StartState(_personajeStatesConfiguration.GetState(0));
        }

        public void SetComponents(TargetComponent targetComponent, InteraccionComponent interaccionComponent, RutaComponent rutaComponent, float velocidadDeInteraccion)
        {
            _targetComponent = Instantiate(targetComponent, transform);
            _interaccionComponent = Instantiate(interaccionComponent, transform);
            _rutaComponent = Instantiate(rutaComponent, transform);
            _velocidadDeInteraccion = velocidadDeInteraccion;
            _targetComponent.Configuracion(this);
            _rutaComponent.Configuration(GetComponent<NavMeshAgent>(), this);
            _personajeStatesConfiguration = new PersonajeStatesConfiguration();
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.CongeladoState, new CongeladoState(this));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.BuscarTargetState, new BuscarTargetState(_targetComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.DesplazarseHaciaElTargetState, new DesplazarseHaciaElTargetState(this, _rutaComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.InteractuarConElTargetState, new InteractuarConElTargetState(this));
        }

        private async void StartState(IPersonajeState state, object data = null)
        {
            while (estaVivo && isTargeteable)
            {
                var resultData = await state.DoAction(data);
                var nextState = _personajeStatesConfiguration.GetState(resultData.NextStateId);
                state = nextState;
                data = resultData.ResultData;
            }
        }
        
        public InteraccionComponent GetInteractionComponent()
        {
            return _interaccionComponent;
        }

        public TargetComponent GetTargetComponent()
        {
            return _targetComponent;
        }

        public bool LaPartidaEstaCongelada()
        {
            return laPartidaEstaCongelada;
        }

        public Vector3 GetCharacterPosition()
        {
            return transform.position;
        }

        public abstract void Muerte();

        private void OnDestroy()
        {
            estaVivo = false;
        }
    }
}