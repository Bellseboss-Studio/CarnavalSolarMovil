using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    public interface IEnemyInstantiate
    {
        void InstanciateEnemy(FactoriaPersonaje factoriaPersonaje);
        void Configuration(IFactoriaCarta factoriaCarta);
        void InstanciateHeroEnemy(FactoriaPersonaje factoriaPersonaje);
    }
}