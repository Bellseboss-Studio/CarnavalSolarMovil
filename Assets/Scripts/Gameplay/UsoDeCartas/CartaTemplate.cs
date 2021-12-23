using ServiceLocatorPath;
using UnityEngine;

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
        public string Id => id;
        
        
        public void Configurate(FactoriaPersonaje factoriaPersonaje)
        {
            _dragComponent = GetComponent<DragComponent>();
            _dragComponent.OnDragging += Dragging;
            _dragComponent.OnDropCompleted += DropCompleted;
            _dragComponent.OnFinishDragging += FinishDragging;
            _factoriaPersonaje = factoriaPersonaje;
            var rectTransformRect = GetComponent<RectTransform>().rect;
            rectTransformRect.width = rectTransformRect.height;
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
                _factoriaPersonaje.CreatePersonaje(hitPoint,
                    new EstadististicasYHabilidadesDePersonaje(modelo3DId, targetComponentId, interaccionComponentId,
                        rutaComponentId, distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo));
            gameObject.SetActive(false);
        }

        private void Dragging()
        {
            //Debug.Log("Se Esta Draggeando");
        }
    }
}

