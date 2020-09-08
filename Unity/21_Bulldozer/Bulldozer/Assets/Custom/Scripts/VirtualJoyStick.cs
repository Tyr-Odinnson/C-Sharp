using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour
{
    public PlayerVirtualJoystick player;

    private bool isDragging;
    private Image bg;
    private Image joystick;
    private Vector2 originalMousePosition;
    private Vector2 direction;

    private void Awake()
    {
        bg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();

        Hide();
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_EDITOR_WIN
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || Input.mousePosition.x > (Screen.width / 2f))
            {
                return;
            }

            Show(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && mouseDelta.magnitude > 0 && isDragging)
        {
            Drag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Hide();
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++) {
            var touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) || touch.position.x > (Screen.width / 2f)) {
                        return;
                    }
                    
                    Show(touch.position);
                    break;
                case TouchPhase.Moved:
                    Drag(touch.position);
                    break;
                case TouchPhase.Ended:
                    Hide();
                    break;
            }
        }
#endif

        player.SetDirection(direction);
    }


    private void Show(Vector2 _mousePosition)
    {
        bg.enabled = true;
        joystick.enabled = true;
        isDragging = true;

        Vector2 mousePosition = _mousePosition;
        mousePosition.y -= Screen.height;
        bg.rectTransform.anchoredPosition = mousePosition;

        originalMousePosition = mousePosition;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void Drag(Vector2 _mousePosition)
    {
        SetJoyStickPosition(_mousePosition);
        SetDirection();
    }

    private void SetJoyStickPosition(Vector2 _mousePosition)
    {
        //Get mouse position, accout for excess Y data.
        Vector2 mousePosition = _mousePosition;
        mousePosition.y -= Screen.height;

        //Find the direction.
        Vector2 mouseDirection = (mousePosition - originalMousePosition).normalized;
        float mouseDistance = Vector2.Distance(mousePosition, originalMousePosition);
        float clampedDistance = Mathf.Clamp(mouseDistance, 0, bg.GetPixelAdjustedRect().width / 2);

        //Move the image to reflect all this updated Data.
        joystick.rectTransform.anchoredPosition = mouseDirection * clampedDistance;
    }

    private void SetDirection()
    {
        Vector2 extents = bg.GetPixelAdjustedRect().size / 2;
        Vector2 joyStickPos = joystick.rectTransform.anchoredPosition;

        float x = Mathf.Lerp(-1, 1, Mathf.InverseLerp(-extents.x, extents.x, joyStickPos.x));
        float y = Mathf.Lerp(-1, 1, Mathf.InverseLerp(-extents.y, extents.y, joyStickPos.y));

        direction = new Vector2(x, y);
    }

    private void Hide()
    {
        bg.enabled = false;
        joystick.enabled = false;
        isDragging = false;
        direction = Vector2.zero;

        Debug.Log("HIIIIIIIDE");
    }
}