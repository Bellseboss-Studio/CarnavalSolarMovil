using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrearSala : MonoBehaviour
{
    [SerializeField] private GameObject botonDeCrear, botonDeUnir;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.Instance.GetService<IMultiplayer>().EstaListo() && !flagDeEntrada)
        {
            flagDeEntrada = true;
            botonDeCrear.SetActive(true);
            botonDeUnir.SetActive(true);
            nombreDeSala.gameObject.SetActive(true);
        }

        if (ServiceLocator.Instance.GetService<IMultiplayer>().TerminoDeProcesarLaSala() && !flatDeEntradaLaSala)
        {
            flatDeEntradaLaSala = true;
            PhotonNetwork.LoadLevel(1);
        }
    }
}
