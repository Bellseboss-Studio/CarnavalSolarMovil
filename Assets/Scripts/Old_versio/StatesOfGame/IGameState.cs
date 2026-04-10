using System.Collections;

namespace StatesOfEnemies
{
    public interface IGameState
    {
        IEnumerator DoAction(IBehavior behavior);
    }
}