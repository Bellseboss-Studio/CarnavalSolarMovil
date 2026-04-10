using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISound : MonoBehaviour
{
    public void PlayUiSound(string buttonName)
    {
        SfxManager.Instance.PlaySound(buttonName);
    }
}
