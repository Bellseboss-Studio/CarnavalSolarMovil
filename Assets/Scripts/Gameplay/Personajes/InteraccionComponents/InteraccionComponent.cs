using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class InteraccionComponent : MonoBehaviour, IInteraccionComponent
    {
        private InteraccionBehaviour _interaccionBehaviour;
        public void Configurate(Personaje origen, InteraccionBehaviour interaccionBehaviour)
        {
            _interaccionBehaviour = interaccionBehaviour;
            interaccionBehaviour.Configurate(origen, this);
        }
        
        public void Interactuar(List<Personaje> target)
        {
            _interaccionBehaviour.Interactuar(target);
        }

        public void AplicarInteraccion(Personaje origen)
        {
            _interaccionBehaviour.AplicarInteraccion(origen);
        }

        public void EjecucionDeInteraccion(Personaje target)
        {
            _interaccionBehaviour.EjecucionDeInteraccion(target);
        }

        public List<Personaje> GetAliadosCercanos(Personaje origen)
        {
            var personajes = FindObjectsOfType<Personaje>();
            List<Personaje> personajesList = new List<Personaje>();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo == origen.enemigo && Vector3.Distance(personaje.transform.position, origen.transform.position)< 2)
                {
                    personajesList.Add(personaje);
                }
            }

            return personajesList;
        }

        public float ObtenerDistancia(Personaje personaje, Personaje target)
        {
            return Vector3.Distance(personaje.transform.position, target.transform.position);
        }

        public Personaje GetAliadoConMenosVida(List<Personaje> aliadosCercanos)
        {
            Personaje aliadoCercanoConMenosVida = null;
            foreach (var aliadoCercano in aliadosCercanos)
            {
                if (aliadoCercanoConMenosVida == null)
                {
                    aliadoCercanoConMenosVida = aliadoCercano;
                }
                else
                {
                    if (aliadoCercanoConMenosVida.health > aliadoCercano.health)
                        aliadoCercanoConMenosVida = aliadoCercano;
                }
            }
            return aliadoCercanoConMenosVida;
        }

        public async void ReducirVelocidadDeMovimientoYAtaqueAlObjetivo(Personaje target, float i, float tiempoDeEspera)
        {
            var factorReduccion = i / 100;
            target.velocidadDeInteraccion -= factorReduccion;
            target.velocidadDeMovimiento -= factorReduccion;
            await Task.Delay(TimeSpan.FromSeconds(tiempoDeEspera));
            target.velocidadDeInteraccion += factorReduccion;
            target.velocidadDeMovimiento += factorReduccion;
        }
        
        public async void AplicarDanioProgresivamente(Personaje origen, Personaje target, float danioARealizar, int repeticiones, float tiempoDeEspera)
        {
            for (var i = 0; i < repeticiones; i++)
            {
                origen.GetInteractionComponent().AplicarDanio(target,danioARealizar);
                if (target.health <= 0)
                {
                    target.Muerte();
                    i = repeticiones;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(tiempoDeEspera));
                }
            }
        }

        private void AplicarDanio(Personaje target, float danioARealizar)
        {
            _interaccionBehaviour.AplicarDanio(target,danioARealizar);
        }
    }
}