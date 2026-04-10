using UnityEngine;
using UnityEngine.UI;
using V2.Cards.Aplication;
using Object = UnityEngine.Object;

public class GameLoopByCardsRules : MonoBehaviour, IGameLoopByCards
{
    [SerializeField, InterfaceType(typeof(ICardsEntryPoint))]
    private Object cards;

    private ICardsEntryPoint Cards => cards as ICardsEntryPoint;

    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject generalUi;


    private void Start()
    {
        Cards.Configure(this);
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        generalUi.SetActive(true);
    }

    private void OnStartGameButtonClicked()
    {
        Cards.Show();
        generalUi.SetActive(false);
    }
}