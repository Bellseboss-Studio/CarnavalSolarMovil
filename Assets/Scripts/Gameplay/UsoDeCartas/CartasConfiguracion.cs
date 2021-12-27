using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [CreateAssetMenu(menuName = "Configuracion de cartas")]
    public class CartasConfiguracion : ScriptableObject
    {
        private Dictionary<string, CartaTemplate> _CartasTemplates;
        [SerializeField] private CartaTemplate[] cartaTemplates;

        private void Awake()
        {
            _CartasTemplates = new Dictionary<string, CartaTemplate>();
            foreach (var cartaTemplate in cartaTemplates)
            {
                _CartasTemplates.Add(cartaTemplate.Id, cartaTemplate);
            }
        }

        public CartaTemplate GetCartaTemplate(string id)
        {
            if (!_CartasTemplates.TryGetValue(id, out var cartaTemplate))
            {
                throw new Exception($"La carta con la id {id} no existe");
            }
            else return cartaTemplate;
        }

        public CartaTemplate[] GetCartasTemplate()
        {
            return cartaTemplates;
        }
    }
}