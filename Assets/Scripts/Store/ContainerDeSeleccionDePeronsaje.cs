using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Rest;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerDeSeleccionDePeronsaje : MonoBehaviour
{
    [SerializeField] private Image foto, seleccionado;
    [SerializeField] private TextMeshProUGUI nombre, ataque, defensa, velocidad;
    [SerializeField] private Button selecting;

    public delegate void OnSelectingPj(Personaje pj);

    public OnSelectingPj EsSeleccionadoElPersonaje;
    
    private Personaje pjLocal;
    public void Fulled(Personaje pj)
    {
        pjLocal = pj;
        //Debug.Log($"Llenado");
        nombre.text = pjLocal.GetNombre();
        ataque.text = $"{pjLocal.GetAtaque()}";
        defensa.text = $"{pjLocal.GetDefensa()}";
        velocidad.text = $"{pjLocal.GetVelocidad()}";
        selecting.onClick.AddListener(Seleccionado);
        LoadImage();
    }

    private void Seleccionado()
    {
        if (pjLocal == null)
        {
            return;
        }
        EsSeleccionadoElPersonaje?.Invoke(pjLocal);
        SeleccionadoPorClick();
        SfxManager.Instance.PlaySound(Path.GetFileName(pjLocal.imagen).Replace(".jpg", ""));
    }

    public void SeleccionadoPorClick()
    {
        seleccionado.enabled = true;
    }
    public void DesHabilitarBoton()
    {
        selecting.enabled = false;
    }

    private void LoadImage()
    {
        //Debug.Log($"Buscando imagen {pjLocal.imagen}");
        StartCoroutine(RestGet.GetImageRequest(pjLocal.imagen, result =>
        {
            foto.sprite = result;
            //Debug.Log($"encontro la imagen");
        }, () =>
        {
            //Debug.Log($"no encontro la imagen");
        }));
    }
}
