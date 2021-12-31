using System;
using DG.Tweening;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using Gameplay.PersonajeStates;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Gameplay
{
    
    [RequireComponent(typeof(TargetComponent),typeof(RutaComponent), typeof(InteraccionComponent))]
    
    public class Personaje : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private TargetComponent _targetComponent;
        [SerializeField] private InteraccionComponent _interaccionComponent;
        [SerializeField] private RutaComponent _rutaComponent;
        [SerializeField] public SinApply _sinApply;
        [SerializeField] public Image imagenIndicadoraDeEquipo;
        [SerializeField] public Slider barraDeVida;
        [SerializeField] public Transform canvasBarraDeVida;

        [SerializeField] private bool esBala;
        public bool EsUnaBala
        {
            get
            {
                return esBala;
            }
            set
            {
                esBala = value;
                if (esBala)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                }
            }
        }

        private PersonajeStatesConfiguration _personajeStatesConfiguration;
        private Animator _animador;
        public bool enemigo;
        private bool laPartidaEstaCongelada;

        public bool LaPartidaEstaCongelada
        {
            get
            {
                return laPartidaEstaCongelada;
            }
            set
            {
                laPartidaEstaCongelada = value;
                if(_animador== null) return;
                _animador?.SetFloat("speedWalk", laPartidaEstaCongelada ? 0 : alternativa1);
                _animador?.SetFloat("speed", laPartidaEstaCongelada ? 0 : alternativa2);
            }
        }

        private bool estaVivo = true;
        public bool isTargeteable = true;
        public bool esInmune = false;
        public string cosaDiferenciadora;
        public float distanciaDeInteraccion;
        public float health = 10;
        private float vidaMaxima;
        public float velocidadDeInteraccion;
        public float velocidadDeMovimiento;
        public float damage;
        public float escudo;
        public float armadura;
        private EstadisticasCarta _estadisticasCarta;
        public string Id => id;
        public OnMuerte MuerteDelegate;
        public delegate void OnMuerte(Personaje personaje);
        
        
        private void Awake()
        {
            
        }

        private void AjustarBarraDeVida()
        {
            var aumentoDeTamañoDeLaBarraDeVida = (health / 250)/5;
            barraDeVida.transform.localScale = new Vector3(1 + aumentoDeTamañoDeLaBarraDeVida, 1,1);
            BarraDeVidaMiraHaciaLaCamara();
        }

        public void BarraDeVidaMiraHaciaLaCamara()
        {
            canvasBarraDeVida.LookAt(Camera.main.transform);
        }

        public void SetComponents(TargetBehaviour targetBehaviour, InteraccionBehaviour interaccionBehaviour, RutaBehaviour rutaBehaviour, EstadisticasCarta estadisticasCarta,
            GameObject prefab, bool renderizarUI)
        {
            var _prefabInstanciado = Instantiate(prefab, transform);
            _animador = _prefabInstanciado.GetComponent<Animator>();
            //hacer el calculo de la velocidad de las animaciones si se puede
            CalculandoVelocidadDeAnimaciones(estadisticasCarta);
            var position = _prefabInstanciado.transform.position;
            position = new Vector3(position.x, position.y + .5f, position.z);
            _prefabInstanciado.transform.position = position;
            velocidadDeInteraccion = estadisticasCarta.VelocidadDeInteraccion;
            health = estadisticasCarta.Health;
            vidaMaxima = estadisticasCarta.Health;
            velocidadDeMovimiento = estadisticasCarta.VelocidadDeMovimiento;
            distanciaDeInteraccion = estadisticasCarta.DistanciaDeInteraccion;
            damage = estadisticasCarta.Damage;
            _estadisticasCarta = estadisticasCarta;
            _targetComponent.Configuracion(targetBehaviour, this);
            _rutaComponent.Configuration(GetComponent<NavMeshAgent>(), this, rutaBehaviour, _sinApply);
            _interaccionComponent.Configurate(this, interaccionBehaviour);
            _personajeStatesConfiguration = new PersonajeStatesConfiguration();
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.CongeladoState, new CongeladoState(this, _interaccionComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.BuscarTargetState, new BuscarTargetState(_targetComponent, this));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.DesplazarseHaciaElTargetState, new DesplazarseHaciaElTargetState(this, _rutaComponent));
            _personajeStatesConfiguration.AddState(PersonajeStatesConfiguration.InteractuarConElTargetState, new InteractuarConElTargetState(this));
            StartState(_personajeStatesConfiguration.GetState(0));
            if (renderizarUI)
            {
                AjustarBarraDeVida();   
            }
            else
            {
                barraDeVida.gameObject.SetActive(false);
            }
        }

        private float cienPorciento1, cienPorciento2, cienPorciento3, cienPorciento4, cienPorciento5, alternativa1, alternativa2, alternativa3, alternativa4, alternativa5;
        private void CalculandoVelocidadDeAnimaciones(EstadisticasCarta estadisticasCarta)
        {
            cienPorciento1 = estadisticasCarta.Caminar.length;
            cienPorciento2 = estadisticasCarta.Golpear.length;
            cienPorciento3 = estadisticasCarta.Idle.length;
            cienPorciento4 = estadisticasCarta.Morir.length;
            alternativa1 = (GetVelocitiOfInteraction(estadisticasCarta.VelocidadDeInteraccion) * 1) / cienPorciento1;
            alternativa2 = (GetVelocitiOfInteraction(estadisticasCarta.VelocidadDeInteraccion) * 1) / cienPorciento2;
            alternativa3 = (GetVelocitiOfInteraction(estadisticasCarta.VelocidadDeInteraccion) * 1) / cienPorciento3;
            alternativa4 = (GetVelocitiOfInteraction(estadisticasCarta.VelocidadDeInteraccion) * 1) / cienPorciento4;
            alternativa2 /= 3;
            Debug.Log($"Caminar {alternativa1} ; Golpear {alternativa2} ; Idle {alternativa3} ; Morir {alternativa4}");
            _animador?.SetFloat("speedWalk", alternativa1);
        }

        private float GetVelocitiOfInteraction(float estadisticasCartaVelocidadDeInteraccion)
        {
            return estadisticasCartaVelocidadDeInteraccion == 0 ? 1 : estadisticasCartaVelocidadDeInteraccion;
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

        public Vector3 GetCharacterPosition()
        {
            return transform.position;
        }

        public void Muerte()
        {
            barraDeVida.gameObject.SetActive(false);
            GetTargetComponent().DejarDeSerTargeteado(this);
            GetTargetComponent().HeDejadoDeTargetear();
            isTargeteable = false;
            _animador?.SetBool("estaCaminando", false);
            _animador?.SetBool("estaGolpeando", false);
            _animador?.SetBool("estaGolpeandoDistancia", false);
            _animador?.SetTrigger("murio");
            _animador?.SetFloat("speed", 1);
            MuerteDelegate?.Invoke(this);
            Destroy(gameObject, 5);
        }

        private void OnDisable()
        {
            estaVivo = false;
        }

        public void Caminar(bool estaCaminando)
        {
            if(gameObject!= null){ _animador?.SetBool("estaCaminando", estaCaminando);}
        }

        public void GolpearTarget()
        {
            if (_estadisticasCarta.DistanciaDeInteraccion > 3 && _animador != null)
            {
                _animador?.SetBool("estaGolpeandoDistancia", true);
            }
            else
            {
                _animador?.SetBool("estaGolpeando", true);
            }
        }

        public void DejarInteractuar()
        {
            if (_estadisticasCarta.DistanciaDeInteraccion > 2 && _animador != null)
            {
                _animador?.SetBool("estaGolpeandoDistancia", false);
            }
            else
            {
                if(_animador!= null) _animador?.SetBool("estaGolpeando", false);
            }
        }

        public RutaComponent GetRutaComponent()
        {
            return _rutaComponent;
        }

        public void ActualizarBarraDeVida()
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, barraDeVida.DOValue(health / vidaMaxima, 1f).SetEase(Ease.OutCirc));
        }
    }
}