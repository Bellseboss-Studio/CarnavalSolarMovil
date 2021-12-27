using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CrearSala : MonoBehaviour
{
    [SerializeField] private GameObject botonDeCrear, botonDeUnir, textoCrear, textoUnir, panelDeError, textoMensajeError, botonSalirDeVentanaError;
    [SerializeField] private TMP_InputField nombreDeSala;
    private bool flagDeEntrada;
    private bool flatDeEntradaLaSala;

    private void Start()
    {
        botonDeCrear.GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GetService<IMultiplayer>().CrearSala(nombreDeSala.text);
        });
        botonDeUnir.GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GetService<IMultiplayer>().UnirseSala(nombreDeSala.text);
        });
        
        nombreDeSala.onValueChanged.AddListener(arg0 =>
        {
            if(botonDeCrear.activeSelf) return;
            botonDeCrear.gameObject.SetActive(true);
            botonDeUnir.gameObject.SetActive(true);
            MostrarBotones();
        });
        
    }

    private void MostrarBotones()
    {
        var sequence = DOTween.Sequence();
        var botonDeCrearImage = botonDeCrear.GetComponent<Image>();
        var botonDeUnirImage = botonDeUnir.GetComponent<Image>();
        sequence.Insert(0, botonDeCrearImage.DOFade(1, 1));
        sequence.Insert(0, botonDeUnirImage.DOFade(1, 1));
        sequence.Insert(0, textoCrear.GetComponent<TextMeshProUGUI>().DOFade(1,1));
        sequence.Insert(0, textoUnir.GetComponent<TextMeshProUGUI>().DOFade(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.Instance.GetService<IMultiplayer>().EstaListo() && !flagDeEntrada)
        {
            flagDeEntrada = true;
            //botonDeCrear.SetActive(true);
            //botonDeUnir.SetActive(true);
            nombreDeSala.gameObject.SetActive(true);
        }

        if (ServiceLocator.Instance.GetService<IMultiplayer>().TerminoDeProcesarLaSala() && !flatDeEntradaLaSala)
        {
            flatDeEntradaLaSala = true;
            if (!ServiceLocator.Instance.GetService<IMultiplayer>().FalloAlgo())
            {
                PhotonNetwork.LoadLevel(1);
            }
            else
            {
                var botonDeCrearImage = botonDeCrear.GetComponent<Image>();
                var botonDeUnirImage = botonDeUnir.GetComponent<Image>();
                textoMensajeError.GetComponent<TextMeshProUGUI>().text =
                    ServiceLocator.Instance.GetService<IMultiplayer>().GetErrorMessage();
                var sequence = DOTween.Sequence();
                sequence.Insert(0, botonDeCrearImage.DOFade(0, 1));
                sequence.Insert(0, botonDeUnirImage.DOFade(0, 1));
                sequence.Insert(0, textoCrear.GetComponent<TextMeshProUGUI>().DOFade(0,1));
                sequence.Insert(0, textoUnir.GetComponent<TextMeshProUGUI>().DOFade(0,1));
                var position = panelDeError.transform.position;
                sequence.Insert(0, panelDeError.GetComponent<RectTransform>().DOMove(new Vector3(position.x,200), 0.6f));
                sequence.Insert(0.6f, panelDeError.GetComponent<RectTransform>().DOMove(new Vector3(position.x, 160), 0.4f));
                botonSalirDeVentanaError.GetComponent<Button>().onClick.AddListener(SalirDeLaVentanaDeError);
            }
        }
        
        
    }

    private void SalirDeLaVentanaDeError()
    {
        ServiceLocator.Instance.GetService<IMultiplayer>().ResetFlags();
        var sequence = DOTween.Sequence();
        var position = panelDeError.transform.position;
        sequence.Insert(0f, panelDeError.GetComponent<RectTransform>().DOMove(new Vector3(position.x, 200), 0.4f));
        sequence.Insert(0.4f, panelDeError.GetComponent<RectTransform>().DOMove(new Vector3(position.x, -105), 0.6f));
        MostrarBotones();
        nombreDeSala.text = "";
        flatDeEntradaLaSala = false;
    }
}
