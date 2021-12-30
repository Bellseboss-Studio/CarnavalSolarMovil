using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class DragComponent : MonoBehaviour
{
    [SerializeField] private bool isDetachable;
    [SerializeField] private RectTransform initialPosition;
    [SerializeField] private Transform dondePosicionarLaCarta;
    public ZonaDeDropeo Zona { get; set; }
    private RectTransform _canvasRectTransform;
    private GameObject _finalPosition;
    private EventTrigger.Entry entry_0;
    private EventTrigger.Entry entry_1;
    private EventTrigger.Entry entry_2; 
    private Camera _camera;
    private bool _isInUse;
    public bool canUseComponent;
    public OnDraggingElement OnDragging;
    public OnDraggingElement OnFinishDragging;
    public OnDraggingElementCompleted OnDropCompleted;
    public bool estaSeleccionadaLaCarta;
    
    public delegate void OnDraggingElement();
    public delegate void OnDraggingElementCompleted(Vector3 hitPoint);
    
    private void Start()
    {
        _camera = Camera.main;
        canUseComponent = true;
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
        RaycastHit[] hits;
        var posicion = transform.position;
        // Does the ray intersect any objects excluding the player layer
        var _mousePos = Input.mousePosition;
        _mousePos.z = 2;
        hits = Physics.RaycastAll(_camera.transform.position, (_camera.ScreenToWorldPoint(_mousePos) - _camera.transform.position), Mathf.Infinity);
        Debug.DrawRay(_camera.transform.position, (_camera.ScreenToWorldPoint(Input.mousePosition)) * 100, Color.yellow);
        bool colisionoConAlgo = false;
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<DropComponent>(out var drop) && drop.Area == Zona)
            {
                drop.Drop(this);
                OnDropCompleted?.Invoke(hit.point);
                colisionoConAlgo = true;
                break;   
            }
        }
        if (!colisionoConAlgo)
        {
            RestartPosition();
            estaSeleccionadaLaCarta = false;
            OnFinishDragging?.Invoke();
        }
        //var position = _finalPosition.transform.position;
        //gameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y, 0);
        RestartPosition();
    }

    public void RestartPosition()
    {
        var position = initialPosition.position;
        gameObject.GetComponent<RectTransform>().position = initialPosition.GetComponent<RectTransform>().position;
    }

    private void BeginDrag_(PointerEventData data)
    {
        OnDragging?.Invoke();
    }

    private void ObjectDragging_(PointerEventData data)
    {
        /*if (!canUseComponent) return;
        if (!estaSeleccionadaLaCarta)
        {
            estaSeleccionadaLaCarta = true;
            GetComponent<RectTransform>().position = dondePosicionarLaCarta.position;
            return;
        }*/
        transform.position = Input.mousePosition;
        RaycastHit[] hits;
        var posicion = transform.position;
        // Does the ray intersect any objects excluding the player layer
        hits = Physics.RaycastAll(_camera.transform.position, posicion - _camera.transform.position, Mathf.Infinity);
        //Debug.Log(Input.mousePosition);
        var _mousePos = Input.mousePosition;
        _mousePos.z = 2; // select distance = 10 units from the camera
        Debug.DrawRay(_camera.transform.position, (_camera.ScreenToWorldPoint(_mousePos) - _camera.transform.position) * 100,  Color.yellow);
        
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<DropComponent>(out var drop))
            {
                drop.Drop(this);
                //Debug.Log("did hit");
                break;
            }
            else
            {
                //Debug.Log(hit.collider.gameObject.name);
            }
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

    public void Configure(RectTransform initial_Position, RectTransform canvasRectTransform, Transform transformParameter)
    {
        //_factoriaCarta = factoriaCartas;
        dondePosicionarLaCarta = transformParameter;
        _canvasRectTransform = canvasRectTransform;
        initialPosition = initial_Position;
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
