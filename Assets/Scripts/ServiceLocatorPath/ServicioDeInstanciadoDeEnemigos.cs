using System.Collections.Generic;
using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ServiceLocatorPath
{
    public class ServicioDeInstanciadoDeEnemigos : MonoBehaviour, IEnemyInstantiate, IHeroeInstancie, IEstadoDePersonajesDelJuego, IInstanciadoDeCosasConfiguradas
    {
        [SerializeField] private GameObject point;
        [SerializeField] private float multipler;
        private List<Gameplay.Personaje> personajesAliado;
        private List<Gameplay.Personaje> personajesEnemigos;
        private IFactoriaCarta _factoriaCarta;
        private IFactoriaPersonajes _factoriaPersonajes;

        private void Start()
        {
            personajesAliado = new List<Gameplay.Personaje>();
            personajesEnemigos = new List<Gameplay.Personaje>();
        }

        public void Configuration(IFactoriaCarta factoriaCarta, IFactoriaPersonajes factoriaPersonajes)
        {
            _factoriaCarta = factoriaCarta;
            _factoriaPersonajes = factoriaPersonajes;
        }

        public void InstanciateHeroEnemy(IFactoriaPersonajes factoriaPersonaje)
        {
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetHeroeRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = factoriaPersonaje.CreatePersonaje(GetPointRandom(), cartaTemplate.GetEstadisticas(), true, true);
            personaje.LaPartidaEstaCongelada = true;
            personajesEnemigos.Add(personaje);
            Destroy(cartaTemplate.gameObject);
        }

        public void InstanciateEnemy(IFactoriaPersonajes factoriaPersonaje)
        {
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            if (ServiceLocator.Instance.GetService<IServicioDeEnergia>()
                .TieneEnergiaSuficienteP2(cartaTemplate.GetCostoEnergia))
            {
                factoriaPersonaje.CreatePersonaje(GetPointRandom(), cartaTemplate.GetEstadisticas(), true, true);
            }
            else
            {
                Destroy(cartaTemplate.gameObject);
                throw new EnergiaInsuficienteException("No tiene energia para continuar");
            }
            Destroy(cartaTemplate.gameObject);
        }

        private Vector3 GetPointRandom()
        {
            var pointToInstantiate = Random.insideUnitSphere.normalized * multipler;
            pointToInstantiate.x += point.transform.position.x;
            pointToInstantiate.z += point.transform.position.z;
            pointToInstantiate.y = point.transform.position.y;
            return pointToInstantiate;
        }

        public void InstanciateHero(IFactoriaPersonajes factoriaPersonaje, Vector3 point, string cualHeroe)
        {
            InstanciateHero(factoriaPersonaje, point, cualHeroe, false);
        }

        public void InstanciateHero(IFactoriaPersonajes factoriaPersonaje, Vector3 point, string cualHeroe, bool enemigo)
        {
            var carta = cualHeroe;//ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = factoriaPersonaje.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), enemigo,true);
            personaje.LaPartidaEstaCongelada = true;
            personajesAliado.Add(personaje);
            Destroy(cartaTemplate.gameObject);
        }

        public void InstanciateHeroEnemy(IFactoriaPersonajes factoriaPersonaje, Vector3 point, string heroe)
        {
            //ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(heroe, gameObject);
            var personaje = factoriaPersonaje.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), true,true);
            personaje.LaPartidaEstaCongelada = true;
            personajesEnemigos.Add(personaje);
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

        public bool GanoElPlayer()
        {
            var terminoAliado = false;
            foreach (var personajeAliado in personajesAliado)
            {
                if (personajeAliado.health > 0)
                {
                    terminoAliado = true;
                }
            }

            return terminoAliado;
        }

        public void InstanciaSinCarta(string carta, Vector3 point, bool esEnemigo)
        {
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = _factoriaPersonajes.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), esEnemigo, false);
            personaje.EsUnaBala = true;
            personaje.imagenIndicadoraDeEquipo.enabled = false;
            Destroy(cartaTemplate.gameObject);
        }
        public void InstanciaSinCartaConTarget(string carta, Vector3 point, bool esEnemigo, Gameplay.Personaje target)
        {
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            var personaje = _factoriaPersonajes.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), esEnemigo, false);
            personaje.GetTargetComponent().SetTarget(target);
            personaje.EsUnaBala = true;
            personaje.imagenIndicadoraDeEquipo.enabled = false;
            Destroy(cartaTemplate.gameObject);
        }
    }
}