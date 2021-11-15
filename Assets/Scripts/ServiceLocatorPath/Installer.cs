using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private MultiplayerV2 multiplayer;
        private void Awake()
        {
            var playFab = new PlayFabCustom();
            ServiceLocator.Instance.RegisterService<IPlayFabCustom>(playFab);
            ServiceLocator.Instance.RegisterService<IMultiplayer>(multiplayer);
        }
    }
}