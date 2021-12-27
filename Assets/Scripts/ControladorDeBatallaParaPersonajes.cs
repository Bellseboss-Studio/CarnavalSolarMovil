using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeBatallaParaPersonajes : MonoBehaviour
{
    [SerializeField] private bool isElOtroPlayer;
    [SerializeField] private PanelDePoderesController panelDePoderesController;

    public PanelDePoderesController GetPanelDePoderesController()
    {
        return panelDePoderesController;
    }
    
    public bool DebeConfigurar => !isElOtroPlayer;
}
