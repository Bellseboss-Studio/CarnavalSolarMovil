using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        private void Awake()
        {
            var playFab = new PlayFabCustom();
            ServiceLocator.Instance.RegisterService<IPlayFabCustom>(playFab);
        }
    }
}