using System.Threading.Tasks;

namespace Gameplay.PersonajeStates
{
    public interface IPersonajeState
    {
        Task<PersonajeStateResult> DoAction(object data);
    }
}