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
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.Log(new Vector3(transform.position.x/1920 * 5, transform.position.y/1080 * 5));
        Debug.Log(_camera.nearClipPlane);
        if (Physics.Raycast(new Vector3(transform.position.x/1920 * 5, transform.position.y/1080 * 5),_camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x/1920 * 5, transform.position.y/1080 * 5), _camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(_camera.ScreenToWorldPoint(transform.position), _camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
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
        var angle = 0;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
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
        var position = new Vector3(mousePos.x, mousePos.y, 0);
        //Debug.Log(position);
        rectTransform.position = position;
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.Log(new Vector3(transform.position.y/1080 * 15,0, transform.position.x/1920 * -15));
        Debug.Log(_camera.nearClipPlane);
        if (Physics.Raycast(new Vector3(transform.position.y/1080 * 15,0, transform.position.x/1920 * -15),_camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.y/1080 * 15,0, transform.position.x/1920 * -15), _camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(_camera.ScreenToWorldPoint(transform.position), _camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
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

    public void Configure(DropComponent drop)
    {
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
