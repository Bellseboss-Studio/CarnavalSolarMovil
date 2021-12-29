using Gameplay.UsoDeCartas;
using UnityEngine;

namespace Gameplay.NewGameStates
{
    public interface IFactoriaPersonajes
    {
        Personaje CreatePersonaje(Vector3 point, EstadististicasYHabilidadesDePersonaje getEstadisticas, bool enemigo);
    }
}