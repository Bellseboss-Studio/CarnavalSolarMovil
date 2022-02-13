using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ServicioDeTiempo _servicioDeTiempo;
        [SerializeField] private ServicioDeEnergia _servicioDeEnergia;
        [SerializeField] private ServicioDeInstanciadoDeEnemigos manejadorDeEnemigos;
        [SerializeField] private ColocacionCartas colocacionCartas;
        [SerializeField] private EnemiBehavior enemiBehavior;
        
        private void Awake()
        {
            var servicioDeBaraja = new ServicioDeBaraja();
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                //Destroy(gameObject);
                return;
            }
            var playFab = new PlayFabCustom();
            ServiceLocator.Instance.RegisterService<IColocacionCartas>(colocacionCartas);
            ServiceLocator.Instance.RegisterService<IPlayFabCustom>(playFab);
            ServiceLocator.Instance.RegisterService<IServicioDeTiempo>(_servicioDeTiempo);
            ServiceLocator.Instance.RegisterService<IServicioDeEnergia>(_servicioDeEnergia);
            ServiceLocator.Instance.RegisterService<IBarajaDelPlayer>(servicioDeBaraja);
            ServiceLocator.Instance.RegisterService<IEnemyInstantiate>(manejadorDeEnemigos);
            ServiceLocator.Instance.RegisterService<IHeroeInstancie>(manejadorDeEnemigos);
            ServiceLocator.Instance.RegisterService<IEstadoDePersonajesDelJuego>(manejadorDeEnemigos);
            ServiceLocator.Instance.RegisterService<IInstanciadoDeCosasConfiguradas>(manejadorDeEnemigos);
            ServiceLocator.Instance.RegisterService<IEnemyBehavior>(enemiBehavior);
            //DontDestroyOnLoad(gameObject);
        }
    }
}