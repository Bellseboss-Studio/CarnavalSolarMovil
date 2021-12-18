using Gameplay.NewGameStates;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public class FactoriaPersonaje : MonoBehaviour, IFactoriaPersonajes
    {
        [SerializeField] private GameObject _modelo3D;
        [SerializeField] private Personaje _personaje;
        
        
        
        
        public void CreateCarta(Vector3 hitPoint)
        {
            var _personajeBuilder = new PersonajeBuilder();
            _personajeBuilder.With3DObject(_modelo3D);
            _personajeBuilder.WithPersonaje(_personaje);
            _personajeBuilder.WithTargetComponent(new BuscartresEnemigosMasCercanos());
            _personajeBuilder.WithInteraccionComponent(new DaniarTresTargetsMasCercanos());
            _personajeBuilder.WithRutaComponent(new RutaMasCorta());
            _personajeBuilder.WithEstadisticasCarta(new EstadisticasCarta(2, 10, 1, 2, 2,0));
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