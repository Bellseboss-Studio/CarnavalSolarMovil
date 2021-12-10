using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private Gameplay.UsoDeCartas.DragComponent _dragComponent;
        [SerializeField] private bool puedeRecibirMasDeUno;
        public OnExitBalloonCorrectly OnExitBalloon;
        public delegate void OnExitBalloonCorrectly();
        public bool IsFull() => _dragComponent != null;

        public void Drop(Gameplay.UsoDeCartas.DragComponent dragComponent)
        {
            _dragComponent = dragComponent;
        }

        public void Clean()
        {
            _dragComponent = null;
            OnExitBalloon?.Invoke();
        }

        public bool IsEqual(Gameplay.UsoDeCartas.DragComponent dragComponent)
        {
            return _dragComponent != null && _dragComponent.Equals(dragComponent);
        }

        public bool HasDrag()
        {
            return _dragComponent != null;
        }

        public Gameplay.UsoDeCartas.DragComponent GetDragComponent()
        {
            return _dragComponent;
        }
    }
}
