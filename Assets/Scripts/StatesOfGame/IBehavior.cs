using UnityEngine;

namespace StatesOfEnemies
{
    public interface IBehavior
    {
        void SetNextState(int nextStateFromState);
        int GetNextState();
    }
}