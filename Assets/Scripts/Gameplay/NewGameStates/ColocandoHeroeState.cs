using System;
using System.Threading.Tasks;
using DG.Tweening;
using Gameplay.PersonajeStates;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gameplay.NewGameStates
{
    public class ColocandoHeroeState : IEstadoDeJuego
    {
        private IMediadorDeEstadosDelJuego _mediadorDeEstadosDelJuego;
        private readonly RectTransform _panelColocarHeroe;

        public ColocandoHeroeState(IMediadorDeEstadosDelJuego mediadorDeEstadosDelJuego, RectTransform panelColocarHeroe)
        {
            _mediadorDeEstadosDelJuego = mediadorDeEstadosDelJuego;
            _panelColocarHeroe = panelColocarHeroe;
        }

        public void InitialConfiguration()
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().ComienzaAContarElTiempo(1);
            _mediadorDeEstadosDelJuego.PedirColocacionDeHeroe();
            var sequence = DOTween.Sequence();
            sequence.Insert(0, _panelColocarHeroe.DOScale(1, .45f).SetEase(Ease.OutBack));
        }

        public void FinishConfiguration()
        {
            ServiceLocator.Instance.GetService<IServicioDeTiempo>().DejaDeContarElTiempo();
        }

        public async Task<PersonajeStateResult> DoAction(object data)
        {
            while (_mediadorDeEstadosDelJuego.SeCololoElHeroe())
            {
                _mediadorDeEstadosDelJuego.PedirColocacionDeHeroe();
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return new PersonajeStateResult(ConfiguracionDeLosEstadosDelJuego.Pausa);
        }
    }
}