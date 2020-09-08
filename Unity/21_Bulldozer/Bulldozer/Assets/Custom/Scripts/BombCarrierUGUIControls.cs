using UnityEngine;
using UnityEngine.EventSystems;

public class BombCarrierUGUIControls : BombCarrier
{
    protected override void Update()
    {
        base.Update();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Interact();
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                        return;
                    }

                    Interact();
                    break;
            }
        }
    }
#endif
}
