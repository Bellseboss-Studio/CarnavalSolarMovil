using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public interface IColocacionCartas
    {
        List<GameObject> GetPosicionesDeCartas();

        int GetNumeroDePosiciones();
        bool PuedoSacarOtraCarta();
        GameObject GetPosicionDeCarta();
        string GetNextCartaId();
    }
}