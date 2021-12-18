using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(Gameplay.UsoDeCartas.DragComponent))]
    public class CartaTemplate : MonoBehaviour, ICartaTemplate
    {
        private DragComponent _dragComponent;
        private FactoriaPersonaje _factoriaPersonaje;
        [SerializeField] private string id;
        [SerializeField] private string modelo3DId, targetComponentId, rutaComponentId, interaccionComponentId;
        [SerializeField] private float distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo;
        public string Id => id;
        
        
        public void Configurate(FactoriaPersonaje factoriaPersonaje)
        {
            _dragComponent = GetComponent<DragComponent>();
            _dragComponent.OnDragging += Dragging;
            _dragComponent.OnDropCompleted += DropCompleted;
            _dragComponent.OnFinishDragging += FinishDragging;
            _factoriaPersonaje = factoriaPersonaje;
        }

        private void FinishDragging()
        {
            Debug.Log("Se Termino De Draggear");
        }

        private void DropCompleted(Vector3 hitPoint)
        {
            _factoriaPersonaje.CreatePersonaje(hitPoint, new EstadististicasYHabilidadesDePersonaje(modelo3DId, targetComponentId, interaccionComponentId, rutaComponentId, distanciaDeInteraccion, health, velocidadDeInteraccion, velocidadDeMovimiento, damage, escudo));
            gameObject.SetActive(false);
        }

        private void Dragging()
        {
            Debug.Log("Se Esta Draggeando");
        }
    }
}

