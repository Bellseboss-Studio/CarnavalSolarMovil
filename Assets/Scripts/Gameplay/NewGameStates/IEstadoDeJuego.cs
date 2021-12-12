using System.Collections;
using System.Threading.Tasks;
using Gameplay.PersonajeStates;
using StatesOfEnemies;

namespace Gameplay.NewGameStates
{
    public interface IEstadoDeJuego
    {
        Task<PersonajeStateResult> DoAction(object data);
    }
}