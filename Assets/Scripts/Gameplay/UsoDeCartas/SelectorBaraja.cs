using System;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UsoDeCartas
{
    public class SelectorBaraja : MonoBehaviour
    {
        [SerializeField] private List<string> listaDeIdDeCartasEnBaraja;
        [SerializeField] private string idHeroe;

        public string IDHeroe => idHeroe;

        [SerializeField] private float porcentajeProbabilidadSacarCartaDeLaMismaFamilia;
        public float PorcentajeProbabilidadSacarCartaDeLaMismaFamilia => porcentajeProbabilidadSacarCartaDeLaMismaFamilia;
        private Button _botonBarajaSeleccionada;
        public bool estaSeleccionadaLaBaraja = false;
        public Button BotonBarajaSeleccionada => _botonBarajaSeleccionada;
        public List<string> ListaDeIdDeCartasEnBaraja => listaDeIdDeCartasEnBaraja;
        private void Awake()
        {
            _botonBarajaSeleccionada = GetComponent<Button>();
        }

        private void Start()
        {
            AniadirBarajaAServiceLocator(this);
        }

        public void AniadirBarajaAServiceLocator(SelectorBaraja baraja)
        {
            ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().AniadirBaraja(baraja);
        }
    }
}