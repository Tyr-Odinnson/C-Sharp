using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerScript : MonoBehaviour
{
    public bool isTrue;

    private void Awake()
    {
        ButtonSetUp();
    }

    private void ButtonSetUp()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry click = new EventTrigger.Entry();
        click.eventID = EventTriggerType.PointerClick;
        click.callback.AddListener((e) => {
            Debug.Log(isTrue);
        });
        trigger.triggers.Add(click);

        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener((e) => {
            Debug.Log("asdggqgwydgqwudgqwedyg");
        });
        trigger.triggers.Add(enter);

    }
}