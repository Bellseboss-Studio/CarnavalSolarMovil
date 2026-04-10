using System.Collections.Generic;
using Gameplay.Personajes.InteraccionComponents;
using Gameplay.Personajes.RutaComponents;
using Gameplay.Personajes.TargetComponents;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class ComportamientoDePersonajesFacade: MonoBehaviour
    {
        private List<Personaje> _personajes;
        [SerializeField] private FactoriaPersonaje factoriaPersonaje;
        [SerializeField] private ServicioDeTiempo _servicioDeTiempo;
        [SerializeField] private Color colorAliado, colorEnemigo;

        private void Awake()
        {
            _personajes = new List<Personaje>();
        }
    
        void Start()
        {
            factoriaPersonaje.PersonajeCreado += PersonajeCreado;
            _servicioDeTiempo.DespausarPersonajes += DescongelarPersonajes;
            _servicioDeTiempo.PausarPersonajes += CongelarPersonajes;
        }

        private void PersonajeCreado(Personaje personaje)
        {
            _personajes.Add(personaje);
            //Debug.Log(_servicioDeTiempo.TiempoQueEstoyContando);
            if (_servicioDeTiempo.TiempoQueEstoyContando == 2)
            {
                personaje.LaPartidaEstaCongelada = true;
            }
            personaje.MuerteDelegate+= EliminarPersonajeDeLaLista;
            if (personaje.enemigo)
            {
                personaje.imagenIndicadoraDeEquipo.color = colorEnemigo;
            }
            else
            {
                personaje.imagenIndicadoraDeEquipo.color = colorAliado;
            }
        }

        private void EliminarPersonajeDeLaLista(Personaje personaje)
        {
            _personajes.Remove(personaje);
        }

        private void CongelarPersonajes()
        {
            foreach (var personaje in _personajes)
            {
                personaje.LaPartidaEstaCongelada = true;
            }
        }
        
        private void DescongelarPersonajes()
        {
            foreach (var personaje in _personajes)
            {
                personaje.LaPartidaEstaCongelada = false;
            }
        }
        
    }
}
