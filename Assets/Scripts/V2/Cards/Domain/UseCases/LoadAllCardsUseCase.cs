using System.Collections.Generic;
using V2.Cards.Domain.Model;

namespace V2.Cards.Domain.UseCases
{
    public class LoadAllCardsUseCase : ILoadAllCardsUseCase
    {
        private List<Card> _cardsConfiguration;

        public LoadAllCardsUseCase(List<Card> instantiate)
        {
            _cardsConfiguration = instantiate;
        }
    }
}