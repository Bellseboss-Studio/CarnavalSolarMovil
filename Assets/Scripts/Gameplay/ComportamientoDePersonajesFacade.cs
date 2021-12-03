using System.Collections.Generic;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay
{
    public class ComportamientoDePersonajesFacade: MonoBehaviour
    {
        private List<Personaje> _personajes;
        private PersonajeBuilder _personajeBuilder;
        [SerializeField] private Personaje _personaje;
        [SerializeField] private RutaComponent _rutaComponent;
        [SerializeField] private TargetComponent _targetComponent;
        [SerializeField] private InteraccionComponent _interaccionComponent;
        [SerializeField] private bool EsEnemigoElPersonaje;
    
    
        private void Awake()
        {
            _personajes = new List<Personaje>();
            _personajeBuilder = new PersonajeBuilder();
        
        }
    
        void Start()
        {
            _personajeBuilder.WithPersonaje(_personaje);
            _personajeBuilder.WithTargetComponent(_targetComponent);
            _personajeBuilder.WithInteraccionComponent(_interaccionComponent);
            _personajeBuilder.WithRutaComponent(_rutaComponent);
            _personajeBuilder.WithVelocidadDeInteraccion(1);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                foreach (var variablePersonaje in _personajes)
                {
                    variablePersonaje.laPartidaEstaCongelada = false;
                }
            }

            if(EsEnemigoElPersonaje) return;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    _personajeBuilder.WithPosition(hit.point);
                    var personaje = _personajeBuilder.Build();
                    personaje.transform.parent = transform;
                    personaje.enemigo = false;
                    _personajes.Add(personaje);
                }
            }
        
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    _personajeBuilder.WithPosition(hit.point);
                    var personaje = _personajeBuilder.Build();
                    personaje.transform.parent = transform;
                    personaje.enemigo = true;
                    _personajes.Add(personaje);
                }
            }
        
        }

        void CongelarPersonajes()
        {
            foreach (var personaje in _personajes)
            {
                personaje.laPartidaEstaCongelada = true;
            }
        }

        void InstanciarPersonaje(PersonajeBuilder personajeBuilder, bool seraEnemigoElPersonaje)
        {
            var personaje = personajeBuilder.Build();
            personaje.transform.parent = transform;
            personaje.enemigo = seraEnemigoElPersonaje;
            _personajes.Add(personaje);
        }

        public PersonajeBuilder GetPersonajeBuilder()
        {
            return _personajeBuilder;
        }
        
        
    }
}
