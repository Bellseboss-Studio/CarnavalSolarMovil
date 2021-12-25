using System;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeTiempo : MonoBehaviour, IServicioDeTiempo
    {

        [SerializeField] private float tiempoDePausa;
        [SerializeField] private float tiempoDeJuego;

        private bool _puedoContarElTiempo;
        private float _deltaTimeLocal;
        
        private void Awake()
        {
            
        }

        private void Update()
        {
            if (_puedoContarElTiempo)
            {
                _deltaTimeLocal += Time.deltaTime;
            }
        }

        public bool EstaPausadoElJuego()
        {
            return _deltaTimeLocal <= tiempoDePausa;
        }

        public void ComienzaAContarElTiempo()
        {
            _puedoContarElTiempo = true;
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
    }
}