using System.Collections.Generic;
using Gameplay.UsoDeCartas;

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

        public Stack<string> GetBarajaElejida()
        {
            var idCartasBarajaSeleccionada = new Stack<string>();
            foreach (var idDeCartaEnBaraja in _barajaSeleccionadaId.ListaDeIdDeCartasEnBaraja)
            {
                idCartasBarajaSeleccionada.Push(idDeCartaEnBaraja);
            }

            return idCartasBarajaSeleccionada;
        }

        public string GetHeroe()
        {
            return _barajaSeleccionadaId.ListaDeIdDeCartasEnBaraja[0];
        }
    }
}