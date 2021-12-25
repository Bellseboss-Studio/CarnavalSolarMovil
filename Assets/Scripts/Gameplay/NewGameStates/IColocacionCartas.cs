using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public interface IColocacionCartas
    {
        List<GameObject> GetPosicionesDeCartas();

        int GetNumeroDePosiciones();
        int GetPosicionDeUltimaCartaInstanciada();
        bool PuedoSacarOtraCarta();
        GameObject GetSiguientePosicionDeCarta();
        string GetNextCartaId();
        void HayCartaEnPosicionInstanciada();
        void YaNoHayCartaEnPosicion(int pos);
        List<int> ObtenerPocisionesSinCartas();
        GameObject GetPosicionDeCarta(int posicionSinCarta);
        void HayCartaEnPosicion(int posicionSinCarta);
    }
}