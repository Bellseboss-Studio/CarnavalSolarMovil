using System;
using System.Collections.Generic;
using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ServiceLocatorPath
{
    public class ServicioDeInstanciadoDeEnemigos : MonoBehaviour, IEnemyInstantiate, IHeroeInstancie, IEstadoDePersonajesDelJuego
    {
        [SerializeField] private GameObject point;
        private List<Gameplay.Personaje> personajesAliado;
        private List<Gameplay.Personaje> personajesEnemigos;
        private IFactoriaCarta _factoriaCarta;

        private void Start()
        {
            personajesAliado = new List<Gameplay.Personaje>();
            personajesEnemigos = new List<Gameplay.Personaje>();
        }

        public void Configuration(IFactoriaCarta factoriaCarta)
        {
            _factoriaCarta = factoriaCarta;
        }

        public void InstanciateHeroEnemy(FactoriaPersonaje factoriaPersonaje)
        {
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = factoriaPersonaje.CreatePersonaje(GetPointRandom(), cartaTemplate.GetEstadisticas(), true);
            personajesEnemigos.Add(personaje);
            Destroy(cartaTemplate.gameObject);
        }

        public void InstanciateEnemy(FactoriaPersonaje factoriaPersonaje)
        {
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            factoriaPersonaje.CreatePersonaje(GetPointRandom(), cartaTemplate.GetEstadisticas(), true);
            Destroy(cartaTemplate.gameObject);
        }

        private Vector3 GetPointRandom()
        {
            var pointToInstantiate = Random.insideUnitSphere.normalized;
            pointToInstantiate.x += point.transform.position.x;
            pointToInstantiate.z += point.transform.position.z;
            pointToInstantiate.y = point.transform.position.y;
            return pointToInstantiate;
        }

        public void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point, string cualHeroe)
        {
            InstanciateHero(factoriaPersonaje, point, cualHeroe, false);
        }

        public void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point, string cualHeroe, bool enemigo)
        {
            var carta = cualHeroe;//ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = factoriaPersonaje.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), enemigo);
            personajesAliado.Add(personaje);
            Destroy(cartaTemplate.gameObject);
        }

        public bool TerminoElJuego()
        {
            var terminoAliado = true;
            var terminoEmemigo = true;
            foreach (var personajeAliado in personajesAliado)
            {
                if (personajeAliado.health > 0)
                {
                    terminoAliado = false;
                }
            }

            foreach (var personajeEnemigo in personajesEnemigos)
            {
                if (personajeEnemigo.health > 0)
                {
                    terminoEmemigo = false;
                }
            }

            return terminoAliado || terminoEmemigo;
        }
    }
}