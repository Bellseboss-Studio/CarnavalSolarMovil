namespace Gameplay.PersonajeStates
{
    public class PersonajeStateResult
    {
        public readonly int NextStateId;
        public readonly object ResultData;
        public PersonajeStateResult(int nextStateId, object resultData = null)
        {
            NextStateId = nextStateId;
            ResultData = resultData;
        }
    }
}