using System;
using System.Collections.Generic;
using DG.Tweening;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UsoDeCartas
{
    public class SelectorBaraja : MonoBehaviour
    {
        [SerializeField] private List<string> listaDeIdDeCartasEnBaraja;
        [SerializeField] private string idHeroe;
        [SerializeField] private Image imageDeFamilia, seleccion;
        [SerializeField] private Sprite foto;
        [SerializeField] private TextMeshProUGUI textoDeNombreDeFamilia;
        [SerializeField] private TextMeshProUGUI textoDeDescripcionDeFamilia;
        [SerializeField] private string nombreFamilia;
        [SerializeField] private string descripcionDeFamilia;
        [SerializeField] private bool puedeSeleccionarlo;

        public string IDHeroe => idHeroe;
        public bool PuedeSeleccionarse => puedeSeleccionarlo;

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
            imageDeFamilia.sprite = foto;
            imageDeFamilia.preserveAspect = true;
            textoDeNombreDeFamilia.text = nombreFamilia;
            textoDeDescripcionDeFamilia.text = descripcionDeFamilia;
        }

        public void AniadirBarajaAServiceLocator(SelectorBaraja baraja)
        {
            ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().AniadirBaraja(baraja);
        }

        public void OscurecerImagen()
        {
            var negro = Color.black;
            negro.a = .2f;
            //seleccion.color = negro;
        }
        public void AclararImagen()
        {
            //seleccion.color = Color.clear;
        }

        public void MueveteALaDerecha(int distancia)
        {
            var sequence = DOTween.Sequence();
            var tra = GetComponent<RectTransform>();
            sequence.Insert(0, tra.DOLocalMoveX(tra.localPosition.x + distancia, .4f));
        }
        public void MueveteALaIzquierda(int distancia)
        {
            var sequence = DOTween.Sequence();
            var tra = GetComponent<RectTransform>();
            sequence.Insert(0, tra.DOLocalMoveX(tra.localPosition.x - distancia, .4f));
        }
    }
}