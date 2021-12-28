using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeInstanciadoDeEnemigos : MonoBehaviour, IEnemyInstantiate, IHeroeInstancie
    {
        [SerializeField] private GameObject point;
        private IFactoriaCarta _factoriaCarta;

        public void Configuration(IFactoriaCarta factoriaCarta)
        {
            _factoriaCarta = factoriaCarta;
        }
        public void InstanciateEnemy(FactoriaPersonaje factoriaPersonaje)
        {
            var pointToInstantiate = Random.insideUnitSphere.normalized;
            pointToInstantiate.x += point.transform.position.x;
            pointToInstantiate.z += point.transform.position.z;
            pointToInstantiate.y = point.transform.position.y;
            //Debug.Log(pointToInstantiate);
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            factoriaPersonaje.CreatePersonaje(pointToInstantiate, cartaTemplate.GetEstadisticas(), true);
            Destroy(cartaTemplate.gameObject);
        }
        public void InstanciateHero(FactoriaPersonaje factoriaPersonaje, Vector3 point)
        {
            //Debug.Log(pointToInstantiate);
            var carta = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            var cartaTemplate = _factoriaCarta.CreateEnemigo(carta, gameObject);
            factoriaPersonaje.CreatePersonaje(point, cartaTemplate.GetEstadisticas(), true);
            Destroy(cartaTemplate.gameObject);
        }
        
    }
}