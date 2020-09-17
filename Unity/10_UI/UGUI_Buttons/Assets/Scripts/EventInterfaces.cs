using UnityEngine;
using UnityEngine.EventSystems;

public class EventInterfaces : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Aaaaargh");
    }
}
