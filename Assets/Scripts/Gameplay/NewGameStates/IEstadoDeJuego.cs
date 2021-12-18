using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using StatesOfEnemies;

namespace Gameplay.NewGameStates
{
    public interface IEstadoDeJuego
    {
        void InitialConfiguration();
        void FinishConfiguration();
        Task<PersonajeStateResult> DoAction(object data);
    }
}