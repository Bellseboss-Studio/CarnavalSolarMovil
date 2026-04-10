using System;

namespace V2.Cards.Domain.Model
{
    [Serializable]
    public class Card : ICard
    {
        public string name;
        public string image;
        public string description;
        public string type;
    }
}