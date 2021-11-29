using UnityEngine;

namespace Gameplay
{
    public abstract class DistanciaComponent : MonoBehaviour
    {
        [SerializeField] protected float distanciaDeAtaque;

        public abstract float GetDistanciaDeAtaque();
        
        public abstract void SetDistanciaDeAtaque(float distancia);
    }
}