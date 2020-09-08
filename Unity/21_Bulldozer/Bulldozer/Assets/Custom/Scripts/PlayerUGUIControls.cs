using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUGUIControls : Player {
    public Button left;
    public Button right;
    public Button up;
    public Button down;

    protected override void Initialize() {
        base.Initialize();

        SetButtons();
    }

    protected override void GetInput() {
        // Do nothing.
    }

    private void SetButtons() {
        SetButton(left , (e) => Left() , (e) => NullifyX());
        SetButton(right, (e) => Right(), (e) => NullifyX());
        SetButton(up   , (e) => Up()   , (e) => NullifyY());
        SetButton(down , (e) => Down() , (e) => NullifyY());
    }

    private void SetButton(Button _button, UnityAction<BaseEventData> _enter, UnityAction<BaseEventData> _exit) {
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(_enter);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(_exit);
        
        EventTrigger trigger = _button.gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);
    }

    private void NullifyX() {
        direction.x = 0;
    }

    private void NullifyY() {
        direction.y = 0;
    }

    private void Left() {
        direction.x = -1 * turnSpeed;
    }

    private void Right() {
        direction.x = 1 * turnSpeed;
    }

    private void Up() {
        direction.y = 1 * speed;
    }

    private void Down() {
        direction.y = -1 * speed;
    }
}
