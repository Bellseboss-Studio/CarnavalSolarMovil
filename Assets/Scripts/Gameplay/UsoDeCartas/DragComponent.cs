using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class DragComponent : MonoBehaviour
{
    [SerializeField] private bool isDetachable;
    [SerializeField] private RectTransform initialPosition;
    private GameObject _finalPosition;
    private EventTrigger.Entry entry_0;
    private EventTrigger.Entry entry_1;
    private EventTrigger.Entry entry_2;
    private Camera _camera;
    private DropComponent _drop;
    private bool _isInUse;
    public bool canUseComponent;
    public OnDraggingElement OnDragging;
    public OnDraggingElement OnFinishDragging;
    public OnDraggingElement OnDropCompleted;
    [SerializeField] private FactoriaCarta _factoriaCarta;
    
    public delegate void OnDraggingElement();


    private void Start()
    {
        _camera = Camera.main;
        canUseComponent = true;
        CreateEventForDragAndDrop();
    }

    public void CreateEventForDragAndDrop()
    {
        var trigger_Object = gameObject.AddComponent<EventTrigger>();

        entry_0 = new EventTrigger.Entry();
        entry_0.eventID = EventTriggerType.BeginDrag;
        entry_0.callback.AddListener((data) => { BeginDrag_((PointerEventData) data); });

        entry_1 = new EventTrigger.Entry();
        entry_1.eventID = EventTriggerType.Drag;
        entry_1.callback.AddListener((data) => { ObjectDragging_((PointerEventData) data); });

        entry_2 = new EventTrigger.Entry();
        entry_2.eventID = EventTriggerType.EndDrag;
        entry_2.callback.AddListener((data) => { ObjectDrop_((PointerEventData) data); });

        trigger_Object.triggers.Add(entry_0);
        trigger_Object.triggers.Add(entry_1);
        trigger_Object.triggers.Add(entry_2);
    }

    private void ObjectDrop_(PointerEventData data)
    {
        //logica del raycast
        RaycastHit hit;
        var posicion = transform.position;
        // Does the ray intersect any objects excluding the player layer
        Debug.Log(posicion);
        Debug.Log(_camera.nearClipPlane);
        if (Physics.Raycast(posicion,transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(posicion, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            _factoriaCarta.CreateCarta(hit.point);
        }
        else
        {
            Debug.DrawRay(posicion, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
            Debug.Log("Did not Hit");
        }
        
        OnFinishDragging?.Invoke();
        if (!isDetachable)
        {
            RestartPosition();
            return;
        }

        var position = _finalPosition.transform.position;
        gameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y, 0);
        if (_finalPosition.TryGetComponent<DropComponent>(out var drop))
        {
            drop.Drop(this);
            OnDropCompleted?.Invoke();
        }
    }

    public void RestartPosition()
    {
        var position = initialPosition.position;
        gameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y, 0);
    }

    private void BeginDrag_(PointerEventData data)
    {
        OnDragging?.Invoke();
    }

    private void ObjectDragging_(PointerEventData data)
    {
        if (!canUseComponent) return;
        var mousePos = Input.mousePosition;
        //Debug.Log(Input.mousePosition);
        var rectTransform = gameObject.GetComponent<RectTransform>();
        //Debug.Log(rectTransform);
        Debug.Log(mousePos);
        rectTransform.localPosition = new Vector3(Input.mousePosition.x - 960, Input.mousePosition.y - 540);
        RaycastHit hit;
        var posicion = transform.position;
        // Does the ray intersect any objects excluding the player layer
        Debug.Log(posicion);
        Debug.Log(_camera.nearClipPlane);
        if (Physics.Raycast(posicion,transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(posicion, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(posicion, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<DropComponent>(out var dropComponent)) return;
        
        isDetachable = !dropComponent.IsFull();
        _finalPosition = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<DropComponent>(out var dropComponent)) return;
        
        if (dropComponent.IsFull() && dropComponent.IsEqual(this))
        {
            dropComponent.Clean();
        }
        isDetachable = false;
        _finalPosition = initialPosition.gameObject;
    }

    public void Configure(DropComponent drop, FactoriaCarta factoriaCartas)
    {
        _factoriaCarta = factoriaCartas;
        _drop = drop;
        CreateEventForDragAndDrop();
    }

    public bool IsInUse()
    {
        return _isInUse;
    }
    
    public void InUse()
    {
        _isInUse = true;
    }

    public void InAnuse()
    {
        _isInUse = false;
    }
}
}
