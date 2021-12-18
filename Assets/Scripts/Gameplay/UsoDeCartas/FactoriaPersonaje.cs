using Gameplay.NewGameStates;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public class FactoriaPersonaje : MonoBehaviour, IFactoriaPersonajes
    {
        [SerializeField] private ConfiguracionDeModelo3DDePersonajes configuracionDeModelo3DDePersonajes;
        [SerializeField] private Personaje personaje;
        
        private void Awake()
        {
            configuracionDeModelo3DDePersonajes = Instantiate(configuracionDeModelo3DDePersonajes);
        }

        public void CreatePersonaje(Vector3 hitPoint, EstadististicasYHabilidadesDePersonaje estadististicasYHabilidadesDePersonaje)
        {
            var _personajeBuilder = new PersonajeBuilder();
            _personajeBuilder.With3DObject(configuracionDeModelo3DDePersonajes.GetPersonajePrefabById(estadististicasYHabilidadesDePersonaje.idModelo3D));
            _personajeBuilder.WithPersonaje(personaje);
            _personajeBuilder.WithTargetComponent(estadististicasYHabilidadesDePersonaje.idTargetComponent);
            _personajeBuilder.WithInteraccionComponent(estadististicasYHabilidadesDePersonaje.idInteraccionComponent);
            _personajeBuilder.WithRutaComponent(estadististicasYHabilidadesDePersonaje.idRutaComponent);
            _personajeBuilder.WithEstadisticasCarta(estadististicasYHabilidadesDePersonaje.EstadisticasCarta);
            _personajeBuilder.WithPosition(hitPoint);
            InstanciarPersonaje(_personajeBuilder, true);
        }
        
        void InstanciarPersonaje(PersonajeBuilder personajeBuilder, bool seraEnemigoElPersonaje)
        {
            var personaje = personajeBuilder.Build();
            personaje.transform.parent = transform;
            personaje.enemigo = seraEnemigoElPersonaje;
            // _personajes.Add(personaje);
        }
    }
}