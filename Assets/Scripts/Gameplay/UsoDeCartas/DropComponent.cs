using UnityEngine;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(BoxCollider))]
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private ZonaDeDropeo area;
        [SerializeField] private DragComponent _dragComponent;
        [SerializeField] private bool puedeRecibirMasDeUno;
        public OnExitBalloonCorrectly OnExitBalloon;
        public delegate void OnExitBalloonCorrectly();
        public bool IsFull() => _dragComponent != null;
        public ZonaDeDropeo Area => area;

        public void Drop(DragComponent dragComponent)
        {
            _dragComponent = dragComponent;
        }

        public void Clean()
        {
            _dragComponent = null;
            OnExitBalloon?.Invoke();
        }

        public bool IsEqual(DragComponent dragComponent)
        {
            return _dragComponent != null && _dragComponent.Equals(dragComponent);
        }

        public bool HasDrag()
        {
            return _dragComponent != null;
        }

        public DragComponent GetDragComponent()
        {
            return _dragComponent;
        }
    }
}
