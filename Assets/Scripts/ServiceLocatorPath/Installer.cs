using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private MultiplayerV2 multiplayer;
        [SerializeField] private GameObject prefab;
        private void Awake()
        {
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            var playFab = new PlayFabCustom();
            ServiceLocator.Instance.RegisterService<IPlayFabCustom>(playFab);
            multiplayer.SetPrefabForInstantiante(prefab);
            ServiceLocator.Instance.RegisterService<IMultiplayer>(multiplayer);
            DontDestroyOnLoad(gameObject);
        }
    }
}