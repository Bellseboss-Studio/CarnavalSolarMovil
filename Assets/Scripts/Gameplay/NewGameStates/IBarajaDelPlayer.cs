using System.Collections.Generic;

namespace Gameplay.NewGameStates
{
    public interface IBarajaDelPlayer
    {
        Stack<string> GetBaraja();
        void AddCarta(string cartaTemplateId);
    }
}