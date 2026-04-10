using UnityEngine;
using V2.Cards.Aplication;
using V2.Cards.Domain.UseCases;
using V2.Cards.Infra.Unity;

public class EntriPointCards : MonoBehaviour, ICardsEntryPoint
{
    private IGameLoopByCards _gameLoopByCardsRules;

    //UseCases
    private LoadAllCardsUseCase _loadAllCardsUseCase;

    [SerializeField] private GameObject panelCards;
    [SerializeField] private CardsConfigurationSO cardsConfigurationSO;
    private CardsConfigurationSO _cardsConfiguration;


    public void Configure(IGameLoopByCards gameLoopByCardsRules)
    {
        _gameLoopByCardsRules = gameLoopByCardsRules;
        Hide();
        _cardsConfiguration = Instantiate(cardsConfigurationSO);
        _loadAllCardsUseCase = new LoadAllCardsUseCase(_cardsConfiguration.GetAllCards());
    }

    public void Show()
    {
        panelCards.SetActive(true);
        
    }

    public void Hide()
    {
        panelCards.SetActive(false);
    }
}