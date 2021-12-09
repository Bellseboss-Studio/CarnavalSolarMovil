using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Personajes.TargetComponents
{
    public class BuscartresEnemigosMasCercanos : TargetBehaviour
    {
        public override List<Personaje> GetTargets()
        {
            return _targets;
        }

        public override void BuscaLosTargets()
        {
            List<Personaje> personajesMasCercanos = null;
            List<Personaje> personajesList = new List<Personaje>();
            var personajes = _targetComponent.GetPersonajes();
            foreach (var personaje in personajes)
            {
                if (personaje.enemigo != _personaje.enemigo && personaje.isTargeteable)
                {
                    personajesList.Add(personaje);
                }
            }
            const float distance = 100;
            for (var i = 0; i < personajesList.Count; i++){
                for (var j = 0; j< personajesList.Count -1; j++)
                {
                    var position = _personaje.transform.position;
                    var distance1 = Vector3.Distance(personajesList[j].gameObject.transform.position, position);
                    var distance2 = Vector3.Distance(personajesList[j+1].gameObject.transform.position, position);
                    
                    if (distance1 > distance2)
                    {
                        (personajesList[j], personajesList[j+1]) = (personajesList[j+1], personajesList[j]);
                    }
                }
            }

            switch (personajesList.Count)
            {
                case 0:
                    break;
                case 1:
                {
                    personajesMasCercanos = new List<Personaje>() {personajesList[0]};
                    if (personajesMasCercanos[0] != null && !_targets.Contains(personajesMasCercanos[0]) && personajesMasCercanos[0].isTargeteable) _targets.Add(personajesMasCercanos[0]);
                    if (personajesMasCercanos[0] != null)
                    {
                        if (!personajesMasCercanos[0].GetTargetComponent().GetTargetTargetedBy().Contains(_personaje))
                        {
                            Debug.Log("se añadio a la lista");
                            personajesMasCercanos[0].GetTargetComponent().AddTargetedBy(_personaje);
                        }
                        Debug.Log(personajesMasCercanos[0].GetTargetComponent().GetTargetTargetedBy().Contains(_personaje));
                    }
                }
                    break;
                case 2:
                {
                    personajesMasCercanos = new List<Personaje>() {personajesList[0], personajesList[1]};
                }
                    foreach (var personajeMasCercano in personajesMasCercanos)
                    {
                        if (personajeMasCercano != null && !_targets.Contains(personajeMasCercano) && personajeMasCercano.isTargeteable) _targets.Add(personajeMasCercano);
                        if (personajeMasCercano != null)
                        {
                            if (!personajeMasCercano.GetTargetComponent().GetTargetTargetedBy().Contains(_personaje))
                            {
                                Debug.Log("se añadio a la lista");
                                personajeMasCercano.GetTargetComponent().AddTargetedBy(_personaje);
                            }
                            Debug.Log(personajeMasCercano.GetTargetComponent().GetTargetTargetedBy().Contains(_personaje));
                        }
                    }
                    break;
                default:
                    personajesMasCercanos = new List<Personaje>() {personajesList[0], personajesList[1], personajesList[2]};
                    for (int i = 0; i < 3; i++)
                    {
                        if (personajesMasCercanos[i] != null && !_targets.Contains(personajesMasCercanos[i]) && personajesMasCercanos[i].isTargeteable) _targets.Add(personajesMasCercanos[i]);
                        if (personajesMasCercanos[i] != null)
                        {
                            if (!personajesMasCercanos[i].GetTargetComponent().GetTargetTargetedBy().Contains(_personaje))
                            {
                                Debug.Log("se añadio a la lista");
                                personajesMasCercanos[i].GetTargetComponent().AddTargetedBy(_personaje);
                            }
                            Debug.Log(personajesMasCercanos[i].GetTargetComponent().GetTargetTargetedBy().Contains(_personaje));
                        }
                    }
                    break;

            }
        }

    }
}