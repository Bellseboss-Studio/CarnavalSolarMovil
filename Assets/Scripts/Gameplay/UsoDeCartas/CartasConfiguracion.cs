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

        public Dictionary<string, CartaTemplate> GetCartasTemplate()
        { 
            var cartasTemplatesResult = new Dictionary<string, CartaTemplate>();
            foreach (var template in _CartasTemplates)
            {
                if (!template.Value.ESUnaCartaIlegal)
                {
                    cartasTemplatesResult.Add(template.Key, template.Value);
                }
            }
            return cartasTemplatesResult;
        }
    }
}