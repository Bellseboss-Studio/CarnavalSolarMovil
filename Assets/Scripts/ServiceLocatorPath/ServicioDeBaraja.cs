using System.Collections.Generic;
using Gameplay.NewGameStates;

namespace ServiceLocatorPath
{
    public class ServicioDeBaraja: IBarajaDelPlayer
    {
        private Stack<string> _baraja;

        public ServicioDeBaraja()
        {
            _baraja = new Stack<string>();
        }

        public Stack<string> GetBaraja()
        {

            return _baraja;
        }

        public void AddCarta(string cartaTemplateId)
        {
            _baraja.Push(cartaTemplateId);
        }
    }
}