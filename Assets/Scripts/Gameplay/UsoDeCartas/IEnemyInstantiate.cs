using Gameplay.NewGameStates;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IEnemyInstantiate
    {
        void InstanciateEnemy(IFactoriaPersonajes factoriaPersonaje);
        void Configuration(IFactoriaCarta factoriaCarta, IFactoriaPersonajes factoriaPersonajes);
        void InstanciateHeroEnemy(IFactoriaPersonajes factoriaPersonaje);
    }
}