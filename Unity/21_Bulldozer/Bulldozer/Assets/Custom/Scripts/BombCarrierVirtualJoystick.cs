using UnityEngine;
using UnityEngine.EventSystems;

public class BombCarrierVirtualJoyStick : BombCarrier
{
    protected override void Update()
    {
        base.Update();

#if UNITY_EDITOR || UNITY_EDITOR_WIN
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || Input.mousePosition.x < (Screen.width / 2f))
            {
                return;
            }

            Interact();
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) || touch.position.x < (Screen.width / 2f))
                {
                    return;
                }

                Interact();
            }
        }
#endif
    }
}
