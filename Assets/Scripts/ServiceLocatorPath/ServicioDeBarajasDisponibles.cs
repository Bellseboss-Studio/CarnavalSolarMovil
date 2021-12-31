using System.Collections.Generic;
using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using UnityEngine;

namespace ServiceLocatorPath
{
    public class ServicioDeBarajasDisponibles : IServicioDeBarajasDisponibles
    {
        private List<SelectorBaraja> _barajasDisponibles;
        private SelectorBaraja _barajaSeleccionadaId;
        public ServicioDeBarajasDisponibles()
        {
            _barajasDisponibles = new List<SelectorBaraja>();
        }
        public void AniadirBaraja(SelectorBaraja baraja)
        {
            _barajasDisponibles.Add(baraja);
        }

        public void SetBarajaSeleccionadaId(SelectorBaraja barajaSeleccionadaId)
        {
            _barajaSeleccionadaId = barajaSeleccionadaId;
        }

        public Stack<string> GetPrimeras10Cartas()
        {
            var idCartasBarajaSeleccionada = new Stack<string>();
            for (int i = 0; i < 10; i++)
            {
                var cartaId = GetCartaSegunFamilia();
                idCartasBarajaSeleccionada.Push(cartaId);
            }

            return idCartasBarajaSeleccionada;
        }

        private string GetCartaSegunFamilia()
        {
            var valorRandomObtenido = Random.Range(1, 100);
            //Debug.Log(valorRandomObtenido);
            if (valorRandomObtenido <= _barajaSeleccionadaId.PorcentajeProbabilidadSacarCartaDeLaMismaFamilia)
            {
                var cartaAAniadir = Random.Range(0, _barajaSeleccionadaId.ListaDeIdDeCartasEnBaraja.Count);
                //Debug.Log(cartaAAniadir);
                return _barajaSeleccionadaId.ListaDeIdDeCartasEnBaraja[cartaAAniadir];
            }

            var cartaRandom = ServiceLocator.Instance.GetService<IBarajaDelPlayer>().GetCartaRandom();
            return cartaRandom;
        }

        public string GetHeroe()
        {
            return _barajaSeleccionadaId.IDHeroe;
        }
    }
}