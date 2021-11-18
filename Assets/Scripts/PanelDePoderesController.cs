using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExitGames.Client.Photon;
using Rest;
using ServiceLocatorPath;
using UnityEngine;
using UnityEngine.UI;
public class PanelDePoderesController : MonoBehaviour
{
    [SerializeField] private Slider normalAttackSlider, specialAttackSlider;
    private float normalAttackCooldown, specialAttackCooldown;
    [SerializeField] private Image miniatura;
    [SerializeField] private DragComponent normalAttackDrag, specialAttackDrag;
    private float _cooldown;
    private Personaje _personaje;


    public void ConfigureSliderValues(Personaje personaje)
    {
        _personaje = personaje;
        _cooldown = personaje.cooldown;
        normalAttackCooldown = personaje.cooldown * 0.5f;
        specialAttackCooldown = personaje.cooldown * 0.8f;
        normalAttackSlider.value = 0;
        specialAttackSlider.value = 0;
        normalAttackDrag.OnDropInAnywhere += CooldownForNormalAttack;
        specialAttackDrag.OnDropInAnywhere += CooldownForSpecialAttack;
    }

    public void CooldownForNormalAttack()
    {
        normalAttackDrag.CannotBeUsed();
        normalAttackSlider.value = 1;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, normalAttackSlider.DOValue(0, normalAttackCooldown));
        sequence.OnComplete (()=> normalAttackDrag.CanBeUsed());
    }

    public void CooldownForSpecialAttack()
    {
        specialAttackDrag.CannotBeUsed();
        specialAttackSlider.value = 1;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, specialAttackSlider.DOValue(0, specialAttackCooldown));
        sequence.OnComplete (()=> specialAttackDrag.CanBeUsed());
    }


    public void LlenarFotoDePerfil()
    {
        StartCoroutine(RestGet.GetImageRequest(_personaje.fotoDePerfil, result =>
        {
            miniatura.sprite = result;
            //Debug.Log($"encontro la imagen");
        }, () =>
        {
            //Debug.Log($"no encontro la imagen");
        }));
    }
}