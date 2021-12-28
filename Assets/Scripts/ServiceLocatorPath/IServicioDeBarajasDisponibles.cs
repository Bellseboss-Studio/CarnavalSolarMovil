using System.Collections.Generic;
using Gameplay.UsoDeCartas;

namespace ServiceLocatorPath
{
    public interface IServicioDeBarajasDisponibles
    {
        void AniadirBaraja(SelectorBaraja baraja);
        void SetBarajaSeleccionadaId(SelectorBaraja barajaSeleccionadaId);
        Stack<string> GetBarajaElejida();
        string GetHeroe();
    }
}