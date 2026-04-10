using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [CreateAssetMenu(menuName = "Configuracion de Personajes")]
    public class ConfiguracionDeModelo3DDePersonajes : ScriptableObject
    {
        private Dictionary<string, GameObject> _personajesId;
        [SerializeField] private GameObject[] personajes;

        private void Awake()
        {
            _personajesId = new Dictionary<string, GameObject>();
            foreach (var personaje in personajes)
            {
                _personajesId.Add(personaje.name, personaje);
            }
        }

        public GameObject GetPersonajePrefabById(string id)
        {
            if (!_personajesId.TryGetValue(id, out var gameObject))
            {
                throw new Exception($"El personaje con la id {id} no existe");
            }
            return gameObject.gameObject;
        }
    }
}