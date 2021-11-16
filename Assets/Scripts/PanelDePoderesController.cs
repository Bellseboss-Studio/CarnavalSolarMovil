using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExitGames.Client.Photon;
using Store;
using UnityEngine;
using UnityEngine.UI;
public class PanelDePoderesController : MonoBehaviour, ICooldownAttacks
{
    [SerializeField] private Slider normalAttackSlider, specialAttackSlider;
    private float normalAttackCooldown, specialAttackCooldown;
    private IMediatorCooldown _mediatorCooldown;
    [SerializeField] private DragComponent normalAttackDrag, specialAttackDrag;
    private float _cooldown;

    public void ConfigureSliderValues(float cooldown, IMediatorCooldown mediatorCooldown)
    {
        _cooldown = cooldown;
        _mediatorCooldown = mediatorCooldown;
        normalAttackCooldown = cooldown * 0.5f;
        specialAttackCooldown = cooldown * 0.8f;
        normalAttackSlider.value = 1;
        specialAttackSlider.value = 1;
        normalAttackDrag.OnDropInAnywhere += CooldownForNormalAttack;
        specialAttackDrag.OnDropInAnywhere += CooldownForSpecialAttack;
    }

    public void CooldownForNormalAttack()
    {
        normalAttackDrag.CannotBeUsed();
        normalAttackSlider.value = 0;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, normalAttackSlider.DOValue(1, normalAttackCooldown));
        sequence.OnComplete (()=> normalAttackDrag.CanBeUsed());
    }

    public void CooldownForSpecialAttack()
    {
        specialAttackDrag.CannotBeUsed();
        specialAttackSlider.value = 0;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, specialAttackSlider.DOValue(1, specialAttackCooldown));
        sequence.OnComplete (()=> specialAttackDrag.CanBeUsed());
    }

    
}