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
        [SerializeField] private int posicionEnBaraja;
        public string Id => id;
        public int PosicionEnBaraja
        {
            get => posicionEnBaraja;
            set => posicionEnBaraja = value;
        }

        public void Configurate(FactoriaPersonaje factoriaPersonaje)
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
            //Debug.Log(rectTransformRect.height);
            //Debug.Log(rectTransformRect.width);
        }

        private void FinishDragging()
        {
            //Debug.Log("Se Termino De Draggear");
        }

        private void DropCompleted(Vector3 hitPoint)
        {
            if (ServiceLocator.Instance.GetService<IServicioDeEnergia>().TieneEnergiaSuficiente(costoEnergia))
            {
                ServiceLocator.Instance.GetService<IColocacionCartas>().YaNoHayCartaEnPosicion(posicionEnBaraja);
                _factoriaPersonaje.CreatePersonaje(hitPoint,GetEstadisticas());
                gameObject.SetActive(false);
                ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateEnemy(_factoriaPersonaje);
            }
        }

        private void Dragging()
        {
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

