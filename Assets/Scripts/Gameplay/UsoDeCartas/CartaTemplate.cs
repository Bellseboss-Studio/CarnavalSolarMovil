using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(Gameplay.UsoDeCartas.DragComponent))]
    public class CartaTemplate : MonoBehaviour, ICartaTemplate
    {
        private DragComponent _dragComponent;
        private FactoriaPersonaje _factoriaPersonaje;
        [SerializeField] private string id;
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
            _factoriaPersonaje.CreateCarta(hitPoint);
        }

        private void Dragging()
        {
            Debug.Log("Se Esta Draggeando");
        }
    }
}

