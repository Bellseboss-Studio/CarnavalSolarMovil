using System;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using Gameplay.PersonajeStates;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    
    [RequireComponent(typeof(TargetComponent),typeof(RutaComponent), typeof(InteraccionComponent))]
    
    public class Personaje : MonoBehaviour
    {
        [SerializeField] private TargetComponent _targetComponent;
        [SerializeField] private InteraccionComponent _interaccionComponent;
        [SerializeField] private RutaComponent _rutaComponent;
        private PersonajeStatesConfiguration _personajeStatesConfiguration;
        public bool enemigo;
        public bool laPartidaEstaCongelada = false;
        private bool estaVivo = true;
        public bool isTargeteable = true;
        public string cosaDiferenciadora;
        public float distanciaDeInteraccion;
        public float health = 10;
        public float velocidadDeInteraccion;
        public float velocidadDeMovimiento;
        public float damage;
        public float escudo;
        private EstadisticasCarta _estadisticasCarta;
        private void Awake()
        {
            
        }

        private void Start()
        {
        }

        public void SetComponents(TargetBehaviour targetBehaviour, InteraccionBehaviour interaccionBehaviour, RutaBehaviour rutaBehaviour, EstadisticasCarta estadisticasCarta, GameObject prefab)
        {
            var _prefabInstanciado = Instantiate(prefab, transform);
            velocidadDeInteraccion = estadisticasCarta.VelocidadDeInteraccion;
            health = estadisticasCarta.Health;
            velocidadDeMovimiento = estadisticasCarta.VelocidadDeMovimiento;
            distanciaDeInteraccion = estadisticasCarta.DistanciaDeInteraccion;
            damage = estadisticasCarta.Damage;
            _targetComponent.Configuracion(targetBehaviour, this);
            _rutaComponent.Configuration(GetComponent<NavMeshAgent>(), this, rutaBehaviour);
            _interaccionComponent.Configurate(this, interaccionBehaviour);
            _personajeStatesConfiguration = new PersonajeStatesConfiguration();
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.CongeladoState, new CongeladoState(this));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.BuscarTargetState, new BuscarTargetState(_targetComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.DesplazarseHaciaElTargetState, new DesplazarseHaciaElTargetState(this, _rutaComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.InteractuarConElTargetState, new InteractuarConElTargetState(this));
            StartState(_personajeStatesConfiguration.GetState(0));
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

        public void Muerte()
        {
            GetTargetComponent().DejarDeSerTargeteado(this);
            GetTargetComponent().HeDejadoDeTargetear();
            isTargeteable = false;
        }

        private void OnDestroy()
        {
            estaVivo = false;
        }
    }

    public class EstadisticasCarta
    {
        private float _distanciaDeInteraccion;
        private float _health;
        private float _velocidadDeInteraccion;
        private float _velocidadDeMovimiento;
        private float _damage;
        private float _escudo;

        public EstadisticasCarta(float distanciaDeInteraccion, float health, float velocidadDeInteraccion, float velocidadDeMovimiento, float damage, float escudo)
        {
            _distanciaDeInteraccion = distanciaDeInteraccion;
            _health = health;
            _velocidadDeInteraccion = velocidadDeInteraccion;
            _velocidadDeMovimiento = velocidadDeMovimiento;
            _damage = damage;
            _escudo = escudo;
        }


        public float DistanciaDeInteraccion => _distanciaDeInteraccion;
        public float Health => _health;
        public float VelocidadDeInteraccion => _velocidadDeInteraccion;
        public float VelocidadDeMovimiento => _velocidadDeMovimiento;
        public float Damage => _damage;
        public float Escudo => _escudo;

    }
}