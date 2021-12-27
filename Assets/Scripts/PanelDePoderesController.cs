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
    private float _cooldown;
    private Personaje _personaje;
    [SerializeField] private List<Image> equis;

    public void ConfigureSliderValues(Personaje personaje)
    {
        _personaje = personaje;
        _cooldown = personaje.cooldown;
        normalAttackCooldown = personaje.cooldown * 0.5f;
        specialAttackCooldown = personaje.cooldown * 0.8f;
        normalAttackSlider.value = 0;
        specialAttackSlider.value = 0;
    }

    public void CooldownForNormalAttack()
    {
        normalAttackSlider.value = 1;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, normalAttackSlider.DOValue(0, normalAttackCooldown));
    }

    public void CooldownForSpecialAttack()
    {
        specialAttackSlider.value = 1;
        var sequence = DOTween.Sequence();
        sequence.Insert(0, specialAttackSlider.DOValue(0, specialAttackCooldown));
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

    public void DesactivarPanelDePoderes()
    {
        var sequence = DOTween.Sequence();
        foreach (var image in equis)
        {
            image.gameObject.SetActive(true);
            sequence.Insert(0,image.DOFade(.7f, .25f));
            sequence.Insert(.25f,image.DOFade(.35f, .2f));
            sequence.Insert(.45f,image.DOFade(.7f, .2f));
        }
    }
}