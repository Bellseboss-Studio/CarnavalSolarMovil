using System.Collections.Generic;
using Gameplay.UsoDeCartas;

namespace ServiceLocatorPath
{
    public interface IServicioDeBarajasDisponibles
    {
        void AniadirBaraja(SelectorBaraja baraja);
        void SetBarajaSeleccionadaId(SelectorBaraja barajaSeleccionadaId);
        public SelectorBaraja GetBarajaSeleccionadaId();
        void SetBarajaEnemigaSeleccionadaId(string barajaSeleccionadaId);
        Stack<string> GetPrimeras10Cartas();
        string GetHeroe();
        string GetHeroeEnemigo();
    }
}