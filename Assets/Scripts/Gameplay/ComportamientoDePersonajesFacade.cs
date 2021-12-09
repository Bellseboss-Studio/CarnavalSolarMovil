using System.Collections.Generic;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay
{
    public class ComportamientoDePersonajesFacade: MonoBehaviour
    {
        private List<Personaje> _personajes;
        [SerializeField] private GameObject _modelo3D;
        [SerializeField] private Personaje _personaje;
        [SerializeField] private RutaBehaviour _rutaComponent;
        [SerializeField] private InteraccionComponent _interaccionComponent;
        private bool EsEnemigoElPersonaje;
    
    
        private void Awake()
        {
            _personajes = new List<Personaje>();
        
        }
    
        void Start()
        {
            
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
                    var _personajeBuilder = new PersonajeBuilder();
                    _personajeBuilder.With3DObject(_modelo3D);
                    _personajeBuilder.WithPersonaje(_personaje);
                    _personajeBuilder.WithTargetComponent(new BuscartresEnemigosMasCercanos());
                    _personajeBuilder.WithInteraccionComponent(new DaniarTresTargetsMasCercanos());
                    _personajeBuilder.WithRutaComponent(new RutaMasCorta());
                    _personajeBuilder.WithEstadisticasCarta(new EstadisticasCarta(2, 10, 1, 2, 2,0));
                    _personajeBuilder.WithPosition(hit.point);
                    InstanciarPersonaje(_personajeBuilder, false);
                }
            }
        
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    var _personajeBuilder = new PersonajeBuilder();
                    _personajeBuilder.With3DObject(_modelo3D);
                    _personajeBuilder.WithPersonaje(_personaje);
                    _personajeBuilder.WithTargetComponent(new BuscartresEnemigosMasCercanos());
                    _personajeBuilder.WithInteraccionComponent(new DaniarTresTargetsMasCercanos());
                    _personajeBuilder.WithRutaComponent(new RutaMasCorta());
                    _personajeBuilder.WithEstadisticasCarta(new EstadisticasCarta(2, 10, 1, 2, 2,0));
                    _personajeBuilder.WithPosition(hit.point);
                    InstanciarPersonaje(_personajeBuilder, true);
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

        
        
    }
}
