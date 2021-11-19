using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeBatallaParaPersonajes : MonoBehaviour
{
    [SerializeField] private DragComponent ataqueNormal, ataqueEspecial;
    [SerializeField] private bool isElOtroPlayer;
    [SerializeField] private PanelDePoderesController panelDePoderesController;
    public DragComponent AtaqueNormal => ataqueNormal;
    public DragComponent AtaqueEspecial => ataqueEspecial;

    public PanelDePoderesController GetPanelDePoderesController()
    {
        return panelDePoderesController;
    }
    
    public bool DebeConfigurar => !isElOtroPlayer;
}
