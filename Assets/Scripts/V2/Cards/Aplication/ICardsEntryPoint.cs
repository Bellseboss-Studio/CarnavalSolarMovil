namespace V2.Cards.Aplication
{
    public interface ICardsEntryPoint
    {
        void Configure(IGameLoopByCards gameLoopByCardsRules);
        void Show();
        void Hide();
    }
}