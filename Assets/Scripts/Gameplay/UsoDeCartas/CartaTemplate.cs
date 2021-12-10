using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(Gameplay.UsoDeCartas.DragComponent))]
    public class CartaTemplate : MonoBehaviour
    {
        private DragComponent _dragComponent;

        public void Configurate()
        {
            _dragComponent = GetComponent<DragComponent>();
            _dragComponent.OnDragging += Dragging;
            _dragComponent.OnDropCompleted += DropCompleted;
            _dragComponent.OnFinishDragging += FinishDragging;
        }

        private void FinishDragging()
        {
            Debug.Log("Se Termino De Draggear");
        }

        private void DropCompleted()
        {
            Debug.Log("Se Solto");
        }

        private void Dragging()
        {
            Debug.Log("Se Esta Draggeando");
        }
    }
}

