using UnityEngine;

namespace Gameplay
{
    public class EstadisticasCarta
    {
        private float _distanciaDeInteraccion;
        private float _health;
        private float _velocidadDeInteraccion;
        private float _velocidadDeMovimiento;
        private float _damage;
        private float _escudo;
        public AnimationClip Caminar { get; }
        public AnimationClip Golpear { get; }
        public AnimationClip Morir { get; }
        public AnimationClip Idle { get; }

        public EstadisticasCarta(float distanciaDeInteraccion, float health, float velocidadDeInteraccion, float velocidadDeMovimiento, float damage, float escudo, AnimationClip caminar, AnimationClip golpear, AnimationClip morir, AnimationClip idle)
        {
            _distanciaDeInteraccion = distanciaDeInteraccion;
            _health = health;
            _velocidadDeInteraccion = velocidadDeInteraccion;
            _velocidadDeMovimiento = velocidadDeMovimiento;
            _damage = damage;
            _escudo = escudo;
            Caminar = caminar;
            Golpear = golpear;
            Morir = morir;
            Idle = idle;
        }

        public float DistanciaDeInteraccion => _distanciaDeInteraccion;
        public float Health => _health;
        public float VelocidadDeInteraccion => _velocidadDeInteraccion;
        public float VelocidadDeMovimiento => _velocidadDeMovimiento;
        public float Damage => _damage;
        public float Escudo => _escudo;

    }
}