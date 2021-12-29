using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Personajes.InteraccionComponents;
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
                if (personaje.enemigo == origen.enemigo && Vector3.Distance(personaje.transform.position, origen.transform.position)< 4)
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

        public void AumentarVelocidadMovimientoPorCincoSegundos(Personaje personaje, DaniarYAumentarVelocidadAlMatar interaccionComponent)
        {
            if (interaccionComponent.YaAumenteMiVelocidad)
            {
                interaccionComponent.YaAumenteMiVelocidad = true;
                StartCoroutine(AumentarVelocidadPorCincoSegundosCoroutine());
            }

            IEnumerator AumentarVelocidadPorCincoSegundosCoroutine()
            {
                personaje.velocidadDeMovimiento += (personaje.velocidadDeMovimiento * .5f);
                yield return new WaitForSeconds(5);
                interaccionComponent.YaAumenteMiVelocidad = false;
            }
        }

        public void AumentarVelocidadMovimientoPersonajePorSegundos(Personaje personaje, float porcentaje, float tiempo)
        {
            var velocidadMovimientoAumentada = personaje.velocidadDeMovimiento * (porcentaje / 100);
            StartCoroutine(
                AumentarVelocidadMovimientoPersonajePorSegundosCoroutine(personaje, velocidadMovimientoAumentada,
                    tiempo));
        }

        public void DisminuirVelocidadInteraciconPersonajePorSegundos(Personaje personaje, float porcentaje, float tiempo)
        {
            var velocidadInteraccionDisminuida = personaje.velocidadDeInteraccion * (porcentaje / 100);
            StartCoroutine(
                DisminuirVelocidadInteraciconPersonajePorSegundosCoroutine(personaje, velocidadInteraccionDisminuida,
                    tiempo));
        }

        public void AumentarArmaduraPorSegundos(Personaje origen, float porcentajeAAumentar, float tiempo)
        {
            StartCoroutine(AumentarArmaduraPorSegundosCoroutine(origen, porcentajeAAumentar, tiempo));
        }

        public void ObtenerInmunidadPorSegundos(Personaje origen, float tiempo)
        {
            StartCoroutine(ObtenerInmunidadPorSegundosCoroutine(origen, tiempo));
        }

        public Personaje GetAliadoMasCercano(Personaje origen)
        {
            var personajes = FindObjectsOfType<Personaje>();
            List<Personaje> _personajesList = new List<Personaje>();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo == origen.enemigo)
                {
                    _personajesList.Add(personaje);
                }
            }

            Personaje _personajeMasCercano = null;
            
            float distance = 100;
            foreach (var personajeList in _personajesList)
            {
                if (Vector3.Distance(origen.transform.position, personajeList.transform.position) < distance)
                {
                    _personajeMasCercano = personajeList;
                    distance = Vector3.Distance(origen.transform.position, personajeList.transform.position);
                }
            }
            
            return _personajeMasCercano;
        }

        IEnumerator ObtenerInmunidadPorSegundosCoroutine(Personaje origen, float tiempo)
        {
            origen.esInmune = false;
            yield return new WaitForSeconds(tiempo);
            origen.esInmune = true;
        }

        IEnumerator AumentarArmaduraPorSegundosCoroutine(Personaje origen, float armaduraAAumentar, float tiempo)
        {
            origen.armadura += armaduraAAumentar;
            yield return new WaitForSeconds(tiempo);
            origen.armadura -= armaduraAAumentar;
        }
        
        IEnumerator AumentarVelocidadMovimientoPersonajePorSegundosCoroutine(Personaje personaje, float velocidadMovimientoPotenciada, float tiempo)
        {
            personaje.velocidadDeMovimiento += velocidadMovimientoPotenciada;
            yield return new WaitForSeconds(tiempo);
            personaje.velocidadDeMovimiento -= velocidadMovimientoPotenciada;
        }

        IEnumerator DisminuirVelocidadInteraciconPersonajePorSegundosCoroutine(Personaje personaje, float velocidadInteraccionPotenciada, float tiempo)
        {
            personaje.velocidadDeInteraccion += velocidadInteraccionPotenciada;
            yield return new WaitForSeconds(tiempo);
            personaje.velocidadDeInteraccion -= velocidadInteraccionPotenciada;
        }
        
        private void AplicarDanio(Personaje target, float danioARealizar)
        {
            _interaccionBehaviour.AplicarDanio(target,danioARealizar);
        }

        public void InteraccionInicial(Personaje personaje)
        {
            _interaccionBehaviour.InteraccionAlIniciarTurno(personaje);
        }
    }
}