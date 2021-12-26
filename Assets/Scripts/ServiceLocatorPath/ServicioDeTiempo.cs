using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocatorPath
{
    public class ServicioDeTiempo : MonoBehaviour, IServicioDeTiempo
    {

        public const int EsTiempoDeColocacionHeroe = 1;
        public const int EsTiempoDePausa = 2;
        public const int EsTiempoDeJuego = 3;
        
        [SerializeField] private float tiempoDePausa;
        [SerializeField] private float tiempoDeJuego;
        [SerializeField] private float tiempoDeColocacionHeroe;
        [SerializeField] private Slider progresBarTiempo;

        private bool _puedoContarElTiempo;
        private float _deltaTimeLocal = 0;
        private float _valorDeLaProgresBar;
        private int _tiempoQueEstoyContando;

        private void Awake()
        {
            
        }

        private void Update()
        {
            if (_puedoContarElTiempo)
            {
                _deltaTimeLocal += Time.deltaTime;
                DarValorALaProgressBar();
            }
        }

        private void DarValorALaProgressBar()
        {
            if (_tiempoQueEstoyContando == EsTiempoDeColocacionHeroe)
            {
                _valorDeLaProgresBar = _deltaTimeLocal / tiempoDeColocacionHeroe;
                progresBarTiempo.value = 1 - _valorDeLaProgresBar;
            }
            else
            {
                if (_tiempoQueEstoyContando == EsTiempoDePausa)
                {
                    _valorDeLaProgresBar = _deltaTimeLocal / tiempoDePausa;
                    progresBarTiempo.value = 1 - _valorDeLaProgresBar;
                }
                else
                {
                    _valorDeLaProgresBar = _deltaTimeLocal / tiempoDeJuego;
                    progresBarTiempo.value = 1 - _valorDeLaProgresBar;
                }
            }
        }

        public bool EstaPausadoElJuego()
        {
            return _deltaTimeLocal <= tiempoDePausa;
        }

        public void ComienzaAContarElTiempo(int queTiempoEstoyContando)
        {
            _puedoContarElTiempo = true;
            _tiempoQueEstoyContando = queTiempoEstoyContando;
        }

        public void DejaDeContarElTiempo()
        {
            _puedoContarElTiempo = false;
            _deltaTimeLocal = 0;
        }

        public bool EstanJugando()
        {
            return _deltaTimeLocal <= tiempoDeJuego;
        }

        public bool SeEstaColocandoElHeroe()
        {
            return _deltaTimeLocal <= tiempoDeColocacionHeroe;
        }
    }
}