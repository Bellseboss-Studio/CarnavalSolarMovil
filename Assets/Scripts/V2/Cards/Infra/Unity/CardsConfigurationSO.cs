using System.Collections.Generic;
using UnityEngine;
using V2.Cards.Domain.Model;

namespace V2.Cards.Infra.Unity
{
    [CreateAssetMenu(menuName = "V2/Cards", fileName = "CardsConfigurationSO", order = 0)]
    public class CardsConfigurationSO : ScriptableObject
    {
        [SerializeField] private List<Card> cards;

        public List<Card> GetAllCards()
        {
            return cards;
        }
    }
}