using System;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragComponent : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private UILineRenderer linesRender;
    private Camera _camera;
    private EventTrigger.Entry entry_0;
    private EventTrigger.Entry entry_1;
    private EventTrigger.Entry entry_2;
    private RectTransform rectTransform;
    private Stack<GameObject> listOfChain;

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

    private void ObjectDrop_(PointerEventData arg0)
    {
        Debug.Log("drop");
    }

    private void ObjectDragging_(PointerEventData arg0)
    {
        Debug.Log("Dragin");
        var mousePos = Input.mousePosition;
        Debug.Log($"mousePos {mousePos} Input.mousePosition {Input.mousePosition}");
        var position = new Vector3(mousePos.x, mousePos.y, 0);
        Debug.Log($"position {position}");
        rectTransform.position = position;
    }

    private void BeginDrag_(PointerEventData arg0)
    {
        Debug.Log("begin");
    }

    private void Awake()
    {
        listOfChain = new Stack<GameObject>();
        listOfChain.Push(origin);
        listOfChain.Push(gameObject);
        linesRender.Points =new Vector2[listOfChain.Count];
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        rectTransform = gameObject.GetComponent<RectTransform>();
        CreateEventForDragAndDrop();
    }

    // Update is called once per frame
    void Update()
    {
        PrintLine();
    }
    private void PrintLine()
    {
        if (listOfChain.Count <= 0)
        {
            linesRender.Points = Array.Empty<Vector2>();
            return;
        }
        var countPosition = 0;
        if (listOfChain.Count > 0)
        {
            countPosition = listOfChain.Count;
        }
        var positions = new Vector2[countPosition];

        var positionCount = 0;
        foreach (var chain in listOfChain)
        {
            positions[positionCount] = chain.transform.localPosition;
            positionCount++;
        }
        linesRender.Points = new Vector2[listOfChain.Count];
        linesRender.Points = positions;
    }
}
