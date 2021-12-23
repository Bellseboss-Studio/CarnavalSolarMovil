using Gameplay.NewGameStates;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ServicioDeTiempo _servicioDeTiempo;
        [SerializeField] private ServicioDeEnergia _servicioDeEnergia;
        
        
        private void Awake()
        {
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            var playFab = new PlayFabCustom();
            ServiceLocator.Instance.RegisterService<IPlayFabCustom>(playFab);
            ServiceLocator.Instance.RegisterService<IServicioDeTiempo>(_servicioDeTiempo);
            ServiceLocator.Instance.RegisterService<IServicioDeEnergia>(_servicioDeEnergia);
            var servicioDeBaraja = new ServicioDeBaraja();
            ServiceLocator.Instance.RegisterService<IBarajaDelPlayer>(servicioDeBaraja);
            DontDestroyOnLoad(gameObject);
        }
    }
}