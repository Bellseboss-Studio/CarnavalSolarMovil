using System;
using System.Collections.Generic;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InterfazDeUsuario
{
    public class UISeleccionDeMazo : MonoBehaviour
    {
        [SerializeField] private List<SelectorBaraja> selectoresDeBaraja;
        [SerializeField] private Button botonContinuar, jugar;
        [SerializeField] private Animator animator;
        private SelectorBaraja _barajaSeleccionada;

        private void Awake()
        {
            var servicioDeBarajasDisponibles = new ServicioDeBarajasDisponibles();
            ServiceLocator.Instance.RegisterService<IServicioDeBarajasDisponibles>(servicioDeBarajasDisponibles);
        }

        private void Start()
        {
            botonContinuar.onClick.AddListener(ContinuarASiguienteEscena);
            jugar.onClick.AddListener(() =>
            {
                animator.SetBool("jugar", true);
            });
            
            ConfigurarBotonesSelectoresDeBaraja();
        }

        private void ContinuarASiguienteEscena()
        {
            ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>()
                .SetBarajaSeleccionadaId(_barajaSeleccionada);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        private void ConfigurarBotonesSelectoresDeBaraja()
        {
            foreach (var selectorDeBaraja in selectoresDeBaraja)
            {
                selectorDeBaraja.BotonBarajaSeleccionada.onClick.AddListener(() =>
                {
                    if (!selectorDeBaraja.estaSeleccionadaLaBaraja)
                    {
                        selectorDeBaraja.estaSeleccionadaLaBaraja = true;
                        foreach (var selectorBaraja in selectoresDeBaraja)
                        {
                            selectorBaraja.BotonBarajaSeleccionada.enabled = false;
                        }
                        selectorDeBaraja.BotonBarajaSeleccionada.enabled = true;
                        _barajaSeleccionada = selectorDeBaraja;
                        botonContinuar.gameObject.SetActive(true);
                    }
                    else
                    {
                        selectorDeBaraja.estaSeleccionadaLaBaraja = false;
                        foreach (var selectorBaraja in selectoresDeBaraja)
                        {
                            selectorBaraja.BotonBarajaSeleccionada.enabled = true;
                        }
                        
                        botonContinuar.gameObject.SetActive(false);
                    }
                });
            }
        }
    }
}