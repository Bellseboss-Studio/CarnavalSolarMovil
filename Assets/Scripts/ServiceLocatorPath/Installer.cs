using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ServicioDeTiempo _servicioDeTiempo;
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
            DontDestroyOnLoad(gameObject);
        }
    }
}