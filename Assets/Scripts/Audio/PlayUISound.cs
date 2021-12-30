using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISound : MonoBehaviour
{
    [SerializeField] bool isActive;
      
    public void PlayUiSound(string buttonName)
    {
        if(!isActive)
        {
            SfxManager.Instance.PlaySound(buttonName);
            isActive = true;
        }else
        {
            SfxManager.Instance.PlaySound("GenericButton");
            isActive = false;
        }
        
    }
}
