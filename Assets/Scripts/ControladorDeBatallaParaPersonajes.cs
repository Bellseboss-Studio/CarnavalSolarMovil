using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeBatallaParaPersonajes : MonoBehaviour
{
    [SerializeField] private DragComponent ataqueNormal, ataqueEspecial;
    [SerializeField] private bool isElOtroPlayer;
    public DragComponent AtaqueNormal => ataqueNormal;
    public DragComponent AtaqueEspecial => ataqueEspecial;

    public bool DebeConfigurar => !isElOtroPlayer;
}
