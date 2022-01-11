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

        public ZonaDeDropeo Zona => zona;

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
            _dropComponent.OcultarZona();
            //Debug.Log("Se Termino De Draggear");
        }

        private void DropCompleted(Vector3 hitPoint)
        {
            _dropComponent.OcultarZona();
            if (ServiceLocator.Instance.GetService<IServicioDeEnergia>().TieneEnergiaSuficiente(costoEnergia))
            {
                ServiceLocator.Instance.GetService<IColocacionCartas>().YaNoHayCartaEnPosicion(_posicionEnBaraja);
                _factoriaPersonaje.CreatePersonaje(hitPoint,GetEstadisticas());
                gameObject.SetActive(false);
                ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateEnemy(_factoriaPersonaje);
            }
        }

        private void Dragging()
        {
            _dropComponent.MostrarZona();
            //Debug.Log("Se Esta Draggeando");
        }

        public EstadististicasYHabilidadesDePersonaje GetEstadisticas()
        {
            return new EstadististicasYHabilidadesDePersonaje(modelo3DId, targetComponentId, interaccionComponentId,
                rutaComponentId, distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento,
                damage, escudo, caminar, golpear, morir, idle);
        }
    }
}

