using DG.Tweening;
using Gameplay.NewGameStates;
using ServiceLocatorPath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(Gameplay.UsoDeCartas.DragComponent))]
    public class CartaTemplate : MonoBehaviour, ICartaTemplate
    {
        private DragComponent _dragComponent;
        private DropComponent _dropComponent;
        private FactoriaPersonaje _factoriaPersonaje;
        [SerializeField] private string id;
        [SerializeField] private string modelo3DId;
        [SerializeField] private InteraccionComponentEnum interaccionComponentId;
        [SerializeField] private TargetComponentEnum targetComponentId;
        [SerializeField] private RutaComponentEnum rutaComponentId;
        [SerializeField] private float distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo;
        [SerializeField] private int costoEnergia;
        [SerializeField] private TextMeshProUGUI valorCarta;
        [SerializeField] private ZonaDeDropeo zona;
        [SerializeField] private AnimationClip caminar, golpear, morir, idle;
        [SerializeField] private bool esUnaCartaIlegal;
        [SerializeField] private Transform panelDeDescripcion;

        public ZonaDeDropeo Zona => zona;
        public int GetCostoEnergia => costoEnergia;

        private int _posicionEnBaraja;
        public string Id => id;
        public bool ESUnaCartaIlegal => esUnaCartaIlegal;
        public int PosicionEnBaraja
        {
            get => _posicionEnBaraja;
            set => _posicionEnBaraja = value;
        }

        public void Configurate(FactoriaPersonaje factoriaPersonaje, DropComponent dropComponent)
        {
            _dragComponent = GetComponent<DragComponent>();
            _dragComponent.OnDragging += Dragging;
            _dragComponent.OnDropCompleted += DropCompleted;
            _dragComponent.OnFinishDragging += FinishDragging;
            _dragComponent.Zona = zona;
            _factoriaPersonaje = factoriaPersonaje;
            var rectTransformRect = GetComponent<RectTransform>().rect;
            rectTransformRect.width = rectTransformRect.height;
            valorCarta.text = $"{costoEnergia}";
            _dropComponent = dropComponent;
            //Debug.Log(rectTransformRect.height);
            //Debug.Log(rectTransformRect.width);
        }

        private void FinishDragging()
        {
            OcultarDescripcion();
            _dropComponent.OcultarZona();
            //Debug.Log("Se Termino De Draggear");
        }

        private void DropCompleted(Vector3 hitPoint)
        {
            OcultarDescripcion();
            _dropComponent.OcultarZona();
            
            if (ServiceLocator.Instance.GetService<IServicioDeEnergia>().TieneEnergiaSuficiente(costoEnergia))
            {
                ServiceLocator.Instance.GetService<IColocacionCartas>().YaNoHayCartaEnPosicion(_posicionEnBaraja);
                _factoriaPersonaje.CreatePersonaje(hitPoint,GetEstadisticas());
                gameObject.SetActive(false);
            }
        }

        private void OcultarDescripcion()
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, panelDeDescripcion.DOScale(0, .4f).SetEase(Ease.InBack));
            sequence.OnComplete(() => panelDeDescripcion.gameObject.SetActive(false));
        }

        private void Dragging()
        {
            MostrarDescripcion();
            _dropComponent.MostrarZona();
            //Debug.Log("Se Esta Draggeando");
        }

        private void MostrarDescripcion()
        {
            panelDeDescripcion.gameObject.SetActive(true);
            var sequence = DOTween.Sequence();
            sequence.Insert(0, panelDeDescripcion.DOScale(1, .4f).SetEase(Ease.OutBack));
        }

        public EstadististicasYHabilidadesDePersonaje GetEstadisticas()
        {
            return new EstadististicasYHabilidadesDePersonaje(modelo3DId, targetComponentId, interaccionComponentId,
                rutaComponentId, distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento,
                damage, escudo, caminar, golpear, morir, idle);
        }
    }
}

