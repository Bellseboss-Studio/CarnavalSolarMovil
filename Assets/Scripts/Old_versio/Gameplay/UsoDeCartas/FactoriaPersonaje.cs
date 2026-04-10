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

        public OnPersonajeCreado PersonajeCreado;
        public delegate void OnPersonajeCreado(Personaje personaje);
        
        private void Awake()
        {
            configuracionDeModelo3DDePersonajes = Instantiate(configuracionDeModelo3DDePersonajes);
        }

        public Personaje CreatePersonaje(Vector3 hitPoint, EstadististicasYHabilidadesDePersonaje estadististicasYHabilidadesDePersonaje, bool esEnemigo = false, bool renderizarUI = true)
        {
            var _personajeBuilder = new PersonajeBuilder();
            _personajeBuilder.With3DObject(configuracionDeModelo3DDePersonajes.GetPersonajePrefabById(estadististicasYHabilidadesDePersonaje.idModelo3D))
                .WithPersonaje(personaje)
                .WithTargetComponent(estadististicasYHabilidadesDePersonaje.idTargetComponent)
                .WithInteraccionComponent(estadististicasYHabilidadesDePersonaje.idInteraccionComponent)
                .WithRutaComponent(estadististicasYHabilidadesDePersonaje.idRutaComponent)
                .WithEstadisticasCarta(estadististicasYHabilidadesDePersonaje.EstadisticasCarta)
                .WithPosition(hitPoint)
                .WithUi(renderizarUI);
            var personajeInstanciado = InstanciarPersonaje(_personajeBuilder, esEnemigo);
            PersonajeCreado?.Invoke(personajeInstanciado);
            return personajeInstanciado;
        }
        
        Personaje InstanciarPersonaje(PersonajeBuilder personajeBuilder, bool seraEnemigoElPersonaje)
        {
            var personajeInstanciado = personajeBuilder.Build();
            personajeInstanciado.transform.parent = transform;
            personajeInstanciado.enemigo = seraEnemigoElPersonaje;
            return personajeInstanciado;
            // _personajes.Add(personaje);
        }
    }
}